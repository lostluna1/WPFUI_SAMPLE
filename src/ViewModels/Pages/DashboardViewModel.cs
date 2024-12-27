namespace WPFUI_SAMPLE.ViewModels.Pages;

public partial class DashboardViewModel : ViewModel
{
    [ObservableProperty]
    private int _counter = 0;

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Counter++;
    }
}
