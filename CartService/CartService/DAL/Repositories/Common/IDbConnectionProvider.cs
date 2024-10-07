using LiteDB;

namespace CartService.DAL.Repositories.Common
{
    public interface IDbConnectionProvider
    {
        public ILiteDatabase GetConnection();
    }
}