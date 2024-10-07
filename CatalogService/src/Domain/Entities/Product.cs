namespace CatalogService.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public uint Amount { get; set; }

    public void Update(Product updatedProduct)
    {
        Name = updatedProduct.Name;
        Description = updatedProduct.Description;
        Category = updatedProduct.Category;
        Price = updatedProduct.Price;
        Amount = updatedProduct.Amount;
        Image = updatedProduct.Image;
    }
}
