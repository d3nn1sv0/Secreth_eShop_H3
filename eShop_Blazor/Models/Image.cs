public class Image
{
    public int ImageId { get; set; }
    public string? Url { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
