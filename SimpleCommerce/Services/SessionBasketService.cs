using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimpleCommerce.Data.Entities;

namespace SimpleCommerce.Services
{
    public class SessionBasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionBasketService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Basket GetOrAddBasket(string key)
        {
            var basketString = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(basketString))
            {
                var basket = new Basket();
                SaveBasket(key, basket);
                return basket;
            }

            return JsonConvert.DeserializeObject<Basket>(_httpContextAccessor.HttpContext.Session.GetString(key));
        }

        public void SaveBasket(string key, Basket basket)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(basket));
        }
    }
}
