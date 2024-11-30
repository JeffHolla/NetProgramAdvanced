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
        var itemForRemoval = Items.Find(item => item.Id == itemId);
        if (itemForRemoval is null)
        {
            throw new NotFoundCartItemException($"Item Id '{itemId}' was not found in a cart with Id '{Id}'.");
        }

        Items.Remove(itemForRemoval);
    }
}
