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
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.title))
        {
            throw new ValidationException("El título del post no puede estar vacío");
        }

        if (string.IsNullOrWhiteSpace(request.body))
        {
            throw new ValidationException("El contenido del post no puede estar vacío");
        }

        long userId = User.GetUserId();
        if (userId <= 0 || userId > int.MaxValue)
        {
            throw new InvalidCredentialsException("El usuario del token no es válido");
        }

        User? user = await this.dataBase.UserRepository.GetUserById((int)userId);
        if (user is null)
        {
            throw new BusinessNotFoundException("El usuario no existe");
        }

        Post post = new Post
        {
            Title = request.title,
            Description = request.body,
            CreationDate = DateTime.UtcNow,
            User = user
        };

        await this.dataBase.PostRepository.Create(post);
        await this.dataBase.SaveChangesAsync(cancellationToken);

        CreatePostResponse response = new CreatePostResponse
        {
            id = post.Id,
            title = post.Title,
            body = post.Description
        };

        return Created($"/api/post/{post.Id}", new ResponseDTO<CreatePostResponse>
        {
            code = (int)HttpStatusCode.Created,
            message = "Post creado correctamente",
            payload = response,
            success = true
        });
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPost(int id,[FromBody] UpdatePostRequest request, CancellationToken cancellationToken)
    {
        Post? post = await this.dataBase.PostRepository.GetById(id);
        if (post is null)
        {
            throw new BusinessNotFoundException("El post no existe");
        }

        long userId = User.GetUserId();
        if (post.UserId != userId)
        {
            return Forbid();
        }

        post.Edit(request.title, request.body);
        await this.dataBase.SaveChangesAsync(cancellationToken);

        UpdatePostResponse response = new UpdatePostResponse
        {
            id = post.Id,
            title = post.Title,
            body = post.Description
        };

        return Ok(new ResponseDTO<UpdatePostResponse>
        {
            code = (int)HttpStatusCode.OK,
            message = "Post actualizado correctamente",
            payload = response,
            success = true
        });
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id, CancellationToken cancellationToken)
    {
        Post? post = await this.dataBase.PostRepository.GetById(id);
        if (post is null)
        {
            throw new BusinessNotFoundException("El post no existe");
        }

        long userId = User.GetUserId();
        if (post.UserId != userId)
        {
            return Forbid();
        }

        this.dataBase.PostRepository.Delete(post);
        await this.dataBase.SaveChangesAsync(cancellationToken);

        return NoContent();
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
