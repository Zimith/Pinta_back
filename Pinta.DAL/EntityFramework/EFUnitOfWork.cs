using Pinta.DAL.interfaces;
using Pinta.DAL.interfaces.Auth;
using Pinta.DAL.interfaces.Security;
using Pinta.DAL.EntityFramework.Auth;
using Pinta.DAL.EntityFramework.Security;

namespace Pinta.DAL.EntityFramework;

public class EFUnitOfWork(PintaDbContext context) : IUnitOfWork
{
    private readonly PintaDbContext _context = context;

    #region IUserRepository
    private IUserRepository? userRepository;
    public IUserRepository UserRepository
    {
        get
        {
            if (userRepository == null)
            {
                userRepository = new EFUserRepository(_context);
            }
            return userRepository;
        }
    }
    #endregion
    #region IbanRepository
    private IBanRepository? banRepository;
    public IBanRepository BanRepository
    {
        get
        {
            if (banRepository == null)
            {
                banRepository = new EFBanRepository(_context);
            }
            return banRepository;
        }
    }
    #endregion


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
