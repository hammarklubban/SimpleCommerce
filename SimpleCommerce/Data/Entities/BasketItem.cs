using System;
namespace SimpleCommerce.Data.Entities
{
    public class BasketItem
    {
        public Guid ProductId { get; set; }
        public int Quanitity { get; set; }
    }
}