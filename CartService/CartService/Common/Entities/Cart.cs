using CartService.Common.Exceptions;

namespace CartService.Common.Entities;

public class Cart
{
    public string Id { get; set; }

    // This constraints are from DB. DBLite REQUIRES Public GET SET properties
    public List<ProductItem> Items { get; init; } = [];

    public void AddItem(ProductItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(ProductItem item)
    {
        Items.Remove(item);
    }

    public void RemoveItem(int itemId)
    {
        // Alternative solution - FindIndex. Array.IndexOf is used in the implementation of List.Remove(Item).
        var itemForRemoval = Items.FirstOrDefault(item => item.Id == itemId);
        if (itemForRemoval is null)
        {
            throw new NotFoundCartItemException($"Item Id '{itemId}' was not found in a cart with Id '{Id}'.");
        }

        Items.Remove(itemForRemoval);
    }
}
