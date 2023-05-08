public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public bool IsVisible { get; set; } = true;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<Image>? Images { get; set; }
}
