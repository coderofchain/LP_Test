using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class OrderService : IOrderService 
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IIdObjectFactory<Order> _orderFactory;

        public OrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepositry)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepositry;
        }

        public Order Create(Guid id, string customer, string shippingAddress, IEnumerable<Product> orderedProducts)
        {
            DateTime dt = DateTime.Today;
            var formatted = dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            var orderDate = DateTime.ParseExact(formatted, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var order = _orderFactory.Create(id);
            order.SetOrderDate(orderDate);

            Update(order, customer, shippingAddress, orderedProducts);
            _orderRepository.Save(order);
            return order;
        }

        public Order GetOrder(Guid orderId)
        {
            return _orderRepository.Get(orderId);
        }

        public IEnumerable<Order> GetOrders(string customer = null, string shippingAddress = null)
        {
            return _orderRepository.Get(customer, shippingAddress);
        }

        public void Update(Order order, string customer, string shippingAddress, IEnumerable<Product> orderedProducts)
        {
            order.SetCustomer(customer);
            order.SetShippingAddress(shippingAddress);
            order.SetOrderedProducts(orderedProducts);
            // todo add a SetUpdatedOrderDate to allow for date changes separate from the original order date.
        }

        public void Delete(Order order)
        {
            _orderRepository.Delete(order);
        }

        public void DeleteAll()
        {
            _orderRepository.DeleteAll();
        }
    }
}
