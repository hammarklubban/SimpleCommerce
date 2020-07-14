using System;
using System.Collections.Generic;
namespace SimpleCommerce.Data.Entities
{
    public class Basket
    {
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}