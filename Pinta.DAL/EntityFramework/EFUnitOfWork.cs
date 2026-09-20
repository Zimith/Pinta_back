using Pinta.DAL.EntityFramework.Auth;
using Pinta.DAL.EntityFramework.Security;
using Pinta.DAL.EntityFramework.Socialmedia;
using Pinta.DAL.interfaces;
using Pinta.DAL.interfaces.Auth;
using Pinta.DAL.interfaces.Security;
using Pinta.DAL.interfaces.Socialmedia;

namespace Pinta.DAL.EntityFramework;

public class EFUnitOfWork (PintaDbContext context) : IUnitOfWork
{
    private readonly PintaDbContext _context = context;

    
    private IUserRepository? userRepository;
    public IUserRepository UserRepository
    {
        get
        {
            if(this.userRepository == null)
            {
                this.userRepository = new EFUserRepository(_context);
            }
            return this.userRepository;
        }
    }

    private IBanRepository? banRepository;
    public IBanRepository BanRepository
    {
        get
        {
            if(this.banRepository == null)
            {
                this.banRepository = new EFBanRepository(_context);
            }
            return this.banRepository;
        }
    }

    private IReactionRepository? reactionRepository;
    public IReactionRepository ReactionRepository
    {
        get
        {
            if(this.reactionRepository == null)
            {
                this.reactionRepository = new EFReactionRepository(_context);
            }
            return this.reactionRepository;
        }
    }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}