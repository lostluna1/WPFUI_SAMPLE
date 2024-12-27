using Wpf.Ui.Abstractions.Controls;
using WPFUI_SAMPLE.ViewModels.Pages;

namespace WPFUI_SAMPLE.Views.Pages;
/// <summary>
/// WritOrderPage.xaml 的交互逻辑
/// </summary>
public partial class WritOrderPage : INavigableView<WritOrderViewModel>
{
    public WritOrderViewModel ViewModel
    {
        get;
    }
    public WritOrderPage(WritOrderViewModel writOrderViewModel)
    {
        ViewModel = writOrderViewModel;
        DataContext = this;
        InitializeComponent();

    }
}
