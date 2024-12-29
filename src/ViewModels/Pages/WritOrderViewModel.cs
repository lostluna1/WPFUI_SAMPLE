using System.Collections.ObjectModel;
using Wpf.Ui;
using WPFUI_SAMPLE.Contracts.Services;
using WPFUI_SAMPLE.Entity;
using WPFUI_SAMPLE.Helpers;
using WPFUI_SAMPLE.Services;
using WPFUI_SAMPLE.Views.Pages;
using WPFUI_SAMPLE.Views.Pages.SamplePage;

namespace WPFUI_SAMPLE.ViewModels.Pages;
public partial class WritOrderViewModel : ViewModel
{
    public readonly IWritOrderService _writOrderService;
    public readonly IFreeSql _fs;
    public readonly IContentDialogService _contentDialogService;

    [ObservableProperty]
    private ObservableCollection<WritOrderEntity> writOrders = new();
    
    [ObservableProperty]
    private ObservableCollection<WritOrderEntity> originalWritOrders = new();

    [ObservableProperty]
    private ObservableCollection<string> writOrderNos = new();

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

    private async Task InitDicData()
    {
        _fs.CodeFirst.SyncStructure<BrandEntity>();
        _fs.CodeFirst.SyncStructure<MaterialEntity>();
        _fs.CodeFirst.SyncStructure<ColourSchemeEntity>();
        _fs.CodeFirst.SyncStructure<LineEntity>();

        // 判断是否已经初始化数据，这里为了演示图省事，实际应当新建页面去配置创建数据，如果要名称也不重复，需要将其设为主键，这里为了演示，采用的代码方式判断重复
        #region 初始化字典数据
        // 插入Brand数据
        List<BrandEntity> newBrands = new()
        {
            new BrandEntity { Id = Guid.NewGuid(), Name = "品牌1" },
            new BrandEntity { Id = Guid.NewGuid(), Name = "品牌2" },
            new BrandEntity { Id = Guid.NewGuid(), Name = "品牌3" }
        };
        foreach (BrandEntity brand in newBrands)
        {
            if (!await _fs.Select<BrandEntity>().AnyAsync(b => b.Name == brand.Name))
            {
                await _fs.Insert(brand).ExecuteAffrowsAsync();
            }
        }

        // 插入Material数据
        List<MaterialEntity> newMaterials = new()
        {
            new MaterialEntity { Id = Guid.NewGuid(), Code = "物料编码1" },
            new MaterialEntity { Id = Guid.NewGuid(), Code = "物料编码2" },
            new MaterialEntity { Id = Guid.NewGuid(), Code = "物料编码3" }
        };
        foreach (MaterialEntity material in newMaterials)
        {
            if (!await _fs.Select<MaterialEntity>().AnyAsync(m => m.Code == material.Code))
            {
                await _fs.Insert(material).ExecuteAffrowsAsync();
            }
        }

        // 插入ColourScheme数据
        List<ColourSchemeEntity> newColourSchemes = new()
        {
            new ColourSchemeEntity { Id = Guid.NewGuid(), Name = "色系1" },
            new ColourSchemeEntity { Id = Guid.NewGuid(), Name = "色系2" },
            new ColourSchemeEntity { Id = Guid.NewGuid(), Name = "色系3" }
        };
        foreach (ColourSchemeEntity colourScheme in newColourSchemes)
        {
            if (!await _fs.Select<ColourSchemeEntity>().AnyAsync(c => c.Name == colourScheme.Name))
            {
                await _fs.Insert(colourScheme).ExecuteAffrowsAsync();
            }
        }

        // 插入Line数据
        List<LineEntity> newLines = new()
        {
            new LineEntity { Id = Guid.NewGuid(), Name = "线别1" },
            new LineEntity { Id = Guid.NewGuid(), Name = "线别2" },
            new LineEntity { Id = Guid.NewGuid(), Name = "线别3" }
        };
        foreach (LineEntity line in newLines)
        {
            if (!await _fs.Select<LineEntity>().AnyAsync(l => l.Name == line.Name))
            {
                await _fs.Insert(line).ExecuteAffrowsAsync();
            }
        }
        #endregion

        // 初始化Brand可观察集合
        Brands = new ObservableCollection<BrandEntity>(await _fs.Select<BrandEntity>().ToListAsync());
        // 初始化Material可观察集合
        Materials = new ObservableCollection<MaterialEntity>(await _fs.Select<MaterialEntity>().ToListAsync());
        // 初始化ColourScheme可观察集合
        ColourSchemes = new ObservableCollection<ColourSchemeEntity>(await _fs.Select<ColourSchemeEntity>().ToListAsync());
        // 初始化Line可观察集合
        Lines = new ObservableCollection<LineEntity>(await _fs.Select<LineEntity>().ToListAsync());
    }

    public async Task InitializeViewModel()
    {
        await InitDicData();
        // 初始化制令单数据
        WritOrders = await _writOrderService.GetAllWritOrder();
        OriginalWritOrders = await _writOrderService.GetAllWritOrder();
        WritOrderNos = new ObservableCollection<string>( WritOrders?.Select(w => w.WritOrderNo));
    }


    [RelayCommand]
    private async Task RefreshData()
    {
        /*var navigationService = App.GetRequiredService<INavigationService>();
        navigationService.Navigate(typeof(WritOrderPage));*/
        await Task.Run(async () =>
        {
            await InitializeViewModel();
        });

/*        // 更新 UI
        OnPropertyChanged(nameof(WritOrders));
        OnPropertyChanged(nameof(OriginalWritOrders));
        OnPropertyChanged(nameof(WritOrderNos));*/
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

        foreach (WritOrderEntity item in WritOrders)
        {
            // 校验集合中的每个对象
            if (!await this.ValidateObjectWithDialogAsync(_contentDialogService, item))
            {
                return;
            }
        }
        await Task.Run(async () =>
        {
            await _writOrderService.AddOrUpdateWritOrder(WritOrders);
        });
        
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
