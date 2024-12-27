using System.ComponentModel.DataAnnotations;
using TableAttribute = FreeSql.DataAnnotations.TableAttribute;

namespace WPFUI_SAMPLE.Entity;

// 这里是实体类,实体类是用来映射数据库表的,每个实体类对应一个数据库表
// 使用实体类同步数据表的方式叫做CodeFirst,即先有代码,后有表,这种方式适合新项目,如果是先有表,后有代码,则是DBFirst,即先有表,后有代码,这种方式适合老项目
// 为字段添加文档注释则会生成数据库表的注释


/// <summary>
/// 订单
/// </summary>
[Table(Name = "ORDER")]// 指定表名,默认是类名
public partial class OrderEntity: ObservableValidator
{
    // 字段如果是命名为Id,并且是int类型,则可以不用标记[Column(IsPrimary = true)],默认是主键,主键会默认加索引

    // [Column(IsPrimary = true)]// 指定主键
    public Guid Id
    {
        get; set;
    }

    /// <summary>
    /// 订单编号
    /// </summary>
    [Required(ErrorMessage ="不能为空")]
    [ObservableProperty]
    // 你可以像下面这样手动指定列名,默认是属性名,不过与[ObservableProperty]标记冲突,如果字段需要通知视图更新,则不能使用源生成器的情况下手动指定列名
    //[Column(Name = "ORDER_NO")]
    public string? orderNo;

    /// <summary>
    /// 订单名称
    /// </summary>
    [Required(ErrorMessage ="不能为空")]
    [ObservableProperty]
    public string? orderName;

    /// <summary>
    /// 订单类型
    /// </summary>
    [ObservableProperty]
    public string? orderType;

    /// <summary>
    /// 订单状态
    /// </summary>
    [ObservableProperty]
    public string? orderStatus;

    /// <summary>
    /// 订单备注
    /// </summary>
    [ObservableProperty]
    public string? orderRemark;
}
