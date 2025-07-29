using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;
        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Product> Get(string name = null, string description = null, string sku = null, decimal? price = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductListIndex>();

            var hasFirstParameter = false;
            if (name != null)
            {
                query = query.WhereEquals("Name", name);
                hasFirstParameter = true;
            }

            if(description != null)
            {
                if(hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.WhereEquals("Description", description);
            }

            if(sku != null)
            {
                if(hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.WhereEquals("Sku", sku);
            }

            if(price != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Price", price);
            }
            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<ProductListIndex>();
        }
    }
}
