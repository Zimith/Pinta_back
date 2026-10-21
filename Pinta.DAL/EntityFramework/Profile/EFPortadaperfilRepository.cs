using Pinta.DAL.interfaces.Profile;
namespace Pinta.DAL.EntityFramework.Profile;
public class EFPortadaperfilRepository : IPortadaperfilRepository
{
    private PintaDbContext dbContext;
    public EFPortadaperfilRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}