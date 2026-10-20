using Pinta.DAL.interfaces.Social;

namespace Pinta.DAL.EntityFramework.Social;

public class EFFollowRepository : IFollowRepository
{
    private PintaDbContext dbContext;
    public EFFollowRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}