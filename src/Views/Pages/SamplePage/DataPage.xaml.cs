using Wpf.Ui.Abstractions.Controls;
using WPFUI_SAMPLE.ViewModels.Pages;

namespace WPFUI_SAMPLE.Views.Pages;
public partial class DataPage : INavigableView<DataViewModel>
{
    public DataViewModel ViewModel
    {
        get;
    }

    public DataPage(DataViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
