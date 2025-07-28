using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ProductListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductListIndex()
        {
            Map = products => from product in products
                           select new
                           {
                               product.Name,
                               product.Description,
                               product.Sku,
                               product.Price
                           };

        }
    }
}
