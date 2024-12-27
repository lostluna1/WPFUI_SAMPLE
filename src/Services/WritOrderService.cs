using System.Collections.ObjectModel;
using WPFUI_SAMPLE.Contracts.Services;
using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Services;

// 继承某个接口时，如果没有实现接口的方法，可以把光标移到IWritOrderService使用快捷键Alt+Enter，选择实现接口的方法
public class WritOrderService : IWritOrderService
{
    private readonly IFreeSql _fs;

    public WritOrderService(IFreeSql freeSql)
    {
        _fs = freeSql;
        _fs.CodeFirst.SyncStructure<WritOrderEntity>();// 同步实体类的结构到数据库,生产环境不要用,会覆盖表结构
        _fs.CodeFirst.IsSyncStructureToUpper = true;// 同步结构时,表名和字段名是否转为大写,默认是false
    }
    public async Task AddOrUpdateWritOrder(ObservableCollection<WritOrderEntity> writOrderEntities)
    {
       await _fs.InsertOrUpdate<WritOrderEntity>().SetSource(writOrderEntities).ExecuteAffrowsAsync();
    }

    public async Task DeleteWritOrder(Guid id)
    {
        await _fs.Delete<WritOrderEntity>().Where(o => o.Id == id).ExecuteAffrowsAsync();
    }

    public async Task<ObservableCollection<WritOrderEntity>> GetAllWritOrder()
    {
        var result = await _fs.Select<WritOrderEntity>().ToListAsync();
        return new ObservableCollection<WritOrderEntity>(result);
    }

    public async Task<WritOrderEntity?> GetWritOrderById(Guid id)
    {
        return await _fs.Select<WritOrderEntity>().Where(o => o.Id == id).FirstAsync();
    }

    public async Task UpdateWritOrder(WritOrderEntity writOrder)
    {
        await _fs.Update<WritOrderEntity>().SetSource(writOrder).ExecuteAffrowsAsync();
    }
}
