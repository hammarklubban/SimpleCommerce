using SimpleCommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCommerce.Models
{
    public class ProductModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Dictionary<string, SortOrder> SortOrders { get; set; }
    }

    public enum SortOrder 
    {
        Ascending,
        Decending
    }
}
