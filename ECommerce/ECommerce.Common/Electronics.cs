namespace ECommerce.Common
{
    public class Electronics : Product
    {
        public int WarrantyMonths { get; set; }
        public string Brand { get; set; }

        public Electronics(string name, decimal price, string brand, int warranty) 
            : base(name, price) // Виклик конструктора батька
        {
            Brand = brand;
            WarrantyMonths = warranty;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $". Бренд: {Brand}, Гарантія: {WarrantyMonths} міс.";
        }
    }
}