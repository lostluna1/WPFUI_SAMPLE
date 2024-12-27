using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows.Threading;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;
using Wpf.Ui.Gallery.Services.Contracts;
using WPFUI_SAMPLE.Services;
using WPFUI_SAMPLE.ViewModels.Pages;
using WPFUI_SAMPLE.ViewModels.Windows;
using WPFUI_SAMPLE.Views.Pages;
using WPFUI_SAMPLE.Views.Windows;
using WPFUI_SAMPLE.Views.Pages.SamplePage;
using FreeSql;
using WPFUI_SAMPLE.Contracts.Services;
namespace WPFUI_SAMPLE;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging


    // !!!!!!!!!! 每个接口和ViewModel都需要在这里注册
    // 接口定义命名规范: I+类名+Service,如类名为Page,则接口名为IPageService
    // 接口实现命名规范: 类名+Service,如类名为Page,则接口实现名为PageService
    // ViewModel命名规范: 类名+ViewModel,如类名为Page,则ViewModel名为PageViewModel
    // 实体类命名规范: 类名,如类名为Order,则实体类名为OrderEntity



    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            _ = c.SetBasePath(AppContext.BaseDirectory);
        })
        .ConfigureServices(
            (_1, services) =>
            {
                _ = services.AddNavigationViewPageProvider();

                // App Host
                _ = services.AddHostedService<ApplicationHostService>();

                // Main window container with navigation
                _ = services.AddSingleton<IWindow, MainWindow>();
                _ = services.AddSingleton<MainWindowViewModel>();
                _ = services.AddSingleton<INavigationService, NavigationService>();
                _ = services.AddSingleton<ISnackbarService, SnackbarService>();
                _ = services.AddSingleton<IContentDialogService, ContentDialogService>();
                _ = services.AddSingleton<WindowsProviderService>();

                // Top-level pages
                _ = services.AddSingleton<DashboardPage>();
                _ = services.AddSingleton<DashboardViewModel>();
                _ = services.AddSingleton<SettingsPage>();
                _ = services.AddSingleton<SettingsViewModel>();

                // Sample pages 需要注入页面类名，页面的VM，页面的VM依赖的服务(接口)
                _ = services.AddSingleton<OrderPage>();
                _ = services.AddSingleton<OrderViewModel>();
                _ = services.AddSingleton<IOrderService, OrderService>();

                _ = services.AddSingleton<WritOrderPage>();
                _ = services.AddSingleton<WritOrderViewModel>();
                _ = services.AddSingleton<IWritOrderService, WritOrderService>();
                // 依赖注入,注册FreeSql,可以避免多次实例化FreeSql
                services.AddSingleton<IFreeSql>(provider =>
                {
                    return new FreeSqlBuilder()
                    
                        .UseConnectionString(DataType.Sqlite, "Data Source=database.db")
                        .UseMonitorCommand(cmd => Console.WriteLine(cmd.CommandText))// 启用控制台打印SQL
                        .Build();
                });

            }
        )
        .Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}
