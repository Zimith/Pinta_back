using Pinta.DAL.interfaces.Auth;
namespace Pinta.DAL.EntityFramework.Auth;

public class EFUserRepository : IUserRepository
{
    private PintaDbContext _context;

    public EFUserRepository(PintaDbContext context)
    {
        _context = context;
    }
}