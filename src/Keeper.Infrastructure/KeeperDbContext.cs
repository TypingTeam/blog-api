using Keeper.Domain;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Infrastructure;

public sealed class KeeperDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KeeperDbContext).Assembly);
    }
}
