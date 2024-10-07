namespace CatalogService.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string? Image { get; set; }
    public Category Parent { get; set; }

    public void Update(Category updatedCategory)
    {
        Name = updatedCategory.Name;
        Image = updatedCategory.Image;
        Parent = updatedCategory.Parent;
    }
}
