using Wpf.Ui.Abstractions.Controls;
using WPFUI_SAMPLE.ViewModels.Pages;

namespace WPFUI_SAMPLE.Views.Pages;
public partial class DashboardPage : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
