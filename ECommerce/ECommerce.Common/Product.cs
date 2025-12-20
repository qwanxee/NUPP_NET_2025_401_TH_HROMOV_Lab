using System;

namespace ECommerce.Common
{
    // Базовий клас
    public class Product
    {
        // Подія (Event)
        public event EventHandler PriceChanged;

        public Guid Id { get; set; }
        public string Name { get; set; }
        
        private decimal _price;
        public decimal Price 
        { 
            get => _price; 
            set 
            {
                if (_price != value)
                {
                    _price = value;
                    // Виклик події при зміні ціни
                    PriceChanged?.Invoke(this, EventArgs.Empty);
                }
            } 
        }

        public static string StoreName = "My Super Shop";

        // Конструктор
        public Product() 
        {
            Id = Guid.NewGuid();
        }

        // Конструктор з параметрами
        public Product(string name, decimal price) : this()
        {
            Name = name;
            Price = price;
        }

        public virtual string GetInfo()
        {
            return $"{Name} коштує {Price}$ (ID: {Id})";
        }
    }
}