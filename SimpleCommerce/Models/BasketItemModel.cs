using SimpleCommerce.Data.Entities;
using System;

namespace SimpleCommerce.Models
{
    public class BasketItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
