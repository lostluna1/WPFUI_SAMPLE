using System.Collections.ObjectModel;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Controls;
using WPFUI_SAMPLE.Entity;
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

    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        Console.WriteLine("AutoSuggestBox_SuggestionChosen"+args.SelectedItem);
        var filteredWritOrders = new ObservableCollection<Entity.WritOrderEntity>( ViewModel.WritOrders.Where(x => x.WritOrderNo == args.SelectedItem.ToString()));
        if (args.SelectedItem.ToString()!=null)
        {
            ViewModel.WritOrders = filteredWritOrders;
        }

    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // 如果是清空操作
        if (args.Reason == AutoSuggestionBoxTextChangeReason.ProgrammaticChange
            || string.IsNullOrWhiteSpace(args.Text))
        {
            ViewModel.WritOrders = ViewModel.OriginalWritOrders;
        }
    }



}
