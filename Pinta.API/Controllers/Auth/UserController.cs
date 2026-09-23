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

    public UserController(IUnitOfWork dataBase, IWebHostEnvironment env)
    {
        this.dataBase = dataBase;
        this.env = env;
    }

    [HttpPost]

    public async Task<IActionResult> Create([FromForm]CreateUserRequest request)
    {
        if(String.IsNullOrEmpty(request.username))
        {
            throw new ValidationException("El nombre de usuario no puede estar vacío");
        }

        if(String.IsNullOrEmpty(request.hashedPassword))
        {
            throw new ValidationException("La contraseña no puede estar vacía");
        }

        if(String.IsNullOrEmpty(request.email))
        {
            throw new ValidationException("El correo electrónico no puede estar vacío");
        }

        User? existinguser = await this.dataBase.UserRepository.GetUserByUsername(request.username);

        if(existinguser != null)
        {
            throw new ValidationException("El nombre de usuario ya existe " + request.username);
        }

        byte[]? fileData = null;
        string? fileName = null;

        if (request.file is not null)
        {
            await using var stream = request.file.OpenReadStream();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            fileData = ms.ToArray();
            fileName = request.file.FileName;
        }

        Image? image = null;
        FileStorageService storageService = new FileStorageService(env);
        if (fileData is not null && fileName is not null)
        {
            image = await storageService.SaveImageAsync(fileData,fileName);
            Console.WriteLine($"IMAGE: {image.FileName}");
            Console.WriteLine($"PATH: {image.StoragePath}");
            Console.WriteLine($"WIDTH: {image.Width}");
            Console.WriteLine($"HEIGHT: {image.Height}");
        }

        //Agregar validacion usando renex
        //8 caracteres, al menos una letra mayúscula, una letra minúscula, un número y un carácter especial

        User user = new User
        {
            Username = request.username,
            Email = request.email,
            Fullname = request.Fullname,
            Avatar = image
            
        };
        Console.WriteLine($"AVATAR: {(user.Avatar is null ? "NULL" : user.Avatar.FileName)}");
        user.SetPassword(request.hashedPassword);

        await dataBase.UserRepository.Create(user);

        await this.dataBase.SaveChangesasync();

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
}