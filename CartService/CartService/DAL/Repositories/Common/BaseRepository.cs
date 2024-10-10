using LiteDB.Async;

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

    public abstract Task<T> GetEntityAsync(int id);

    public abstract Task AddEntityAsync(T newEntity);

    public abstract Task UpdateEntityAsync(int entityToUpdateId, T updatedEntity);

    protected ILiteDatabaseAsync OpenConnection() 
        => _connectionProvider.GetConnection();
}
