using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    [Precision(8,2)]
    public decimal Price { get; set; }
    public string Description { get; set; }
    public bool IsVisible { get; set; } = true;

    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [Required]
    public int SupplierId { get; set; }
    [ForeignKey("SupplierId")]
    public Supplier Supplier { get; set; }
    public ICollection<Image>? Images { get; set; }
}
