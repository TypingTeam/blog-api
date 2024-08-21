using Keeper.Application;

namespace Keeper.API;

internal static class PostApi
{
    public static RouteGroupBuilder MapBlogPostApi(this RouteGroupBuilder builder)
    {
        builder.MapGet("/post/{postSlug}", (string postSlug, PostsHandler handler, CancellationToken token) 
            => handler.GetPostBySlugAsync(postSlug, token));

        return builder;
    }
}
