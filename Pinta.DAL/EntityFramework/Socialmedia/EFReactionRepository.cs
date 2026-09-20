using Pinta.DAL.interfaces.Socialmedia;
namespace Pinta.DAL.EntityFramework.Socialmedia;



public class EFReactionRepository : IReactionRepository
{
    private PintaDbContext _context;

    public EFReactionRepository(PintaDbContext context)
    {
        _context = context;
    }
}