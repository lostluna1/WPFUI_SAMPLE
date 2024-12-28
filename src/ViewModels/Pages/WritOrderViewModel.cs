using System.Collections.ObjectModel;
using Wpf.Ui;
using WPFUI_SAMPLE.Contracts.Services;
using WPFUI_SAMPLE.Entity;
using WPFUI_SAMPLE.Helpers;

namespace WPFUI_SAMPLE.ViewModels.Pages;
public partial class WritOrderViewModel : ViewModel
{
    public readonly IWritOrderService _writOrderService;
    public readonly IFreeSql _fs;
    public readonly IContentDialogService _contentDialogService;
    [ObservableProperty]
    private ObservableCollection<WritOrderEntity> writOrders = new();

    [ObservableProperty]
    private WritOrderEntity selectedWritOrder = new();

    [ObservableProperty]
    private ObservableCollection<BrandEntity> brands = new();

    [ObservableProperty]
    private ObservableCollection<MaterialEntity> materials = new();

    [ObservableProperty]
    private ObservableCollection<ColourSchemeEntity> colourSchemes = new();

    [ObservableProperty]
    private ObservableCollection<LineEntity> lines = new();

    [ObservableProperty]
    private ObservableCollection<LineEntity> actualLines = new();

    public WritOrderViewModel(IWritOrderService writOrderService, IFreeSql fs,
        IContentDialogService contentDialogService)
    {
        _writOrderService = writOrderService;
        _fs = fs;
        _contentDialogService = contentDialogService;
        _ = InitializeViewModel();
    }

    public async Task InitializeViewModel()
    {
        // 初始化制令单数据
        WritOrders = await _writOrderService.GetAllWritOrder();

        // 初始化Brand数据
        Brands = new ObservableCollection<BrandEntity>(await _fs.Select<BrandEntity>().ToListAsync());

        // 初始化Material数据
        Materials = new ObservableCollection<MaterialEntity>(await _fs.Select<MaterialEntity>().ToListAsync());

        // 初始化ColourScheme数据
        ColourSchemes = new ObservableCollection<ColourSchemeEntity>(await _fs.Select<ColourSchemeEntity>().ToListAsync());

        // 初始化Line数据
        Lines = new ObservableCollection<LineEntity>(await _fs.Select<LineEntity>().ToListAsync());

    }

    [RelayCommand]
    private async Task RefreshData()
    {
        // 需要刷新的数据在这里调用对应方法
        await InitializeViewModel();
    }

    [RelayCommand]
    private void AddNewWritOrder()
    {
        WritOrderEntity newWritOrder = new()
        {
            Id = Guid.NewGuid(),
            WritOrderNo = string.Empty,
            BrandId = Guid.Empty,
            MaterialId = Guid.Empty,
            Specification = string.Empty,
            ColourSchemeId = Guid.Empty,
            PlanQuantity = null,
            LineId = Guid.Empty,
            OriginalPlanProductionDate = null,
            IsTrialProduction = false,
            Requirement = string.Empty,
            Customer = string.Empty,
            DeliveryDate = null,
            ActuallyBrandId = Guid.Empty,
            ActualProductionSpecification = string.Empty,
            ActualLineId = Guid.Empty,
            DocumentDailyReport = string.Empty,
            ProgressReportData = string.Empty,
            ProductionDate = null,
            Difference = string.Empty,
            MaterialCode2 = string.Empty,
            InspectionQuantity = null,
            Remark = string.Empty
        };
        WritOrders.Add(newWritOrder);
    }

    [RelayCommand]
    public async Task SaveWritOrder()
    {

        foreach (var item in WritOrders)
        {
            // 校验集合中的每个对象
            if (!await this.ValidateObjectWithDialogAsync(_contentDialogService, item))
            {
                return;
            }
        }

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
            var index = WritOrders.IndexOf(SelectedWritOrder);
            WritOrders[index] = SelectedWritOrder;
        }
    }
}
