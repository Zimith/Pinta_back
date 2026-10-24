using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;
using Pinta.Domain.Auth;
using Pinta.Domain.Exceptions;
using Pinta.Domain.FileSystem;
using Pinta.Domain.Posts;

namespace Pinta.API.Controllers;

[ApiController]
[Route("api/post")]
public class PostsController : ControllerBase
{
    private IUnitOfWork dataBase;
    public PostsController(IUnitOfWork unitOfWork)
    {
        this.dataBase = unitOfWork;
    }

	[Authorize]
    [HttpGet]
    public async Task<IActionResult> GetPosts([FromQuery] GetPostRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

		User user = await this.dataBase.UserRepository.GetUserById(userId);

		List<Post> posts = await this.dataBase.PostRepository.GetPosts(
			user,
			request.currentPage,
			request.pageSize,
			request.orderBy,
			request.orderDirection,
			request.search
		);
		
		List<PostDTO> postDTOs = new List<PostDTO>();
		foreach (Post post in posts)
		{
			postDTOs.Add(new PostDTO
			{
				Id = post.Id,
				UserName = post.GetUserName(),
				Content = post.Description,
				UrlImage = post.ImageUrl(),
			});
		}

		return Ok(new ResponseDTO<List<PostDTO>>()
		{
			code = (int)HttpStatusCode.OK,
			message = "Post obtenidos correctamente",
			payload = postDTOs,
			success = true
		});
    }
}
