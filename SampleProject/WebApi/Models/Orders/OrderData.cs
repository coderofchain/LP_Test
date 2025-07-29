using BusinessEntities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            OrderDate = order.OrderDate;
            Customer = order.Customer;
            ShippingAddress = order.ShippingAddress;
            OrderedProducts = order.OrderedProducts;
        }

        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<Product> OrderedProducts { get; set; }
    }
}