using Microsoft.EntityFrameworkCore;
using Pinta.DAL.interfaces.FileSystem;
using Pinta.Domain.FileSystem;

namespace Pinta.DAL.EntityFramework.FileSystem;

public class EFFileRepository : IFileRepository
{
    private PintaDbContext dbContext;
    public EFFileRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Pinta.Domain.FileSystem.File?> GetById(long id)
    {
        Pinta.Domain.FileSystem.File? file = await this.dbContext.Files.FindAsync(id);
        return file;
    }

    public async Task<Domain.FileSystem.File?> GetByName(string name)
    {
        List<Pinta.Domain.FileSystem.File> files = await this.dbContext.Files.Where(file => file.FileName.Trim().ToUpper().Equals(name.Trim().ToUpper())).ToListAsync();
        if (files != null && files.Count > 0) return files[0];
        return null;
        
    }
}