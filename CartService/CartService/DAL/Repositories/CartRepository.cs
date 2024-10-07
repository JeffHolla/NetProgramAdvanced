using CartService.Common.Entities;
using CartService.DAL.Repositories.Common;

namespace CartService.DAL.Repositories
{
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(IDbConnectionProvider connectionProvider)
            : base(connectionProvider) {}

        public override Cart GetEntity(int id)
        {
            using var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            var cart = carts.FindOne(cart => cart.Id == id);
            return cart;
        }

        public override void AddEntity(Cart newEntity)
        {
            using var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            carts.Insert(newEntity);
        }

        public override void UpdateEntity(int entityToUpdateId, Cart updatedEntity)
        {
            using var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            var cartToUpdate = carts.FindOne(cart => cart.Id == cart.Id);
            var result = carts.Update(cartToUpdate.Id, updatedEntity);
        }
    }
}
