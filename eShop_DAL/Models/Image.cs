using System.ComponentModel.DataAnnotations;

public class Image
{
    [Key]
    public int ImageId { get; set; }
    public string? Url { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
