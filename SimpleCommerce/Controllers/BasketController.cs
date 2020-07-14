using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCommerce.Data;
using SimpleCommerce.Data.Entities;
using SimpleCommerce.Models;
using SimpleCommerce.Services;

namespace SimpleCommerce.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly ApplicationContext _context;

        public BasketController(IBasketService basketService, ApplicationContext context)
        {
            _basketService = basketService;
            _context = context;
        }
        // GET: Basket
        public IActionResult Index()
        {
            Basket basket = _basketService.GetOrAddBasket("basket");
            var productIds = basket.BasketItems.Select(b => b.ProductId).ToArray();
            var products = _context.Product.AsNoTracking().Where(p => productIds.Contains(p.Id)).ToDictionary(p => p.Id, p => p);

            var basketItemModels = basket.BasketItems.Select(b => new BasketItemModel
            {
                ProductId = b.ProductId,
                ProductName = products[b.ProductId].Name,
                ProductPrice = products[b.ProductId].Price,
                Quantity = b.Quanitity
            });

            return View(basketItemModels);
        }
         
        // POST: Basket/Add
        [HttpPost]
        public IActionResult Add([FromBody] BasketUpdateModel basketUpdateModel)
        {
            Basket basket = _basketService.GetOrAddBasket("basket");
            var basketItemsLookup = basket.BasketItems.ToDictionary(b => b.ProductId, b => b);

            if (!basketItemsLookup.ContainsKey(basketUpdateModel.ProductId))
            {
                basket.BasketItems.Add(new BasketItem
                {
                    ProductId = basketUpdateModel.ProductId,
                    Quanitity = 1
                });
            }
            else
            {
                basketItemsLookup[basketUpdateModel.ProductId].Quanitity++;
            }

            _basketService.SaveBasket("basket", basket);

            return Ok();
        }
                
        // POST: Basket/Remove
        [HttpPost]
        public ActionResult Remove([FromBody] BasketUpdateModel basketUpdateModel)
        {
            Basket basket = _basketService.GetOrAddBasket("basket");
            var basketItemsLookup = basket.BasketItems.ToDictionary(b => b.ProductId, b => b);

            if (!basketItemsLookup.ContainsKey(basketUpdateModel.ProductId))
            {
                return NotFound();
            }

            var basketItem = basketItemsLookup[basketUpdateModel.ProductId];
            basketItem.Quanitity--;

            if (basketItem.Quanitity <= 0)
            {
                basket.BasketItems.Remove(basketItem);
            }

            _basketService.SaveBasket("basket", basket);

            return Ok();
        }
    }
}
