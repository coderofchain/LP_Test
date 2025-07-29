using BusinessEntities;
using Common.Extensions;
using Core.Services.Orders;
using Core.Services.Products;
using Core.Services.Users;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using System.Globalization;
using WebApi.Models.Orders;
using WebApi.Models.Products;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var existingIds = _orderService.GetOrders().Select(o => o.Id).ToList();
            if (existingIds.Contains(orderId))
            {
                return DuplicateRecord();
            }
            
            var order = _orderService.Create(orderId, model.Customer, model.ShippingAddress, model.OrderedProducts);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _orderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _orderService.Update(order, model.Customer, model.ShippingAddress, model.OrderedProducts);
            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _orderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _orderService.Delete(order);
            return Found();
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
            _orderService.DeleteAll();
            return Found();
        }

        [Route("id")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return DoesNotExist();
            }
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(string customer = null, string shippingAddress = null)
        {
            var orders = _orderService.GetOrders(customer, shippingAddress)
                                       .Select(q => new OrderData(q))
                                       .ToList();
            return Found(orders);
        }

        // allow the user to pass in a productId and see all orders relating to it
        [Route("list/productOrders")]
        [HttpGet]
        public HttpResponseMessage GetOrdersByProduct(Guid productId)
        {
            IEnumerable<Order> result = new List<Order>();
            var orders = _orderService.GetOrders();

            // the dreaded nested loop here..would think about this some more and possibly change in the future
            foreach (Order o in orders)
            {
                foreach(Product p in o.OrderedProducts)
                {
                    if(p.Id == productId)
                    {
                        result.Append(o);
                        break;
                    }
                    
                }
            }
            return Found(orders);
        }

        [Route("list/orderDate")]
        [HttpGet]
        public HttpResponseMessage GetOrdersByDate(string orderDate)
        {
            var format = "MM/dd/yyyy";
            DateTime dt;

            if (!DateTime.TryParseExact(orderDate,format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return InvalidDateFormat();
            }

            var orders = _orderService.GetOrders()
                         .Where(o => o.OrderDate.Date == dt.Date)
                         .Select(q => new OrderData(q))
                         .ToList();

            return Found(orders); 
        }
    }
}