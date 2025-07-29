using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class OrderListIndex : AbstractIndexCreationTask<Order>
    {
        public OrderListIndex()
        {
            Map = orders => from order in orders
                              select new
                              {
                                  order.OrderDate,
                                  order.Customer,
                                  order.ShippingAddress,
                              };
            Index(x => x.OrderedProducts, FieldIndexing.NotAnalyzed);
        }
    }
}
