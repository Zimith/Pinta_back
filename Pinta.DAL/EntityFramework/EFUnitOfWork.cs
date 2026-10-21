using Pinta.DAL.EntityFramework.Auth;
using Pinta.DAL.EntityFramework.Security;
using Pinta.DAL.EntityFramework.Social;
using Pinta.DAL.EntityFramework.Posts;
using Pinta.DAL.EntityFramework.Profile;

using Pinta.DAL.interfaces;
using Pinta.DAL.interfaces.Auth;
using Pinta.DAL.interfaces.Security;
using Pinta.DAL.interfaces.Social;
using Pinta.DAL.interfaces.Posts;
using Pinta.DAL.interfaces.FileSystem;
using Pinta.DAL.EntityFramework.FileSystem;
using Pinta.DAL.interfaces.Profile;
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

    private IFileRepository? fileRepository;
    public IFileRepository FileRepository
    {
        get
        {
            if(this.fileRepository is null)
            {
                this.fileRepository = new EFFileRepository(_context);
            }
            return this.fileRepository;
        }
    }

    private IImageRepository? imageRepository;
    public IImageRepository ImageRepository
    {
        get
        {
            if(this.imageRepository is null)
            {
                this.imageRepository = new EFImageRepository(_context);
            }
            return this.imageRepository;
        }
    }

    private IPostRepository? postRepository;
    public IPostRepository PostRepository
    {
        get
        {
            if(this.postRepository is null)
            {
                this.postRepository = new EFPostRepository(_context);
            }
            return this.postRepository;
        }
    }

    private IFollowRepository? followRepository;
    public IFollowRepository FollowRepository
    {
        get
        {
            if(this.followRepository is null)
            {
                this.followRepository = new EFFollowRepository(_context);
            }
            return this.followRepository;
        }
    }

    private IAvatarRepository? avatarRepository;
    public IAvatarRepository AvatarRepository
    {
        get
        {
            if(this.avatarRepository is null)
            {
                this.avatarRepository = new EFAvatarRepository(_context);
            }
            return this.avatarRepository;
        }
    }

    private IPortadaperfilRepository? portadaperfilRepository;
    public IPortadaperfilRepository PortadaperfilRepository
    {
        get
        {
            if(this.portadaperfilRepository is null)
            {
                this.portadaperfilRepository = new EFPortadaperfilRepository(_context);
            }
            return this.portadaperfilRepository;
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

    public async Task SaveChangesasync()
    {
        await _context.SaveChangesAsync();
    }
}