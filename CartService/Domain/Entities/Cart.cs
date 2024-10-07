using Domain.Exceptions;

namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; init; }

        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();
        private readonly List<Item> _items = [];

        // To discuss - what is more appropriate: using of method for retrieving items or a getter? (basically, they are the same thing, but what looks more right?)
        public IReadOnlyCollection<Item> GetItems()
            => _items.AsReadOnly();

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public void RemoveItem(int itemId)
        {
            // Alternative solution - FindIndex. Array.IndexOf is used in the implementation of List.Remove(Item).
            var itemForRemoval = _items.FirstOrDefault(item => item.Id == itemId);
            if (itemForRemoval is null)
            {
                throw new NotFoundCartItemException($"Item Id '{itemId}' was not found in a cart with Id '{Id}'.");
            }

            _items.Remove(itemForRemoval);
        }
    }
}
