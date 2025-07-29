using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    public interface IOrderService
    {
        Order GetOrder(Guid id);

        void Delete(Order order);

        void DeleteAll();

        Order Create(Guid id, string customer, string shippingAddress, IEnumerable<Product> orderedProducts);

        void Update(Order order, string customer, string shippingAddress, IEnumerable<Product> orderedProducts);

        IEnumerable<Order> GetOrders(string customer = null, string shippingAddress = null);

        Product Create(string name, string description, string sku, decimal? price, ProductTypes type);
    }
}
