namespace ECommerce.Common
{
    public class Clothing : Product
    {
        public string Size { get; set; } // S, M, й тощо
        public string Material { get; set; }

        public Clothing(string name, decimal price, string size, string material)
            : base(name, price)
        {
            Size = size;
            Material = material;
        }
    }
}