using Pinta.DAL.interfaces.Posts;
namespace Pinta.DAL.EntityFramework.Posts;



public class EFReactionRepository : IReactionRepository
{
    private PintaDbContext _context;

    public EFReactionRepository(PintaDbContext context)
    {
        _context = context;
    }
}