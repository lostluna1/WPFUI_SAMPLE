using Oracle.ManagedDataAccess.OpenTelemetry;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Wpf.Ui.Controls;

namespace WPFUI_SAMPLE.ViewModels.Windows;

public partial class MainWindowViewModel : ViewModel
{
    [ObservableProperty]
    private string _applicationTitle = "WPF UI - WPFUI_SAMPLE";

    [ObservableProperty]// Mvvm生成器生成之后为MenuItems,规则是去掉下划线并首字母大写,你也可以命名不带下划线,但首字母必须小写
    private ObservableCollection<object> _menuItems = [];

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems;


    public MainWindowViewModel()
    {
        MenuItems = LoadMenuItems("menuItems.json");
        FooterMenuItems = new ObservableCollection<object>
        {
            new NavigationViewItem
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };
    }

    private ObservableCollection<object> LoadMenuItems(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var items = JsonSerializer.Deserialize<List<MenuItemData>>(json);
            if (items == null)
            {
                return [];
            }
            foreach (var item in items)
            {
                item.Icon ??= "Home";// 为空则默认为Home
                var navigationItem = new NavigationViewItem
                {
                    Content = item.Content,
                    Icon = new SymbolIcon { Symbol = (SymbolRegular)Enum.Parse(typeof(SymbolRegular), item.Icon) },
                    TargetPageType = Type.GetType("WPFUI_SAMPLE." + item.TargetPageType),
                    NavigationCacheMode = NavigationCacheMode.Disabled
                };


                if (item.Children != null && item.Children.Count > 0)
                {
                    foreach (var child in item.Children)
                    {
                        child.Icon ??= "Home";// 为空则默认为Home
                        var childItem = new NavigationViewItem
                        {
                            Content = child.Content,
                            Icon = new SymbolIcon { Symbol = (SymbolRegular)Enum.Parse(typeof(SymbolRegular), child.Icon) },
                            TargetPageType = Type.GetType("WPFUI_SAMPLE." + child.TargetPageType)
                        };
                        navigationItem.MenuItems.Add(childItem);
                    }
                }

                MenuItems.Add(navigationItem);
            }
        }

        return MenuItems;
    }
}

public class MenuItemData
{
    public string? Content
    {
        get; set;
    }
    public string? Icon
    {
        get; set;
    }
    public string? TargetPageType
    {
        get; set;
    }
    public List<MenuItemData>? Children
    {
        get; set;
    }
}
