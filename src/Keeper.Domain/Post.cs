using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Keeper.Domain.Services;

namespace Keeper.Domain;

public partial class Post: Entity
{
    private Post() { }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public string Content { get; private set; }
    public string PreviewImageUrl { get; private set; }
    public int ReadingTimeInMinutes { get; private set; }
    public int Likes { get; set; }
    public IList<string> Tags { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public DateTime? PublishedAt { get; private set; }
    public bool IsPublished => PublishedAt is not null;
    
    public DateTime? ScheduledPublishing { get; private set; }
    public bool IsScheduled => ScheduledPublishing is not null;
    
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
            Description = shortDescription,
            Content = content,
            UpdatedAt = createdAt,
            CreatedAt = createdAt,
            ScheduledPublishing = scheduledPublishDate,
            PreviewImageUrl = previewImageUrl,
            Tags = tags?.Select(t => t.Trim()).ToImmutableArray() ?? [],
            ReadingTimeInMinutes = 11,
            Slug = TitleSanitizer.GenerateSlug(title)
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
        Description = post.Description;
        Content = post.Content;
        UpdatedAt = DateTime.UtcNow;
        ScheduledPublishing = post.ScheduledPublishing;
        PreviewImageUrl = post.PreviewImageUrl;
        Tags = post.Tags;
        ReadingTimeInMinutes = post.ReadingTimeInMinutes;
    }
}
