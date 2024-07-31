using Keeper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Keeper.API.Infrastructure;

internal static class DatabaseExtensions
{
    internal static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KeeperDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("KeeperDb")));
        
        return services;
    }
}
