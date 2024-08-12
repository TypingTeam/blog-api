using Keeper.Domain;
using Keeper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Application;

public class PostsHandler
{
    private readonly KeeperDbContext _context;

    public PostsHandler(KeeperDbContext context)
    {
        _context = context;
    }

    public async Task<Post> GetPostBySlugAsync(string slug, CancellationToken token)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(post => post.Slug == slug, token)
            ?? throw new Exception("Post not found");

        return post;
    }
}
