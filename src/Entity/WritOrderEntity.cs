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
    [Required(ErrorMessage = "不能为空")]
    [ObservableProperty]
    public string? writOrderNo;

    /// <summary>
    /// 品名
    /// </summary>
    [Required(ErrorMessage = "不能为空")]
    [ObservableProperty]
    public string? brandName;

    /// <summary>
    /// 物料编码
    /// </summary>
    [ObservableProperty]
    public string? materialCode;

    /// <summary>
    /// 规格(W*H*L)
    /// </summary>
    [ObservableProperty]
    public string? specification;

    /// <summary>
    /// 色系
    /// </summary>
    [ObservableProperty]
    public string? colourScheme;


    /*计划数量 线别  原计划生产日期 是否算试产   要求 客户  发货日期 实际生产品名  实际生产规格 实际生产线别  文档日报表 进度报表数据  生产日期 差异  物料编码2 送检数量    备注*/
    /// <summary>
    /// 计划数量
    /// </summary>
    /// 
    [ObservableProperty]
    public int? planQuantity;

    /// <summary>
    /// 线别
    /// </summary>
    /// 
    [ObservableProperty]
    public string? line;

    /// <summary>
    /// 原计划生产日期
    /// </summary>
    /// 
    [ObservableProperty]
    public DateTime? originalPlanProductionDate;

    /// <summary>
    /// 是否算试产
    /// </summary>
    /// 
    [ObservableProperty]
    public bool? isTrialProduction;

    /// <summary>
    /// 要求
    /// </summary>
    /// 
    [ObservableProperty]
    public string? requirement;

    /// <summary>
    /// 客户
    /// </summary>
    /// 
    [ObservableProperty]
    public string? customer;

    /// <summary>
    /// 发货日期
    /// </summary>
    /// 
    [ObservableProperty]
    public DateTime? deliveryDate;

    /// <summary>
    /// 实际生产品名
    /// </summary>
    /// 
    [ObservableProperty]
    public string? actualProductName;

    /// <summary>
    /// 实际生产规格
    /// </summary>
    /// 
    [ObservableProperty]
    public string? actualProductionSpecification;

    /// <summary>
    /// 实际生产线别
    /// </summary>
    /// 
    [ObservableProperty]
    public string? actualProductionLine;

    /// <summary>
    /// 文档日报表
    /// </summary>
    /// 
    [ObservableProperty]
    public string? documentDailyReport;

    /// <summary>
    /// 进度报表数据
    /// </summary>
    /// 
    [ObservableProperty]
    public string? progressReportData;

    /// <summary>
    /// 生产日期
    /// </summary>
    /// 
    [ObservableProperty]
    public DateTime? productionDate;

    /// <summary>
    /// 差异  
    /// </summary>
    /// 
    [ObservableProperty]
    public string? difference;

    /// <summary>
    /// 物料编码2
    /// </summary>
    ///
    [ObservableProperty]
    public string? materialCode2;

    /// <summary>
    /// 送检数量
    /// </summary>
    /// 
    [ObservableProperty]
    public int? inspectionQuantity;

    /// <summary>
    /// 备注
    /// </summary>
    /// 
    [ObservableProperty]
    public string? remark;




}
