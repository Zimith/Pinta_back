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

    public async Task<User?> GetUserByUsername(string username)
    {
        List <User> users = await this._context.Users.Where(u => u.Username.ToUpper().Equals(username.ToUpper())).ToListAsync();

        if(users != null && users.Count > 0) return users[0];
        return null;
    }
}