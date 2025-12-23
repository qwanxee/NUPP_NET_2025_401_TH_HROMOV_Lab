using System;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Infrastructure; // Для контексту бази
using ECommerce.Infrastructure.Models; // Для сутностей
using ECommerce.Infrastructure.Repositories; // Для репозиторіїв

namespace ECommerce.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №3: База Даних та EF Core ===");

            // 1. Створюємо контекст (підключення до БД)
            using var context = new ECommerceContext();
            
            // Гарантуємо, що база створена
            context.Database.EnsureCreated();

            // 2. Створюємо Репозиторій
            // Зверни увагу: ми використовуємо ProductEntity, а не Product з першої лаби,
            // бо база даних працює з моделями з папки Infrastructure/Models
            var repository = new BaseRepository<ProductEntity>(context);

            // 3. Створюємо Сервіс і передаємо туди репозиторій
            var service = new AsyncCrudService<ProductEntity>(repository);

            Console.WriteLine("\n[1] Додаємо тестовий товар у Базу Даних...");
            
            var newProduct = new ElectronicsEntity
            {
                Name = "Samsung Galaxy S24",
                Price = 1200,
                Brand = "Samsung",
                WarrantyMonths = 24,
                CategoryId = 1 // Припускаємо, що категорія 1 існує (Seeding)
            };

            await service.CreateAsync(newProduct);
            Console.WriteLine("   -> Товар додано в чергу.");

            // Зберігаємо зміни (хоча BaseRepository вже робить Save, але для надійності)
            await service.SaveAsync();
            Console.WriteLine("   -> Зміни збережено в БД (ecommerce.db).");

            Console.WriteLine("\n[2] Читаємо всі товари з Бази Даних:");
            var products = await service.ReadAllAsync();
            
            foreach (var p in products)
            {
                Console.WriteLine($"   ID: {p.Id} | {p.Name} - {p.Price}$");
            }
        }
    }
}