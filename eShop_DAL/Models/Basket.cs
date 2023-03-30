public class Basket
{
    public int BasketId { get; set; }
    public int CustomerId { get; set; }

    public Customer Customer { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
