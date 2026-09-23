using Pinta.Domain.Auth;

namespace Pinta.DAL.interfaces.Auth;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username);

    Task<bool> Create(User user);
}