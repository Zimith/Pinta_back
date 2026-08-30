// using Pinta.DAL.interfaces;
// using Microsoft.AspNetCore.Mvc;
// using Pinta.Domain.Exceptions;
// using System.Text.RegularExpressions;



// using Microsoft.IdentityModel.Tokens;
// using Pinta.Domain.Auth;
// namespace Pinta.API.Controllers;

// [ApiController]
// [Route("api/user")]

// public class UserController : ControllerBase
// {
//     private readonly IUnitOfWork database;

//     public UserController(IUnitOfWork database)
//     {
//         this.database = database;
//     }

//     [HttpPost]
    
//     public Task<IActionResult> Create([FromBody] CreateUserRequest request)
//     {
//         if (String.IsNullOrEmpty(request.username))
//         {
//             throw new ValidationException("Username is required");
//         }
//         if (String.IsNullOrEmpty(request.password))
//         {
//             throw new ValidationException("Password is required");
//         }
//         if (!Regex.IsMatch(request.password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
//         {
//             throw new ValidationException("Password must contain at least 8 characters, one uppercase letter, one lowercase letter and one number");
//         }
//         if (String.IsNullOrEmpty(request.email))
//         {
//             throw new ValidationException("Email is required");
//         }

//         User? user = this.database.UserRepository.GetUserByUserName(request.username);
//         if(user != null)
//         {
//             throw new ValidationException("A user with this name already exists " + request.username);
//         }

//         user = this.database.UserRepository.GetUserByEmail(request.email);
//         if (user !=null)
//         {
//             throw new ValidationException("A user with this email already exists " + request.email);
//         };


//         User newuser = new User
//         {
//             Username = request.username
//         };


//     }

// }