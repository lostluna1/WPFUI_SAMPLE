using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Services;

public interface IOrderService 
{
    /// <summary>
    /// ��ȡ���ж���
    /// </summary>
    /// <returns></returns>
    Task<List<OrderEntity>> GetAllOrdersAsync();

    /// <summary>
    /// ����Id��ȡ����
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<OrderEntity?> GetOrderByIdAsync(Guid id);
    /// <summary>
    /// ��Ӷ���
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task AddOrderAsync(OrderEntity order);

    /// <summary>
    /// ���¶���
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    Task UpdateOrderAsync(OrderEntity order);

    /// <summary>
    /// ɾ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteOrderAsync(Guid id);
}
