using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Broccoli.ViewModels.Workspaces;
using Broccoli.Windows.Dialogs;
using Domain.Repositories.ServiceConfiguration;
using Domain.Services.ServiceConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Database.ServiceConfiguration;
using ReactiveUI;
using MainWindow = Broccoli.Windows.MainWindow;
using WorkspaceCrudWindow = Broccoli.Windows.Workspaces.WorkspaceCrudWindow;
using WorkspaceManagerWindow = Broccoli.Windows.Workspaces.WorkspaceManagerWindow;

namespace Broccoli;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Infrastructure
        services.SqliteDbContextConfigureServices("database");
        services.RepositoriesConfigureServices();
        services.ServicesConfigureServices();

        // Message Bus
        services.AddSingleton<IMessageBus, MessageBus>();

        // View Models
        services.AddTransient<WorkspaceManagerViewModel>();
        services.AddTransient<WorkspaceCrudViewModel>();

        // Windows
        services.AddTransient<MainWindow>();
        services.AddTransient<WorkspaceManagerWindow>();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetService<WorkspaceManagerWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}