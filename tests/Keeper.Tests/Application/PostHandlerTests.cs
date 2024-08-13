using FluentAssertions;
using Keeper.Application;
using Keeper.Domain;
using Keeper.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Keeper.Tests.Application;

public class PostHandlerTests
{
    [Fact]
    public async Task PostsHandler_GetPostBySlug_PostReturned()
    {
        var contextOptions = new DbContextOptionsBuilder<KeeperDbContext>()
            .UseInMemoryDatabase("PostHandlerDb")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        using var context = new KeeperDbContext(contextOptions);
        context.Posts.Add(Post.Create("test", "test", "test", "test"));

        var handler = new PostsHandler(context);

        var post = await handler.GetPostBySlugAsync("tests", default);

        post.Should().NotBeNull();
    }
}
