using BusinessEntities;
using Common.Extensions;
using Core.Services.Products;
using Core.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage createProduct(Guid productId, [FromBody] ProductModel model)
        {
            // check for existing productIds before creating
            var existingIds = _productService.GetProducts().Select(p => p.Id).ToList();
            if(existingIds.Contains(productId))
            {
                return DuplicateRecord();
            }
            // rename to CreateProduct
            var product = _productService.Create(productId, model.Name, model.Description, model.Sku, model.Price, model.Type);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _productService.GetProduct(productId);
            if(product == null)
            {
                return DoesNotExist();
            }
            _productService.Update(product, model.Name, model.Description, model.Sku, model.Price, model.Type);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _productService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            _productService.Delete(product);
            return Found();
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            _productService.DeleteAll();
            return Found();
        }

        [Route("id")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid id)
        {
            var product = _productService.GetProduct(id);
            if(product == null)
            {
                return DoesNotExist();
            }

            return Found(new ProductData(product));
        }

        // re-test with next request checkin
        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            var products = _productService.GetProducts()
                                          .Select(p => new ProductData(p))
                                          .ToList();
            return Found(products);
        }

        [Route("list/type")]
        [HttpGet]
        public HttpResponseMessage GetProductsByType(string type)
        {
            List<ProductData> products = new List<ProductData>();

            if (int.TryParse(type, out int value))
            {
                products = _productService.GetProducts()
                               .Where(p => ((int)p.Type == (value)))
                               .Select(q => new ProductData(q))
                               .ToList();
            }

            return Found(products);
        }
    }
}