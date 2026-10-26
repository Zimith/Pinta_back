using Pinta.Domain.Auth;

namespace Pinta.DAL.interfaces.Auth;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default);

    Task<bool> Create(User user, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserById(int idToken, CancellationToken cancellationToken = default);
    Task<List<User>> SearchUsers(string username, CancellationToken cancellationToken = default);
    Task<User?> GetProfile(int id, CancellationToken cancellationToken = default);
    Task<User> GetUserById(long userId, CancellationToken cancellationToken = default);
}