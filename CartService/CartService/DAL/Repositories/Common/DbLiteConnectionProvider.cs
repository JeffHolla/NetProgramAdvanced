using LiteDB.Async;

namespace CartService.DAL.Repositories.Common
{
    public class DbLiteConnectionProvider : IDbConnectionProvider
    {
        public required string ConnectionString { get; set; }

        public ILiteDatabaseAsync GetConnection() 
            => new LiteDatabaseAsync(ConnectionString);
    }
}
