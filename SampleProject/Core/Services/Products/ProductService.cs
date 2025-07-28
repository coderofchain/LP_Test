using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    [AutoRegister]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IIdObjectFactory<Product> _productFactory;

        public ProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
        }

        public Product Create(Guid id, string name, string description, string sku, decimal? price, ProductTypes type)
        {
            var product = _productFactory.Create(id);
            Update(product, name, description, sku, price, type);
            _productRepository.Save(product);
            return product;
        }

        public void Update(Product product, string name, string description, string sku, decimal? price, ProductTypes type)
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetSku(sku);
            product.SetPrice(price);
            product.SetType(type);
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
        }

        public void DeleteAll()
        {
            _productRepository.DeleteAll();
        }

        public Product GetProduct(Guid productId)
        {
            return _productRepository.Get(productId);
        }

        public IEnumerable<Product> GetProducts(string name = null, string description = null, string sku = null, decimal? price = null)
        {
            return _productRepository.Get(name, description, sku, price);
        }
    }
}
