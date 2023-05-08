using Microsoft.EntityFrameworkCore;

public class Basket
{
    public int BasketId { get; set; }
    public int CustomerId { get; set; }
    [Precision(8, 2)]
    public decimal TotalPrice { get; set; }

    public Customer Customer { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
