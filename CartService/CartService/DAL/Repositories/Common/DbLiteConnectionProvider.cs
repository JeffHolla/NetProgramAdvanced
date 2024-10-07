using LiteDB;

namespace CartService.DAL.Repositories.Common
{
    public class DbLiteConnectionProvider : IDbConnectionProvider
    {
        public required string ConnectionString { get; set; }
        
        public ILiteDatabase GetConnection()
        {
            return new LiteDatabase(ConnectionString);
        }
    }
}
