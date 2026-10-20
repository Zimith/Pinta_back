using Pinta.DAL.interfaces.Auth;
using Pinta.DAL.interfaces.Security;
using Pinta.DAL.interfaces.Social;
using Pinta.DAL.interfaces.Posts;
using Pinta.DAL.interfaces.FileSystem;
namespace Pinta.DAL.interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task SaveChangesasync();

    IUserRepository UserRepository { get; }
    IFileRepository FileRepository { get; }
    IImageRepository ImageRepository { get; }
    IPostRepository PostRepository { get; }
    IReactionRepository ReactionRepository { get; }
    IBanRepository BanRepository { get; }
    IFollowRepository FollowRepository { get; }
    
}
