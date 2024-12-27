using System.IO;
using System.Reflection;
using System.Windows.Threading;
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;
using WPFUI_SAMPLE.Services;
using WPFUI_SAMPLE.ViewModels.Pages;
using WPFUI_SAMPLE.ViewModels.Windows;
using WPFUI_SAMPLE.Views.Pages;
using WPFUI_SAMPLE.Views.Pages.SamplePage;
using WPFUI_SAMPLE.Views.Windows;

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



    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<ApplicationHostService>();

            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Theme manipulation
            services.AddSingleton<IThemeService, ThemeService>();

            // TaskBar manipulation
            services.AddSingleton<ITaskBarService, TaskBarService>();

            // Service containing navigation, same as INavigationWindow... but without window
            services.AddSingleton<INavigationService, NavigationService>();

            // Main window with navigation
            services.AddSingleton<INavigationWindow, MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<DashboardPage>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<DataPage>();
            services.AddSingleton<DataViewModel>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<SettingsViewModel>();


            services.AddSingleton<IOrderService,OrderService>();
            services.AddSingleton<OrderPage>();
            services.AddSingleton<OrderViewModel>();

            // 依赖注入,注册FreeSql,可以避免多次实例化FreeSql
            services.AddSingleton<IFreeSql>(provider =>
            {
                return new FreeSqlBuilder()
                    .UseConnectionString(DataType.Sqlite, "Data Source=database.db")
                    .Build();
            });
        }).Build();

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
