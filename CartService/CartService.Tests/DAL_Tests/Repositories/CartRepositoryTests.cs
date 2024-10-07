using CartService.Common.Entities;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using FluentAssertions;
using LiteDB;
using NSubstitute;

namespace CartService.Tests.DAL_Tests.Repositories
{
    // Example of Integration Tests
    public class CartRepositoryTests
    {
        private readonly string DbFilePath = "./TestData.db";

        private ILiteDatabase _databaseConnection => new LiteDatabase(DbFilePath);

        [Test]
        public void GetCartItems_Works()
        {
            var item = new ProductItem(1, "name 1", 1, 1);
            var expectedCart = new Cart() { Id = 1, Items = [item] };
            InitializeTestDatabase(expectedCart);

            var dbConnectionProvider = Substitute.For<IDbConnectionProvider>();
            dbConnectionProvider.GetConnection().Returns(_ => _databaseConnection);

            var cartRepository = new CartRepository(dbConnectionProvider);

            // Act
            var actual = cartRepository.GetEntity(1);

            // Assert
            actual.Should().BeEquivalentTo(expectedCart);
        }

        private void InitializeTestDatabase(Cart cart)
        {
            using var db = _databaseConnection;
            var carts = db.GetCollection<Cart>();
            carts.DeleteAll();

            carts.Insert(cart);
        }
    }
}
