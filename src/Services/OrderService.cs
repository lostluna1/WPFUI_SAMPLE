using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Services;

public class OrderService : IOrderService
{
    private readonly IFreeSql _fs;

    public OrderService(IFreeSql freeSql)
    {
        _fs = freeSql;

        _fs.CodeFirst.SyncStructure<OrderEntity>();// ͬ��ʵ����Ľṹ�����ݿ�,����������Ҫ��,�Ḳ�Ǳ�ṹ
        _fs.CodeFirst.IsSyncStructureToUpper = true;// ͬ���ṹʱ,�������ֶ����Ƿ�תΪ��д,Ĭ����false
    }

    public async Task<List<OrderEntity>> GetAllOrdersAsync()
    {
        return await _fs.Select<OrderEntity>().ToListAsync();
    }

    public async Task<OrderEntity?> GetOrderByIdAsync(Guid id)
    {
        return await _fs.Select<OrderEntity>().Where(o => o.Id == id).FirstAsync();
    }

    public async Task AddOrderAsync(OrderEntity order)
    {
        await _fs.Insert(order).ExecuteAffrowsAsync();
    }

    public async Task UpdateOrderAsync(OrderEntity order)
    {
        await _fs.Update<OrderEntity>().SetSource(order).ExecuteAffrowsAsync();
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        await _fs.Delete<OrderEntity>().Where(o => o.Id == id).ExecuteAffrowsAsync();
    }
}
