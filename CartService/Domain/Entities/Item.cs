namespace Domain.Entities
{
    public class Item
    {
        public int Id { get; set; } // required
        public string Name { get; set; } // required
        public string Image { get; set; }  // optional
        public decimal Price { get; set; } // required
        public int Quantity { get; set; }
    }
}
