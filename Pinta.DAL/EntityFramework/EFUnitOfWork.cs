using Pinta.DAL.interfaces;

namespace Pinta.DAL.EntityFramework;

public class EFUnitOfWork(PintaDbContext context) : IUnitOfWork
{
    private readonly PintaDbContext _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
