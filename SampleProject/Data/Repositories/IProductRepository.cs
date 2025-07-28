using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        // todo add an additional enumeration type for the params
        IEnumerable<Product> Get(string name = null, string description = null, string sku = null, decimal? price = null);
        void DeleteAll();
    }
}
