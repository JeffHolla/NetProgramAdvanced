namespace CartService.Common.Entities;

public class ProductItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public ProductItem(int id, string name, decimal price, int quantity, string image = "not exists")
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Image = image;
    }
}
