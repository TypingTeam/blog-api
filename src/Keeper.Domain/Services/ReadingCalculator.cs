namespace Keeper.Domain.Services;

public static class ReadingCalculator
{
    public static int CalculateReadingTime(Post post)
    {
        const double WordsPerMinute = 250;

        var wordCount = post.Content.Count(content => content == ' ');
        var readTimeWords = wordCount / WordsPerMinute;
        
        return (int)Math.Ceiling(readTimeWords);
    }
}
