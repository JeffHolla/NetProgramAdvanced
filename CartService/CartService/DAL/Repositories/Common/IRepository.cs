namespace CartService.DAL.Repositories.Common;

public interface IRepository<T>
{
    Task<T> GetEntityAsync(int id);
    Task AddEntityAsync(T newEntity);
    Task UpdateEntityAsync(int entityId, T updatedEntity);
}
