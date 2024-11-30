using CartService.Common.Entities;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using FluentAssertions;
using LiteDB.Async;
using NSubstitute;

namespace CartService.Tests.DAL_Tests.Repositories
{
    // Example of Integration Tests
    public class CartRepositoryTests
    {
        private readonly string DbFilePath = "./TestData.db";

        private ILiteDatabaseAsync DatabaseConnection => new LiteDatabaseAsync(DbFilePath);

        [Test]
        public async Task GetCartItems_Works()
        {
            var item = new ProductItem(1, "name 1", 1, 1);
            var expectedCart = new Cart() { Id = "1", Items = [item] };
            await InitializeTestDatabaseAsync(expectedCart);

            var dbConnectionProvider = Substitute.For<IDbConnectionProvider>();
            dbConnectionProvider.GetConnection().Returns(_ => DatabaseConnection);

            var cartRepository = new CartRepository(dbConnectionProvider);

            // Act
            var actual = await cartRepository.GetEntityAsync("1");

            // Assert
            actual.Should().BeEquivalentTo(expectedCart);
        }

        private async Task InitializeTestDatabaseAsync(Cart cart)
        {
            using var db = DatabaseConnection;
            var carts = db.GetCollection<Cart>();
            await carts.DeleteAllAsync();

            await carts.InsertAsync(cart);
        }
    }
}
