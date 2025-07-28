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
            var product = _productService.Create(productId, model.Name, model.Description, model.Sku, model.Price);
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
            _productService.Update(product, model.Name, model.Description, model.Sku, model.Price);
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

        // todo will add a storageType for 'perishible' vs. 'non-perishible' for filtering purposes
        // and to enrich our response outputs a bit more.
        [Route("list/type")]
        [HttpGet]
        public HttpResponseMessage GetProductsByType(string type)
        {
            // todo will add a filter here based on type
            throw new NotImplementedException();
        }

    }
}