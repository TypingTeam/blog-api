using Keeper.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Keeper.Infrastructure.Configuration;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .IsUnicode(false)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.PreviewImageUrl).HasMaxLength(1024).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Likes).IsRequired();
        builder.Property(x => x.Tags).HasMaxLength(2096);
    }
}
