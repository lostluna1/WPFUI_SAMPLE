using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using WPFUI_SAMPLE.ViewModels.Windows;

namespace WPFUI_SAMPLE.Views.Windows;
public partial class MainWindow : INavigationWindow
{
    public MainWindowViewModel ViewModel
    {
        get;
    }

    public MainWindow(
        MainWindowViewModel viewModel,
        IPageService pageService,
        INavigationService navigationService
    )
    {
        ViewModel = viewModel;// 构造中注入ViewModel
        DataContext = this;// 设置DataContext

        SystemThemeWatcher.Watch(this);// 监听系统主题变化

        InitializeComponent();
        SetPageService(pageService);

        navigationService.SetNavigationControl(RootNavigation);
    }

    #region INavigationWindow methods

    // => 是C# 7.0中的新特性，用于简化Lambda表达式的写法，可以用来返回一个表达式的值,其等价于 { return RootNavigation; }
    public INavigationView GetNavigation() => RootNavigation;

    public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

    public void SetPageService(IPageService pageService) => RootNavigation.SetPageService(pageService);

    public void ShowWindow() => Show();

    public void CloseWindow() => Close();

    #endregion INavigationWindow methods

    /// <summary>
    /// Raises the closed event.
    /// </summary>
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        // Make sure that closing this window will begin the process of closing the application.
        Application.Current.Shutdown();
    }

    INavigationView INavigationWindow.GetNavigation()
    {
        throw new NotImplementedException();
    }

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }
}
