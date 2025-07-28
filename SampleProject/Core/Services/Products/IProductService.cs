using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IProductService
    {
        Product GetProduct(Guid id);

        void Delete(Product product);

        void DeleteAll();

        Product Create(Guid id, string name, string description, string sku, decimal? price, ProductTypes type);

        void Update(Product product, string name, string description, string sku, decimal? price, ProductTypes type);

        IEnumerable<Product> GetProducts(string name = null, string description = null, string sku = null, decimal? price = null);

    }
}