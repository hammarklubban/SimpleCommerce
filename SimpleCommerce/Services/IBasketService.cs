using SimpleCommerce.Data.Entities;

namespace SimpleCommerce.Services
{
    public interface IBasketService
    {
        Basket GetOrAddBasket(string key);
        void SaveBasket(string key, Basket basket);
    }
}