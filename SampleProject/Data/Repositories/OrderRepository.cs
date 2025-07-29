using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>,IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Order> Get(string customer = null, string shippingAddress = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrderListIndex>();

            var hasFirstParameter = false;
            if(customer != null)
            {
                query = query.WhereEquals("Customer", customer);
                hasFirstParameter = true;
            }

            if(shippingAddress != null)
            {
                if(hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.WhereEquals("ShippingAddress", shippingAddress);
            }
            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<OrderListIndex>();
        }
    }
}
