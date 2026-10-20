using Pinta.DAL.interfaces.Posts;

namespace Pinta.DAL.EntityFramework.Posts;

public class EFPostRepository : IPostRepository
{
    private PintaDbContext dbContext;
    public EFPostRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}