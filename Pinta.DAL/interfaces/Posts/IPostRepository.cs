using Pinta.Domain.Auth;
using Pinta.Domain.Posts;

namespace Pinta.DAL.interfaces.Posts;

public interface IPostRepository
{
    Task<bool> Create(Post post);
    Task<Post?> GetById(int id);
    void Delete(Post post);
    Task<List<Post>> GetPosts(User user, int currentPage, int pageSize, string orderBy, string orderDirection, string search);
}