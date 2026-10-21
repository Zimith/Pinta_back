using Pinta.DAL.interfaces.Profile;
namespace Pinta.DAL.EntityFramework.Profile;
public class EFAvatarRepository : IAvatarRepository
{
    private PintaDbContext dbContext;
    public EFAvatarRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}