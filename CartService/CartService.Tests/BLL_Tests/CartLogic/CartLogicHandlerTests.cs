using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using CartService.Common.Exceptions;
using CartService.DAL.Repositories.Common;
using FluentAssertions;
using NSubstitute;

namespace CartService.Tests.BLL_Tests.CartLogic
{
    // Example of Unit Tests
    public class CartLogicHandlerTests
    {
        private CartLogicHandler _cartLogic;
        private IRepository<Cart> _cartRepository;

        [SetUp]
        public void Setup()
        {
            _cartRepository = Substitute.For<IRepository<Cart>>();
            _cartLogic = new CartLogicHandler(_cartRepository);
        }

        [Test]
        public async Task GetCartItemsAsync_Works()
        {
            var cartId = 1;
            var expectedItemInCart = new ProductItem(1, "name 1", 1, 1);

            var cartDummy = new Cart() { Id = cartId, Items = [expectedItemInCart] };
            _cartRepository.GetEntityAsync(Arg.Any<int>()).Returns(cartDummy);

            // Act
            var cartItems = await _cartLogic.GetCartItemsAsync(cartId);

            // Assert
            cartItems.Should().BeEquivalentTo([expectedItemInCart]);
        }

        [Test]
        public async Task AddItemToCartAsync_Works()
        {
            var cartId = 1;
            var itemToAdd = new ProductItem(1, "name 1", 1, 1);

            var cartDummy = new Cart() { Id = cartId };
            _cartRepository.GetEntityAsync(Arg.Any<int>()).Returns(cartDummy);

            // Act
            await _cartLogic.AddItemToCartAsync(cartId, itemToAdd);

            // Assert
            await _cartRepository.Received(1).UpdateEntityAsync(cartId, Arg.Any<Cart>());
        }

        [Test]
        public async Task AddItemToCartAsync_ThrowsExceptionOnInvalidItem()
        {
            var cartId = 1;
            var itemInvalidName = "";
            var itemToAdd = new ProductItem(1, itemInvalidName, 1, 1);

            var cartDummy = new Cart() { Id = cartId };
            _cartRepository.GetEntityAsync(Arg.Any<int>()).Returns(cartDummy);

            // Act
            var action = async () => await _cartLogic.AddItemToCartAsync(cartId, itemToAdd);

            // Assert
            await action.Should().ThrowAsync<ValidationFailedException>();
            await _cartRepository.DidNotReceive().UpdateEntityAsync(cartId, Arg.Any<Cart>());
        }
    }
}
