using LiteDB.Async;

namespace CartService.DAL.Repositories.Common
{
    public class DbLiteConnectionProvider : IDbConnectionProvider
    {
        public required string ConnectionString { get; set; }

        private ILiteDatabaseAsync _connection;
        public ILiteDatabaseAsync GetConnection()
        { 
            _connection ??= new LiteDatabaseAsync(ConnectionString);
            return _connection;
        }
    }
}
