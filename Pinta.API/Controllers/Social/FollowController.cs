using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;

namespace Pinta.API.Controllers;

[ApiController]
[Route("api/follows")]
public class FollowController : ControllerBase
{
	private readonly IUnitOfWork dataBase;

	public FollowController(IUnitOfWork dataBase)
	{
		this.dataBase = dataBase;
	}
}