using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinta.DAL.interfaces;
using Pinta.Domain.Exceptions;
using Pinta.Domain.FileSystem;

namespace Pinta.API.Controllers;

[ApiController]
[Route("api/images")]
public class ImagesController : ControllerBase
{
    private IUnitOfWork dataBase;
	private IWebHostEnvironment env;
    
    public ImagesController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        this.dataBase = unitOfWork;
        this.env = env;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(string id, CancellationToken cancellationToken)
    {
        Pinta.Domain.FileSystem.File? file = await this.dataBase.FileRepository.GetByName(id);
        if (file is null)
        {
            throw new BusinessNotFoundException("La imagen no existe en la base de datos mi chaval");
        }

		FileStorageService fileStorage = new FileStorageService(this.env);
        var physicalPath = fileStorage.GetPhysicalPath(file.StoragePath);

        if (string.IsNullOrWhiteSpace(physicalPath) || !System.IO.File.Exists(physicalPath))
        {
            throw new BusinessNotFoundException("No se encontró la imagen fisica");
        }

        var contentType = string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType;
        return PhysicalFile(physicalPath, contentType);
    }
}
