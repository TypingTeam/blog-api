using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Keeper.Domain;

public partial class Post: Entity
{
    private Post() { }
    public string Title { get; private set; }
    public string ShortDescription { get; private set; }
    public string Content { get; private set; }
    public string PreviewImageUrl { get; private set; }
    public int ReadingTimeInMinutes { get; private set; }
    
    public int Likes { get; set; }
    public string? PreviewImageUrlFallback { get; private set; }
    public IList<string> Tags { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public DateTime? PublishedAt { get; private set; }
    public bool IsPublished => PublishedAt is not null;
    
    public DateTime? ScheduledPublishing { get; private set; }
    public bool IsScheduled => ScheduledPublishing is not null;

    public string Slug => GenerateSlug();
    
    [GeneratedRegex(
        @"[^A-Za-z0-9\s]",
        RegexOptions.CultureInvariant,
        matchTimeoutMilliseconds: 1000)]
    private static partial Regex MatchIfSpecialCharactersExist();

    [GeneratedRegex(
        @"\s+",
        RegexOptions.CultureInvariant,
        matchTimeoutMilliseconds: 1000)]
    private static partial Regex MatchIfAdditionalSpacesExist();

    [GeneratedRegex(
        @"\s",
        RegexOptions.CultureInvariant,
        matchTimeoutMilliseconds: 1000)]
    private static partial Regex MatchIfSpaceExist();
    
    public static Post Create(
        string title,
        string shortDescription,
        string content,
        string previewImageUrl,
        string? previewImageUrlFallback = null,
        DateTime? scheduledPublishDate = null,
        IEnumerable<string>? tags = null)
    {
        var createdAt = DateTime.UtcNow;

        var blogPost = new Post
        {
            Title = title,
            ShortDescription = shortDescription,
            Content = content,
            UpdatedAt = createdAt,
            CreatedAt = createdAt,
            ScheduledPublishing = scheduledPublishDate,
            PreviewImageUrl = previewImageUrl,
            PreviewImageUrlFallback = previewImageUrlFallback,
            Tags = tags?.Select(t => t.Trim()).ToImmutableArray() ?? [],
            ReadingTimeInMinutes = 11
        };

        return blogPost;
    }

    public void Publish()
    {
        ScheduledPublishing = null;
        PublishedAt = DateTime.UtcNow;
    }

    public void Update(Post post)
    {
        ArgumentNullException.ThrowIfNull(post);

        if (post == this)
            return;

        Title = post.Title;
        ShortDescription = post.ShortDescription;
        Content = post.Content;
        UpdatedAt = DateTime.UtcNow;
        ScheduledPublishing = post.ScheduledPublishing;
        PreviewImageUrl = post.PreviewImageUrl;
        PreviewImageUrlFallback = post.PreviewImageUrlFallback;
        Tags = post.Tags;
        ReadingTimeInMinutes = post.ReadingTimeInMinutes;
    }
    
    private string GenerateSlug()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            return Title;
        }

        var normalizedTitle = Title.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedTitle.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
        {
            stringBuilder.Append(c);
        }

        var cleanTitle = stringBuilder
            .ToString()
            .Normalize(NormalizationForm.FormC)
            .ToLower(CultureInfo.CurrentCulture);

        cleanTitle = MatchIfSpecialCharactersExist().Replace(cleanTitle, "");
        cleanTitle = MatchIfAdditionalSpacesExist().Replace(cleanTitle, " ");
        cleanTitle = MatchIfSpaceExist().Replace(cleanTitle, "-");

        return cleanTitle.Trim();
    }
}
