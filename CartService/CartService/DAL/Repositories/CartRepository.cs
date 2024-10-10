using CartService.Common.Entities;
using CartService.DAL.Repositories.Common;

namespace CartService.DAL.Repositories
{
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(IDbConnectionProvider connectionProvider)
            : base(connectionProvider) {}

        public override async Task<Cart> GetEntityAsync(int id)
        {
            using var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            var cart = await carts.FindOneAsync(cart => cart.Id == id);
            return cart;
        }

        public override async Task AddEntityAsync(Cart newEntity)
        {
            using var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            await carts.InsertAsync(newEntity);
        }

        public override async Task UpdateEntityAsync(int entityToUpdateId, Cart updatedEntity)
        {
            using var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            var cartToUpdate = await carts.FindOneAsync(cart => cart.Id == cart.Id);
            await carts.UpdateAsync(cartToUpdate.Id, updatedEntity);
        }
    }
}
