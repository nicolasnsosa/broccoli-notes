using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Models.Domain;
using RepositoryManager = Domain.Repositories.RepositoryManager;

namespace Domain.Services.ServiceConfiguration;

public static class ServicesServiceExtension
{
    public static void ServicesConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<WorkspaceService>();
    }
}