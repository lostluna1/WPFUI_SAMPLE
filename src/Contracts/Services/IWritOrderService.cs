using System.Collections.ObjectModel;
using WPFUI_SAMPLE.Entity;

namespace WPFUI_SAMPLE.Contracts.Services;
public interface IWritOrderService
{
    Task<ObservableCollection<WritOrderEntity>> GetAllWritOrder();
    Task<WritOrderEntity?> GetWritOrderById(Guid id);
    Task AddOrUpdateWritOrder(ObservableCollection<WritOrderEntity> writOrderEntities);
    Task UpdateWritOrder(WritOrderEntity writOrder);
    Task DeleteWritOrder(Guid id);
}
