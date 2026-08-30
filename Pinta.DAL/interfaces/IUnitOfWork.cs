using Pinta.DAL.interfaces.Auth;
using Pinta.DAL.interfaces.Security;
namespace Pinta.DAL.interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IUserRepository UserRepository { get; }

    IBanRepository BanRepository { get; }
}
