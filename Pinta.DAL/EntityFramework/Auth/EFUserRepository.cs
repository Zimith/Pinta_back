using Microsoft.EntityFrameworkCore;
using Pinta.DAL.interfaces.Auth;
using Pinta.Domain.Auth;
namespace Pinta.DAL.EntityFramework.Auth;

public class EFUserRepository : IUserRepository
{
    private PintaDbContext _context;

    public EFUserRepository(PintaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Create(User user, CancellationToken cancellationToken = default)
    {
        await this._context.Users.AddAsync(user, cancellationToken);
        return true;
    }

    public async Task<User?> GetUserById(int idToken, CancellationToken cancellationToken = default)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Id == idToken, cancellationToken);
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper(), cancellationToken);
    }

    public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        List <User> users = await this._context.Users
            .Where(u => u.Username.ToUpper().Equals(username.ToUpper()))
            .ToListAsync(cancellationToken);

        if(users != null && users.Count > 0) return users[0];
        return null;
    }

    public async Task<List<User>> SearchUsers(string username, CancellationToken cancellationToken = default)
    {
        List<User> users = await this._context.Users
            .Where(u =>u.Username.ToUpper().Contains(username.ToUpper()))
            .ToListAsync(cancellationToken);
        return users;
    }

    public async Task<User?> GetProfile(int id, CancellationToken cancellationToken = default)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<User> GetUserById(long userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}