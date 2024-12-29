using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Services;

public interface IOrderService 
{
    /// <summary>
    /// 获取所有订单
    /// </summary>
    /// <returns></returns>
    Task<List<OrderEntity>> GetAllOrdersAsync();

    /// <summary>
    /// 根据Id获取订单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<OrderEntity?> GetOrderByIdAsync(Guid id);
    /// <summary>
    /// 添加订单
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task AddOrderAsync(OrderEntity order);

    /// <summary>
    /// 更新订单
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task UpdateOrderAsync(OrderEntity order);

    /// <summary>
    /// 删除订单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteOrderAsync(Guid id);
}
