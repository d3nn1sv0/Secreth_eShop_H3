public interface IBasketRepository : IRepository<Basket>
{
    Task<Basket> GetBasketWithItemsAsync(int customerId);
}
