namespace CartService.DAL.Repositories.Common;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllEntitiesAsync();
    Task<T> GetEntityAsync(string id);
    Task AddEntityAsync(T newEntity);
    Task UpdateEntityAsync(string entityId, T updatedEntity);
}
