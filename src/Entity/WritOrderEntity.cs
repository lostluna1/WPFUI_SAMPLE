using System.ComponentModel.DataAnnotations;

namespace WPFUI_SAMPLE.Entity;

/// <summary>
/// 制令单
/// </summary>
[Table(Name = "WRIT_ORDER")]
public partial class WritOrderEntity : ObservableValidator
{


    // [Column(IsPrimary = true)]
    public Guid Id
    {
        get; set;
    }

    /// <summary>
    /// 制令单编号
    /// </summary>
    [Required(ErrorMessage = "制令单编号不能为空")]
    [ObservableProperty]
    private string? writOrderNo;

    /// <summary>
    /// 品名
    /// </summary>
    [NotEmptyGuid(ErrorMessage = "品名不能为空")]
    [ObservableProperty]
    private Guid brandId;


    [Navigate(nameof(BrandId))]
    public BrandEntity? Brand
    {
        get; set;
    }

    /// <summary>
    /// 物料编码
    /// </summary>
    [ObservableProperty]
    private Guid materialId;

    [Navigate(nameof(MaterialId))]
    [ObservableProperty]
    public MaterialEntity? material;

    /// <summary>
    /// 规格(W*H*L)
    /// </summary>
    [ObservableProperty]
    private string? specification;

    /// <summary>
    /// 色系
    /// </summary>
    [ObservableProperty]
    private Guid colourSchemeId;

    [Navigate(nameof(ColourSchemeId))]
    public ColourSchemeEntity? ColourScheme
    {
        get; set;
    }

    /// <summary>
    /// 计划数量
    /// </summary>
    [ObservableProperty]
    private int? planQuantity;

    /// <summary>
    /// 线别
    /// </summary>
    [ObservableProperty]
    private Guid lineId;

    [Navigate(nameof(LineId))]
    public LineEntity? Line
    {
        get; set;
    }

    /// <summary>
    /// 原计划生产日期
    /// </summary>
    [ObservableProperty]
    private DateTime? originalPlanProductionDate;

    /// <summary>
    /// 是否算试产
    /// </summary>
    [ObservableProperty]
    private bool? isTrialProduction;

    /// <summary>
    /// 要求
    /// </summary>
    [ObservableProperty]
    private string? requirement;

    /// <summary>
    /// 客户
    /// </summary>
    [ObservableProperty]
    private string? customer;

    /// <summary>
    /// 发货日期
    /// </summary>
    [ObservableProperty]
    private DateTime? deliveryDate;

    /// <summary>
    /// 实际生产品名
    /// </summary>
    [ObservableProperty]
    private Guid actuallyBrandId;

    [Navigate(nameof(ActuallyBrandId))]
    public BrandEntity? ActualBrand
    {
        get; set;
    }

    /// <summary>
    /// 实际生产规格
    /// </summary>
    [ObservableProperty]
    private string? actualProductionSpecification;

    /// <summary>
    /// 实际生产线别
    /// </summary>
    [ObservableProperty]
    private Guid actualLineId;

    [Navigate(nameof(ActualLineId))]
    public LineEntity? ActualLine
    {
        get; set;
    }

    /// <summary>
    /// 文档日报表
    /// </summary>
    [ObservableProperty]
    private string? documentDailyReport;

    /// <summary>
    /// 进度报表数据
    /// </summary>
    [ObservableProperty]
    private string? progressReportData;

    /// <summary>
    /// 生产日期
    /// </summary>
    [ObservableProperty]
    private DateTime? productionDate;

    /// <summary>
    /// 差异  
    /// </summary>
    [ObservableProperty]
    private string? difference;

    /// <summary>
    /// 物料编码2
    /// </summary>
    [ObservableProperty]
    private string? materialCode2;

    /// <summary>
    /// 送检数量
    /// </summary>
    [ObservableProperty]
    private int? inspectionQuantity;

    /// <summary>
    /// 备注
    /// </summary>
    [ObservableProperty]
    private string? remark;
}
