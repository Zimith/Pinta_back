using Pinta.DAL.interfaces.Security;
namespace Pinta.DAL.EntityFramework.Security;

public class EFBanRepository : IBanRepository
{
    private PintaDbContext dbContext;
    public EFBanRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}
