using Keeper.Domain;

namespace Keeper.Application;

public class PostsHandler
{
    public Post GetPostAsync(string postId, CancellationToken token)
    {
        return Post.Create("test", "test", "test", "no-image");
    }
}
