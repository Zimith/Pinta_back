using ImageMagick;
using Pinta.Domain.FileSystem;

public class FileStorageService
{
    private readonly IWebHostEnvironment _env;

    private readonly string _storagePath;

    private readonly string _relativeStoragePath;

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
        _relativeStoragePath = Path.Combine("storage", "image");
        _storagePath = Path.Combine(_env.ContentRootPath, _relativeStoragePath);
    }

    public async Task<Image> SaveImageAsync(
        byte[] data,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_storagePath);

        var extension = Path.GetExtension(fileName);
        var finalFileName = $"{Guid.NewGuid()}{extension}";
        var physicalPath = Path.Combine(_storagePath, finalFileName);
        var relativePath = Path.Combine(_relativeStoragePath, finalFileName);

        using var image = new MagickImage(data);

        image.Format = MagickFormat.WebP;
        image.Write(physicalPath);

        return new Image
        {
            FileName = finalFileName,
            ContentType = "image/webp",
            StoragePath = relativePath,
            Width = (int)image.Width,
            Height = (int)image.Height
        };
    }

    public string GetPhysicalPath(string storagePath)
    {
        if (string.IsNullOrEmpty(storagePath))
        {
            return string.Empty;
        }

        if (Path.IsPathRooted(storagePath))
        {
            return storagePath;
        }

        return Path.Combine(_env.ContentRootPath, storagePath);
    }
}