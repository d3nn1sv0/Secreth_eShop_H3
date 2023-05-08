using System.ComponentModel.DataAnnotations;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ShippingAddress { get; set; }
    public bool IsVisible { get; set; } = true;

    public ICollection<Order>? Orders { get; set; }
    public Basket? Basket { get; set; }
}
