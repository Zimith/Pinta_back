using Pinta.Domain.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;
using Pinta.Domain.Auth;
using Pinta.Domain.FileSystem;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Pinta.API.Controllers;

[ApiController]
[Route("api/user")]

public class UserController : ControllerBase
{
    private readonly IUnitOfWork dataBase;
    private IWebHostEnvironment env;
    private JwtTokenGenerator generator;

    public UserController(IUnitOfWork dataBase, IWebHostEnvironment env,JwtTokenGenerator generator)
    {
        this.dataBase = dataBase;
        this.env = env;
        this.generator= generator;
    }

    [HttpPost]

    public async Task<IActionResult> Create([FromForm]CreateUserRequest request, CancellationToken cancellationToken)
    {
        await validationUser(request, cancellationToken);
        Image? image = await generatorImage(request.file, cancellationToken);

        //Agregar validacion usando renex
        //8 caracteres, al menos una letra mayúscula, una letra minúscula, un número y un carácter especial

        User user = createUser(request, image);
        await saveToDatabase(user, cancellationToken);
        

        CreateUserResponse response = new CreateUserResponse
        {
            id = user.Id,
            username = user.Username,
            hashedPassword = user.HashedPassword,
            email = user.Email,
            avatar = user.GetAvatar()
        };

        return Ok(new ResponseDTO<CreateUserResponse>
        {
            success = true,
            message = "Usuario creado exitosamente",
            code = (int)HttpStatusCode.OK,
            payload = response
        });

    }

    private async Task saveToDatabase(User user, CancellationToken cancellationToken)
    {
        await dataBase.UserRepository.Create(user, cancellationToken);
        await dataBase.SaveChangesAsync(cancellationToken);
    }

    private static User createUser(CreateUserRequest request, Image? image)
    {
        User user = new User
        {
            Username = request.username,
            Email = request.email,
            Avatar = image

        };
        Console.WriteLine($"AVATAR: {(user.Avatar is null ? "NULL" : user.Avatar.FileName)}");
        user.SetPassword(request.hashedPassword);
        return user;
    }

    private async Task<Image?> generatorImage(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file is null) return null;
        
        await using var stream = file.OpenReadStream();
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms, cancellationToken);
        byte[]? fileData = ms.ToArray();
        string?fileName = file.FileName;
        
        FileStorageService storageService = new FileStorageService(env);
        return await storageService.SaveImageAsync(fileData, fileName, cancellationToken);
    }

    private async Task validationUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (String.IsNullOrEmpty(request.username))
        {
            throw new ValidationException("El nombre de usuario no puede estar vacío");
        }

        if (String.IsNullOrEmpty(request.hashedPassword))
        {
            throw new ValidationException("La contraseña no puede estar vacía");
        }

        if (String.IsNullOrEmpty(request.email))
        {
            throw new ValidationException("El correo electrónico no puede estar vacío");
        }

        User? existingUserName = await this.dataBase.UserRepository.GetUserByUsername(request.username, cancellationToken);

        if (existingUserName != null)
        {
            throw new ValidationException("El nombre de usuario ya existe " + request.username);
        }
        User?  existingEmail = await this.dataBase.UserRepository.GetUserByEmail(request.email, cancellationToken);
        if (existingEmail !=null)
        {
            throw new ValidationException("El Email ya esta en uso" + request.email);
        }

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login ([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        User user = await validateLogin(request, cancellationToken);

        //Esta todo correcto, generamos el token debajo

        string token = this.generator.GenerateToken(user.Id);

        LoginResponse response = new LoginResponse
        {
            id = user.Id,
            username = user.Username,
            token = token,
            avatar = user.GetAvatar(),
        };
        return Ok(new ResponseDTO<LoginResponse>
        {
            code = (int)HttpStatusCode.OK,
            message = "Login exitoso",
            success = true,
            payload = response
        });
    }

    private async Task<User> validateLogin(LoginRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (String.IsNullOrEmpty(request.username))
        {
            throw new ValidationException("El nombre de usuario no puede estar vacío");
        }
        if (String.IsNullOrEmpty(request.hashedPassword))
        {
            throw new ValidationException("La contraseña no puede estar vacía");
        }

        User? user = await this.dataBase.UserRepository.GetUserByUsername(request.username, cancellationToken);
        if (user == null)
        {
            throw new InvalidCredentialsException("Usuario o contraseña incorrectos");
        }

        if (!user.IsPassword(request.hashedPassword))
        {
            throw new InvalidCredentialsException("Usuario o contraseña incorrectos");
        }

        return user;
    }
    [Authorize]
    [HttpPut("edit")]
    public async Task<IActionResult> Update([FromForm] UpdateRequest request, CancellationToken cancellationToken)
    {

        string? idToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if(string.IsNullOrEmpty(idToken))
        {
            return Unauthorized("Token Invalido");
        }
        int userId = int.Parse(idToken);

        User? userDB = await this.dataBase.UserRepository.GetUserById(userId, cancellationToken);
        if (userDB==null)
        {
            return NotFound("El usuario no existe");
        }
        if(!string.IsNullOrEmpty(request.username))
        {
            userDB.Username = request.username;
        }
        if(request.avatar != null)
        {
            userDB.Avatar = await generatorImage(request.avatar, cancellationToken);
        }
        if(request.banner != null)
        {
            userDB.Banner = await generatorImage(request.banner, cancellationToken);
        }
        await this.dataBase.SaveChangesAsync(cancellationToken);

        UpdateResponse response = new UpdateResponse
        {
            id = userDB.Id,
            username = userDB.Username,
            avatar = userDB.GetAvatar(),
            banner = userDB.GetBanner(),
        };

        return Ok(new ResponseDTO<UpdateResponse>
        {
            code = (int)HttpStatusCode.OK,
            message = "Perfil actualizado correctamente",
            success = true,
            payload = response
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? username = "", CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return BadRequest("El buscador esta vacio, escribi algo");
        }
        List<User> userSearch = await this.dataBase.UserRepository.SearchUsers(username, cancellationToken);
        
        if(userSearch.Count == 0)
        {
            return NotFound("El usuario se encontro.");
        };
        List<SearchUserResponse> responsList = new List<SearchUserResponse>();

        foreach(User users in userSearch)
        {
            SearchUserResponse userList = new SearchUserResponse
            {
                id = users.Id,
                username = users.Username,
                avatar = users.GetAvatar(),
                isFollow = false
            };
            responsList.Add(userList);
        }
        return Ok(new ResponseDTO<List<SearchUserResponse>>
        {
            code = (int)HttpStatusCode.OK,
            message = "Usuarios Encontrados",
            payload = responsList
        });
        
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile ([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("El id del usuario no es válido");
        }
        User? userDB = await this.dataBase.UserRepository.GetProfile(id, cancellationToken);

        if (userDB == null)
        {
            return NotFound("Usuario no encontrado");
        }
        UserProfileResponse response = new UserProfileResponse
        {
            username = userDB.Username,
            avatar = userDB.GetAvatar(),
            banner = userDB.GetBanner(),
            /*
            followingCount = userDB.followingCount,
            followersCount = userDB.followsCount,
            postsCount = userDB.postsCount,
            description = userDB.description,
            country = userDB.country,
            joinData = userDB.joinData,
            */
        };
        return Ok(new ResponseDTO<UserProfileResponse>
        {
            code = (int)HttpStatusCode.OK,
            message = "Perfil encontrado",
            success = true,
            payload = response
        });
    }

}
