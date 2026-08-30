using Pinta.Domain.Auth;

namespace Pinta.DAL.interfaces.Auth;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    User GetUserByUserName(object userName);
}