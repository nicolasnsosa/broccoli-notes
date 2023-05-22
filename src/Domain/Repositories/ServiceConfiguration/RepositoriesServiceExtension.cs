using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Models.Domain;

namespace Domain.Repositories.ServiceConfiguration;

public static class RepositoriesServiceExtension
{
    public static void RepositoriesConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IEntityConfiguration, EntityConfiguration<Workspace>>();
        services.AddSingleton<Repository<Workspace>>();

        services.AddSingleton<RepositoryManager>();
    }
}