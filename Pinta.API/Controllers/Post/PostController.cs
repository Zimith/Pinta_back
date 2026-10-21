using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;

namespace Pinta.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
	private readonly IUnitOfWork dataBase;

	public PostController(IUnitOfWork dataBase)
	{
		this.dataBase = dataBase;
	}

    [HttpPost]
    public Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        
    }
}