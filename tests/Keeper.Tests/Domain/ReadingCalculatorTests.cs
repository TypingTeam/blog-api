using FluentAssertions;
using Keeper.Domain;
using Keeper.Domain.Services;

namespace Keeper.Tests.Domain;

public class ReadingCalculatorTests
{
    [Fact]
    public void ShortPostContains4Words()
    {
        var post = Post.Create("post", "post", "Short Content in four words", string.Empty);

        var readingTime = ReadingCalculator.CalculateReadingTime(post);

        readingTime.Should().Be(1);
    }
}
