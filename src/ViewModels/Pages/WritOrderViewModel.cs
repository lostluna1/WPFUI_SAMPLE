using System.Collections.ObjectModel;
using WPFUI_SAMPLE.Contracts.Services;
using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.ViewModels.Pages;
public partial class WritOrderViewModel : ViewModel
{
    public readonly IWritOrderService _writOrderService;

    [ObservableProperty]
    public ObservableCollection<WritOrderEntity> writOrders = [];

    [ObservableProperty]
    public WritOrderEntity selectedWritOrder = new();
    public WritOrderViewModel(IWritOrderService writOrderService)
    {
        _writOrderService = writOrderService;
        _ = InitializeViewModel();
    }

    public async Task InitializeViewModel()
    {
        ObservableCollection<WritOrderEntity> writOrders = await _writOrderService.GetAllWritOrder();
        WritOrders = writOrders;
    }

    [RelayCommand]
    private async Task RefreshData()
    {
        //需要刷新的数据在这里调用对应方法
       await InitializeViewModel();
    }


    [RelayCommand]
    private void AddNewWritOrder()
    {
        WritOrderEntity newWritOrder = new()
        {
            Id = Guid.NewGuid(),
            WritOrderNo = string.Empty,
            BrandName = string.Empty,
            MaterialCode = string.Empty,
            Specification = string.Empty,
            ColourScheme = string.Empty,
            PlanQuantity = null,
            Line = string.Empty,
            OriginalPlanProductionDate = null,
            IsTrialProduction = false,
            Requirement = string.Empty,
            Customer = string.Empty,
            DeliveryDate = null,
            ActualProductName = string.Empty,
            ActualProductionSpecification = string.Empty,
            ActualProductionLine = string.Empty,
            DocumentDailyReport = string.Empty,
            ProgressReportData = string.Empty,
            ProductionDate = null,
            Difference = string.Empty,
            MaterialCode2 = string.Empty,
            InspectionQuantity = null,
            Remark = string.Empty
        };
        WritOrders.Add(newWritOrder);
        //SelectedWritOrder = newWritOrder;
    }

    [RelayCommand]
    private async Task SaveWritOrder()
    {
        await _writOrderService.AddOrUpdateWritOrder(WritOrders);
    }

    [RelayCommand]
    private async Task DeleteWritOrder()
    {
        if (SelectedWritOrder != null)
        {
            await _writOrderService.DeleteWritOrder(SelectedWritOrder.Id);
            WritOrders.Remove(SelectedWritOrder);
        }
    }

    [RelayCommand]
    private async Task UpdateWritOrder()
    {
        if (SelectedWritOrder != null)
        {
            await _writOrderService.UpdateWritOrder(SelectedWritOrder);
            int index = WritOrders.IndexOf(SelectedWritOrder);
            WritOrders[index] = SelectedWritOrder;
        }
    }
}
