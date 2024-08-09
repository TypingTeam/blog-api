using Keeper.Domain;

namespace Keeper.Application;

public class PostsHandler
{
    public Task<Post> GetPostBySlugAsync(string postId, CancellationToken token)
    {
        return Task.FromResult(Post.Create("test", "test", "test", "no-image"));
    }
}
