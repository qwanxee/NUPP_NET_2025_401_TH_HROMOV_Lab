namespace ECommerce.Infrastructure.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string WarehouseLocation { get; set; }

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}