using System;

namespace ECommerce.Common
{
    public class Electronics : Product
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }

        public Electronics() { } 

        public Electronics(string name, decimal price, string brand, int warranty)
        {
            Name = name;
            Price = price;
            Brand = brand;
            WarrantyMonths = warranty;
        }

        public static Electronics CreateNew()
        {
            var random = new Random();
            string[] brands = { "Samsung", "Apple", "Sony", "LG", "Asus" };
            string[] types = { "Laptop", "Phone", "TV", "Headphones" };

            var generatedBrand = brands[random.Next(brands.Length)];
            var generatedType = types[random.Next(types.Length)];

            return new Electronics
            {
                Id = Guid.NewGuid(),
                Name = $"{generatedBrand} {generatedType} #{random.Next(100, 999)}",
                Price = random.Next(100, 5000), // Ціна від 100 до 5000
                Brand = generatedBrand,
                WarrantyMonths = random.Next(6, 36)
            };
        }
    }
}