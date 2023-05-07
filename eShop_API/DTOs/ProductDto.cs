public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public bool IsVisible { get; set; }
    public int CategoryId { get; set; }
    public CategoryDto Category { get; set; }
    public int SupplierId { get; set; }
    public SupplierDto Supplier { get; set; }
    public ICollection<ImageDto> Images { get; set; }
}
