using Pinta.DAL.interfaces.Auth;
namespace Pinta.DAL.EntityFramework.Auth;

public class EFUserRepository : IUserRepository
{
    private PintaDbContext dbContext;
    public EFUserRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}