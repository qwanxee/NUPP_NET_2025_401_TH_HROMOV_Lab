using System;
using ECommerce.Common; 

namespace ECommerce.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо сервіс
            SimpleCrudService<Product> productService = new SimpleCrudService<Product>();
            
            // Підписка на подію зміни ціни
            productService.ProductPriceChanged += (sender, message) => 
            {
                System.Console.WriteLine("-------------------------");
                System.Console.WriteLine($">>> УВАГА! {message}"); 
            };

            System.Console.WriteLine("=== ТЕСТУВАННЯ МАГАЗИНУ ===");

            // Створюємо товари
            var laptop = new Electronics("MacBook Pro", 2500m, "Apple", 12);
            laptop.Id = Guid.NewGuid();

            var tshirt = new Clothing("Футболка Geek", 25m, "L", "Бавовна");
            tshirt.Id = Guid.NewGuid();

            // Додаємо
            productService.Create(laptop);
            productService.Create(tshirt);

            // Виводимо список
            System.Console.WriteLine("\n--- Список товарів ---");
            foreach (var p in productService.GetAll())
            {
                System.Console.WriteLine($"{p.Name} - {p.Price}$");
            }

            // Оновлюємо ціну
            System.Console.WriteLine("\n--- Зміна ціни ---");
            laptop.Price = 2000m; 
            productService.Update(laptop);

            // Видаляємо
            System.Console.WriteLine("\n--- Видалення товару ---");
            productService.Delete(tshirt.Id); 

            // Фінальний список
            System.Console.WriteLine("\n--- Залишилось товарів ---");
            foreach (var p in productService.GetAll())
            {
                System.Console.WriteLine(p.Name);
            }
        }
    }
}