using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private DateTime _orderDate;
        private string _customer;
        private string _shippingAddress;
        private IEnumerable<Product> _orderedProducts = new List<Product>();

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public string Customer
        {
            get => _customer;
            private set => _customer = value;
        }

        public string ShippingAddress
        {
            get => _shippingAddress;
            private set => _shippingAddress = value;
        }

        public IEnumerable<Product> OrderedProducts
        {
            get => _orderedProducts;
            private set => _orderedProducts = value;
        }

        public void SetOrderDate(DateTime date)
        {
            _orderDate = date;
        }

        public void SetCustomer(string customer)
        {
            if(string.IsNullOrEmpty(customer))
            {
                throw new ArgumentNullException("Customer was not provided.");
            }
            _customer = customer;
        }

        public void SetShippingAddress(string shippingAddress)
        {
            if(string.IsNullOrEmpty(shippingAddress))
            {
                throw new ArgumentNullException("Address was not provided.");
            }
            _shippingAddress = shippingAddress;
        }

        public void SetOrderedProducts(IEnumerable<Product> orderedProducts)
        {
            _orderedProducts = orderedProducts;
        }
    }
}
