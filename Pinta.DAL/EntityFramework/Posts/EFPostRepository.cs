using Microsoft.EntityFrameworkCore;
using Pinta.DAL.interfaces.Posts;
using Pinta.Domain.Auth;
using Pinta.Domain.Posts;

namespace Pinta.DAL.EntityFramework.Posts;

public class EFPostRepository : IPostRepository
{
    private PintaDbContext dbContext;
    public EFPostRepository(PintaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> Create(Post post)
    {
        await dbContext.Posts.AddAsync(post);
        return true;
    }

    public async Task<List<Post>> GetPosts(
        User user, 
        int currentPage, 
        int pageSize, 
        string orderBy, 
        string orderDirection, 
        string? search)
    {
        //Listado de ids de gente que yo sigo.
        IQueryable<int> followedIds = dbContext.Follows
            .Where(f => f.UserFollowing != null && f.UserFollowing.Id == user.Id)
            .Select(f => f.UserFollowed!.Id);

        IQueryable<Post> posts = dbContext.Posts
            .Where(post => post.User.Id == user.Id ||
                followedIds.Contains(post.User.Id));

        if (!string.IsNullOrEmpty(search))
        {
            posts = posts.Where(post => post.Description.Contains(search));
        }

        AddOrder(ref orderBy, ref orderDirection, posts);

        //Paginación
        posts = posts
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);

        return await posts.ToListAsync();
    }

    private static void AddOrder(ref string orderBy, ref string orderDirection, IQueryable<Post> posts)
    {
        if (string.IsNullOrEmpty(orderBy)) orderBy = "CreatedAt";
        if (string.IsNullOrEmpty(orderDirection)) orderDirection = "desc";

        if (orderBy.ToUpper().Equals("CreatedAt"))
        {
            if (orderDirection.ToUpper().Equals("asc"))
            {
                posts.OrderBy(post => post.CreationDate);
            }
            else
            {
                posts.OrderByDescending(post => post.CreationDate);
            }
        }
        else if (orderBy.ToUpper().Equals("Description"))
        {
            if (orderDirection.ToUpper().Equals("asc"))
            {
                posts.OrderBy(post => post.Description);
            }
            else
            {
                posts.OrderByDescending(post => post.Description);
            }
        }
        else
        {
            if (orderBy.ToUpper().Equals("asc"))
            {
                if (orderDirection.ToUpper().Equals("asc"))
                {
                    posts.OrderBy(post => post.User.Id);
                }
                else
                {
                    posts.OrderByDescending(post => post.User.Id);
                }
            }
        }
    }
}