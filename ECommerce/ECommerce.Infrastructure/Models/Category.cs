using System.Collections.Generic;

namespace ECommerce.Infrastructure.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}