using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Sku = product.Sku;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Sku { get; set; }
    }
}