using System.ComponentModel.DataAnnotations;

namespace WPFUI_SAMPLE.Entity;

/// <summary>
/// 品牌
/// </summary>
[Table(Name = "BRAND")]
public class BrandEntity
{
    [Column(IsPrimary = true)]
    public Guid Id
    {
        get; set;
    }

    [Required(ErrorMessage = "不能为空")]
    public string? Name
    {
        get; set;
    }

    [Navigate(nameof(WritOrderEntity.BrandId))]
    public List<WritOrderEntity> WritOrders { get; set; } = new();

    [Navigate(nameof(WritOrderEntity.ActuallyBrandId))]
    public List<WritOrderEntity> ActuallyWritOrders { get; set; } = new();
}

/// <summary>
/// 物料
/// </summary>
[Table(Name = "MATERIAL")]
public partial class MaterialEntity : ObservableValidator
{
    [ObservableProperty]
    public Guid id;

    [Required(ErrorMessage = "不能为空")]
    [ObservableProperty]
    public string? code;

    [Navigate(nameof(WritOrderEntity.MaterialId))]
    public List<WritOrderEntity> WritOrders { get; set; } = new();
}

/// <summary>
/// 色系
/// </summary>
[Table(Name = "COLOUR_SCHEME")]
public class ColourSchemeEntity
{
    [Column(IsPrimary = true)]
    public Guid Id
    {
        get; set;
    }

    [Required(ErrorMessage = "不能为空")]
    public string? Name
    {
        get; set;
    }

    [Navigate(nameof(WritOrderEntity.ColourSchemeId))]
    public List<WritOrderEntity> WritOrders { get; set; } = new();
}

/// <summary>
/// 线别
/// </summary>
[Table(Name = "LINE")]
public class LineEntity
{
    [Column(IsPrimary = true)]
    public Guid Id
    {
        get; set;
    }

    [Required(ErrorMessage = "不能为空")]
    public string? Name
    {
        get; set;
    }

    [Navigate(nameof(WritOrderEntity.LineId))]
    public List<WritOrderEntity> WritOrders { get; set; } = new();

    [Navigate(nameof(WritOrderEntity.ActualLineId))]
    public List<WritOrderEntity> ActualWritOrders { get; set; } = new();
}
