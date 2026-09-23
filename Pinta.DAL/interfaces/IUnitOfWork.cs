using Pinta.DAL.interfaces.Auth;
using Pinta.DAL.interfaces.Security;
using Pinta.DAL.interfaces.Socialmedia;
namespace Pinta.DAL.interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task SaveChangesasync();

    IUserRepository UserRepository { get; }

    IBanRepository BanRepository { get; }
    IReactionRepository ReactionRepository { get; }
}
