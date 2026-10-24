using Pinta.Domain.Auth;

namespace Pinta.DAL.interfaces.Auth;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username);

    Task<bool> Create(User user);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(int idToken);
    Task<List<User>> SearchUsers(string username);
    Task<User?> GetProfile(int id);
    Task<User> GetUserById(long userId);
}