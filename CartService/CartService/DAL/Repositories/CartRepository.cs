using CartService.Common.Entities;
using CartService.DAL.Repositories.Common;

namespace CartService.DAL.Repositories
{
    // TODO: Rework this 100%
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(IDbConnectionProvider connectionProvider)
            : base(connectionProvider) {}

        public override async Task<IEnumerable<Cart>> GetAllEntitiesAsync()
        {
            var connection = OpenConnection();
            var cartsCollection = connection.GetCollection<Cart>();

            var carts = await cartsCollection.FindAllAsync();
            return carts;
        }

        public override async Task<Cart> GetEntityAsync(string id)
        {
            var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            var cart = await carts.FindOneAsync(cart => cart.Id == id);
            return cart;
        }

        public override async Task AddEntityAsync(Cart newEntity)
        {
            var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            await carts.InsertAsync(newEntity);
        }

        public override async Task UpdateEntityAsync(string entityId, Cart updatedEntity)
        {
            var connection = OpenConnection();
            var carts = connection.GetCollection<Cart>();

            var cartToUpdate = await carts.FindOneAsync(cart => cart.Id == entityId);
            await carts.UpdateAsync(cartToUpdate.Id, updatedEntity);
        }
    }
}
