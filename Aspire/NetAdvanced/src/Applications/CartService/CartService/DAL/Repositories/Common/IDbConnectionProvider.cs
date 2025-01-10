using LiteDB.Async;

namespace CartService.DAL.Repositories.Common
{
    public interface IDbConnectionProvider
    {
        public ILiteDatabaseAsync GetConnection();
    }
}