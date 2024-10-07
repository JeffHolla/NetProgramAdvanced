using LiteDB;

namespace CartService.DAL.Repositories.Common;

// Maybe makes sense to use not the Repository pattern, but a set of DbSet like in EF
// We can use set of Collection classes from LiteDatabase
public abstract class BaseRepository<T> : IRepository<T>
{
    private readonly IDbConnectionProvider _connectionProvider;

    protected BaseRepository(IDbConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public abstract T GetEntity(int id);

    public abstract void AddEntity(T newEntity);

    public abstract void UpdateEntity(int entityToUpdateId, T updatedEntity);
    
    protected ILiteDatabase OpenConnection()
    {
        return _connectionProvider.GetConnection();
    }
}
