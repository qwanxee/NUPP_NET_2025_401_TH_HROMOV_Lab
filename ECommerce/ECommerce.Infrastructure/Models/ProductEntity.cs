using System;

namespace ECommerce.Infrastructure.Models
{
    
    public class ProductEntity
    {
        public int Id { get; set; } 
        public Guid PublicId { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }

       
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
        public ProductStock Stock { get; set; }
    }

    
    public class ElectronicsEntity : ProductEntity
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }
    }

    
    public class ClothingEntity : ProductEntity
    {
        public string Size { get; set; }
        public string Material { get; set; }
    }
}