using BusinessEntities;
using System;
using System.Collections.Generic;
using WebApi.Models.Products;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string Customer { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<ProductModel> OrderedProducts { get; set; }
    }
}