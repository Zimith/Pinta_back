using Pinta.Domain.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;
using Pinta.Domain.Auth;
using Pinta.Domain.FileSystem;

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

    public async Task<IActionResult> Create([FromForm]CreateUserRequest request)
    {
        await validationUser(request);
        Image? image = await generatorImage(request);

        //Agregar validacion usando renex
        //8 caracteres, al menos una letra mayúscula, una letra minúscula, un número y un carácter especial

        User user = createUser(request, image);
        await saveToDatabase(user);

        CreateUserResponse response = new CreateUserResponse
        {
            id = user.Id,
            username = user.Username,
            hashedPassword = user.HashedPassword,
            email = user.Email,
            //Fullname = user.Fullname,
            //avatar = user.GetAvatar()
        };

        return Ok(new ResponseDTO<CreateUserResponse>
        {
            success = true,
            message = "Usuario creado exitosamente",
            code = (int)HttpStatusCode.OK,
            payload = response
        });

    }

    private async Task saveToDatabase(User user)
    {
        await dataBase.UserRepository.Create(user);
        await this.dataBase.SaveChangesasync();
    }

    private static User createUser(CreateUserRequest request, Image? image)
    {
        User user = new User
        {
            Username = request.username,
            Email = request.email,
            Fullname = request.Fullname,
            Avatar = image

        };
        Console.WriteLine($"AVATAR: {(user.Avatar is null ? "NULL" : user.Avatar.FileName)}");
        user.SetPassword(request.hashedPassword);
        return user;
    }

    private async Task<Image?> generatorImage(CreateUserRequest request)
    {
        if (request.file is null) return null;
        
        await using var stream = request.file.OpenReadStream();
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        byte[]? fileData = ms.ToArray();
        string?fileName = request.file.FileName;
        
        FileStorageService storageService = new FileStorageService(env);
        return await storageService.SaveImageAsync(fileData, fileName);
    }

    private async Task validationUser(CreateUserRequest request)
    {
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

        User? existinguser = await this.dataBase.UserRepository.GetUserByUsername(request.username);

        if (existinguser != null)
        {
            throw new ValidationException("El nombre de usuario ya existe " + request.username);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login ([FromBody] LoginRequest request)
    {
        User user = await validateLogin(request);

        //Esta todo correcto, generamos el token debajo

        string token = this.generator.GenerateToken(user.Id);

        LoginResponse response = new LoginResponse
        {
            id = user.Id,
            username = user.Username,
            token = token,
        };
        return Ok(new ResponseDTO<LoginResponse>
        {
            code = (int)HttpStatusCode.OK,
            message = "Login exitoso",
            success = true,
            payload = response
        });
    }

    private async Task<User> validateLogin(LoginRequest request)
    {
        if (String.IsNullOrEmpty(request.username))
        {
            throw new ValidationException("El nombre de usuario no puede estar vacío");
        }
        if (String.IsNullOrEmpty(request.hashedPassword))
        {
            throw new ValidationException("La contraseña no puede estar vacía");
        }
        User? user = await this.dataBase.UserRepository.GetUserByUsername(request.username);
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
}
