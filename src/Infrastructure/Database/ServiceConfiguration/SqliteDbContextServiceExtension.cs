using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.ServiceConfiguration;

public static class SqliteDbContextServiceExtension
{
    public static void SqliteDbContextConfigureServices(this IServiceCollection services, string fileName)
    {
        if (!fileName.EndsWith(".db"))
            fileName += ".db";

        services.AddDbContext<MyDbContext>(
            options => options.UseSqlite($"Data Source={fileName}"), 
                ServiceLifetime.Singleton);
        services.AddSingleton<RepositoryManager>();
    }
}