using System;
using System.Collections.Generic;
using System.Linq;

public class CartService
{
    private List<Product> _cartItems = new List<Product>();

    public event Action CartItemCountChanged;

    public void AddToCart(Product product)
    {
        _cartItems.Add(product);
        CartItemCountChanged?.Invoke();
    }

    public int GetCartItemCount()
    {
        return _cartItems.Count;
    }

    public IReadOnlyList<Product> GetCartItems()
    {
        return _cartItems.AsReadOnly();
    }

    public void RemoveFromCart(Product product)
    {
        _cartItems.Remove(product);
        CartItemCountChanged?.Invoke();
    }
}
