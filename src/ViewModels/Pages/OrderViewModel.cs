using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using WPFUI_SAMPLE.Entity;
using WPFUI_SAMPLE.Services;

namespace WPFUI_SAMPLE.ViewModels.Pages;
public partial class OrderViewModel : ViewModel
{
    private readonly IOrderService _orderService;
    private bool _isInitialized = false;

    [ObservableProperty]
    private ObservableCollection<OrderEntity> _orders;

    [ObservableProperty]
    private OrderEntity _selectedOrder;

    public OrderViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        Orders = new ObservableCollection<OrderEntity>();
        SelectedOrder = new OrderEntity();
        InitializeViewModel();
    }

    /*public void OnNavigatedTo()
    {
        if(!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    public void OnNavigatedFrom()
    {
    }*/

    private async void InitializeViewModel()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        Orders = new ObservableCollection<OrderEntity>(orders);
        _isInitialized = true;
    }

    [RelayCommand]
    // 但凡是RelayCommand,在xaml中绑定Command时,名称都会自动加上Command结尾,这是MvvmToolkitCommunity的源生成器特性,比如这里是AddOrderCommand
    private async Task AddOrder()
    {
        var newOrder = new OrderEntity
        {
            Id = Guid.NewGuid(),
            OrderNo = "NewOrderNo",
            OrderName = "NewOrderName",
            OrderType = "NewOrderType",
            OrderStatus = "NewOrderStatus",
            OrderRemark = "NewOrderRemark"
        };
        await _orderService.AddOrderAsync(newOrder);
        Orders.Add(newOrder);
    }

    [RelayCommand]
    private async Task UpdateOrder()
    {
        if(SelectedOrder != null)
        {
            SelectedOrder.OrderName = "UpdatedOrderName";
            await _orderService.UpdateOrderAsync(SelectedOrder);
            var index = Orders.IndexOf(SelectedOrder);
            Orders[index] = SelectedOrder;
        }
    }

    [RelayCommand]
    private async Task DeleteOrder()
    {
        if(SelectedOrder != null)
        {
            await _orderService.DeleteOrderAsync(SelectedOrder.Id);
            Orders.Remove(SelectedOrder);
        }
    }
}
