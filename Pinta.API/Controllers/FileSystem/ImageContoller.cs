using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;

namespace Pinta.API.Controllers;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
	private readonly IUnitOfWork dataBase;

	public ImageController(IUnitOfWork dataBase)
	{
		this.dataBase = dataBase;
	}
}