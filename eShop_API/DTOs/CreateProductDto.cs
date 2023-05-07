public class CreateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public bool IsVisible { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}
