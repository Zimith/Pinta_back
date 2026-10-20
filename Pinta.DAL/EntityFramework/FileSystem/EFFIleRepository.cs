using Pinta.DAL.interfaces.FileSystem;

namespace Pinta.DAL.EntityFramework.FileSystem;

public class EFFileRepository : IFileRepository
{
    private PintaDbContext dbContext;
    public EFFileRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}