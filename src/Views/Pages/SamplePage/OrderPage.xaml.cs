using Wpf.Ui.Abstractions.Controls;
using WPFUI_SAMPLE.ViewModels.Pages;

namespace WPFUI_SAMPLE.Views.Pages.SamplePage;
/// <summary>
/// OrderPage.xaml 的交互逻辑
/// </summary>
public partial class OrderPage : INavigableView<OrderViewModel>
{
    public OrderViewModel ViewModel
    {
        get;
    }
    public OrderPage(OrderViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }
}
