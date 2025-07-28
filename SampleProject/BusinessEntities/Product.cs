using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private string _description;
        private decimal? _price;
        private string _sku;
        private ProductTypes _type = ProductTypes.Non_Perishable;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public decimal? Price
        {
            get => _price;
            private set => _price = value;
        }

        public string Sku
        {
            get => _sku;
            private set => _sku = value;
        }

        public ProductTypes Type
        {
            get => _type;
            private set => _type = value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            _name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("Description was not provided.");
            }
            _description = description;
        }

        public void SetSku(string sku)
        { 
            if(string.IsNullOrEmpty(sku))
            {
                throw new ArgumentNullException("Skku was not provided.");
            }
            _sku = sku;
        }

        public void SetPrice(decimal? price)
        {
            if (!price.HasValue)
            {
                throw new ArgumentNullException("Price was not provided.");
            }
            _price = price;
        }
        public void SetType(ProductTypes type)
        {
            _type = type;
        }
    }
}
