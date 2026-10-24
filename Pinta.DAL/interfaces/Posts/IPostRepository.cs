using Pinta.Domain.Auth;
using Pinta.Domain.Posts;

namespace Pinta.DAL.interfaces.Posts;

public interface IPostRepository
{
    Task<List<Post>> GetPosts(User user, int currentPage, int pageSize, string orderBy, string orderDirection, string search);
}