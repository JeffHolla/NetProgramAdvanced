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
        public void GetCartItems_Works()
        {
            var cartId = 1;
            var expectedItemInCart = new ProductItem(1, "name 1", 1, 1);

            var cartDummy = new Cart() { Id = cartId, Items = [expectedItemInCart] };
            _cartRepository.GetEntity(Arg.Any<int>()).Returns(cartDummy);

            // Act
            var cartItems = _cartLogic.GetCartItems(cartId);

            // Assert
            cartItems.Should().BeEquivalentTo([expectedItemInCart]);
        }

        [Test]
        public void AddItemToCart_Works()
        {
            var cartId = 1;
            var itemToAdd = new ProductItem(1, "name 1", 1, 1);

            var cartDummy = new Cart() { Id = cartId };
            _cartRepository.GetEntity(Arg.Any<int>()).Returns(cartDummy);

            // Act
            _cartLogic.AddItemToCart(cartId, itemToAdd);

            // Assert
            _cartRepository.Received(1).UpdateEntity(cartId, Arg.Any<Cart>());
        }

        [Test]
        public void AddItemToCart_ThrowsExceptionOnInvalidItem()
        {
            var cartId = 1;
            var itemInvalidName = "";
            var itemToAdd = new ProductItem(1, itemInvalidName, 1, 1);

            var cartDummy = new Cart() { Id = cartId };
            _cartRepository.GetEntity(Arg.Any<int>()).Returns(cartDummy);

            // Act
            var action = () => _cartLogic.AddItemToCart(cartId, itemToAdd);

            // Assert
            action.Should().Throw<ValidationFailedException>();
            _cartRepository.DidNotReceive().UpdateEntity(cartId, Arg.Any<Cart>());
        }
    }
}
