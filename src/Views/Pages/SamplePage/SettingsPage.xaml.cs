using Wpf.Ui.Controls;
using WPFUI_SAMPLE.ViewModels.Pages;

namespace WPFUI_SAMPLE.Views.Pages;
public partial class SettingsPage : INavigableView<SettingsViewModel>
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
