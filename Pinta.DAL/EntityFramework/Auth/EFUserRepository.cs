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

    public async Task<bool> Create(User user)
    {
        await this._context.Users.AddAsync(user);
        return true;
    }

    public async Task<User?> GetUserById(int idToken)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Id == idToken);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        List <User> users = await this._context.Users.Where(u => u.Username.ToUpper().Equals(username.ToUpper())).ToListAsync();

        if(users != null && users.Count > 0) return users[0];
        return null;
    }

    public async Task<List<User>> SearchUsers(string username)
    {
        List<User> users = await this._context.Users.Where(u =>u.Username.ToUpper().Contains(username.ToUpper())).ToListAsync();
        return users;
    }

    public async Task<User?> GetProfile(int id)
    {
        return await this._context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User> GetUserById(long userId)
    {
        throw new NotImplementedException();
    }
}