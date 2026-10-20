using Pinta.DAL.interfaces.FileSystem;

namespace Pinta.DAL.EntityFramework.FileSystem;

public class EFImageRepository : IImageRepository
{
    private PintaDbContext dbContext;
    public EFImageRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}