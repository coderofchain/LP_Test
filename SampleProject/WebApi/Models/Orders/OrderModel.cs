using BusinessEntities;
using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string Customer { get; set; }
        public string ShippingAddress { get; set; }

        public  IEnumerable<Product> OrderedProducts = new List<Product>();
    }
}