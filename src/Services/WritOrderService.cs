using System.Collections.ObjectModel;
using System.Diagnostics;
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
        _fs.CodeFirst.SyncStructure<WritOrderEntity>(); // 同步实体类的结构到数据库,生产环境不要用,会覆盖表结构
        _fs.CodeFirst.IsSyncStructureToUpper = true; // 同步结构时,表名和字段名是否转为大写,默认是false
    }

    public async Task AddOrUpdateWritOrder(ObservableCollection<WritOrderEntity> writOrderEntities)
    {
        foreach (var writOrder in writOrderEntities)
        {
            await EnsureNavigationProperties(writOrder);
        }
        await _fs.InsertOrUpdate<WritOrderEntity>().SetSource(writOrderEntities).ExecuteAffrowsAsync();
    }

    public async Task DeleteWritOrder(Guid id)
    {
        await _fs.Delete<WritOrderEntity>().Where(o => o.Id == id).ExecuteAffrowsAsync();
    }

    public async Task<ObservableCollection<WritOrderEntity>> GetAllWritOrder()
    {
        // Include方法用于关联查询，可以将关联的实体类一起查询出来
        // 比如要打印制令单的品牌信息，可以Console.WriteLine( result.Include(o => o.Brand))
        var result = await _fs.Select<WritOrderEntity>()
            .Include(o => o.Brand)
            .Include(o => o.Material)
            .Include(o => o.ColourScheme)
            .Include(o => o.Line)
            .ToListAsync();
        foreach (var item in result)
        {
            Debug.WriteLine(item.Brand?.Name);
        }
        return new ObservableCollection<WritOrderEntity>(result);
    }

    public async Task<WritOrderEntity?> GetWritOrderById(Guid id)
    {
        return await _fs.Select<WritOrderEntity>()
            .Include(o => o.Brand)
            .Include(o => o.Material)
            .Include(o => o.ColourScheme)
            .Include(o => o.Line)
            .Where(o => o.Id == id)
            .FirstAsync();
    }

    public async Task UpdateWritOrder(WritOrderEntity writOrder)
    {
        await EnsureNavigationProperties(writOrder);
        await _fs.Update<WritOrderEntity>().SetSource(writOrder).ExecuteAffrowsAsync();
    }

    private async Task EnsureNavigationProperties(WritOrderEntity writOrder)
    {
        if (writOrder.BrandId != Guid.Empty)
        {
            writOrder.Brand = await _fs.Select<BrandEntity>().Where(b => b.Id == writOrder.BrandId).FirstAsync();
        }

        if (writOrder.MaterialId != Guid.Empty)
        {
            writOrder.Material = await _fs.Select<MaterialEntity>().Where(m => m.Id == writOrder.MaterialId).FirstAsync();
        }

        if (writOrder.ColourSchemeId != Guid.Empty)
        {
            writOrder.ColourScheme = await _fs.Select<ColourSchemeEntity>().Where(c => c.Id == writOrder.ColourSchemeId).FirstAsync();
        }

        if (writOrder.LineId != Guid.Empty)
        {
            writOrder.Line = await _fs.Select<LineEntity>().Where(l => l.Id == writOrder.LineId).FirstAsync();
        }
    }
}



