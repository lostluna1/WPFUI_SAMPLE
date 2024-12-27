using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Services;

public interface IOrderService
{
    Task<List<OrderEntity>> GetAllOrdersAsync();
    Task<OrderEntity?> GetOrderByIdAsync(Guid id);
    Task AddOrderAsync(OrderEntity order);
    Task UpdateOrderAsync(OrderEntity order);
    Task DeleteOrderAsync(Guid id);
}
