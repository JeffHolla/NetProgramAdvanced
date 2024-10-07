namespace CartService.DAL.Repositories.Common
{
    public interface IRepository<T>
    {
        T GetEntity(int id);
        void AddEntity(T newEntity);
        void UpdateEntity(int entityId, T updatedEntity);
    }
}
