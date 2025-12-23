using System;
using System.Collections.Concurrent; // Для потокобезпечних колекцій
using System.Diagnostics; // Для заміру часу
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Common;

namespace ECommerce.ConsoleApp
{
    class Program
    {
        // робимо Main асинхронним 
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Асинхронність ");

            string filePath = "products_data.json";
            
            var service = new AsyncCrudService<Product>(filePath);

            Console.WriteLine("\n[1] Початок паралельної генерації 1000 товарів...");
            var stopwatch = Stopwatch.StartNew();

          
            var tasks = new Task[1000];
            
            for (int i = 0; i < 1000; i++)
            {
                tasks[i] = Task.Run(async () => 
                {
                    var product = Electronics.CreateNew();
                    await service.CreateAsync(product);
                });
            }

            await Task.WhenAll(tasks);
            stopwatch.Stop();

            Console.WriteLine($"[Готово] 1000 товарів створено та додано за {stopwatch.ElapsedMilliseconds} мс.");

            Console.WriteLine("\n[2] Аналіз даних...");
            var allProducts = await service.ReadAllAsync();
            
            if (allProducts.Any())
            {
                var minPrice = allProducts.Min(p => p.Price);
                var maxPrice = allProducts.Max(p => p.Price);
                var avgPrice = allProducts.Average(p => p.Price);

                Console.WriteLine($"   -> Мінімальна ціна: {minPrice}$");
                Console.WriteLine($"   -> Максимальна ціна: {maxPrice}$");
                Console.WriteLine($"   -> Середня ціна:    {avgPrice:F2}$");
            }

            Console.WriteLine("\n[3] Тест пагінації (сторінка 2, по 5 штук):");
            var page2 = await service.ReadAllAsync(page: 2, amount: 5);
            foreach (var p in page2)
            {
                Console.WriteLine($"   - {p.Name} ({p.Price}$)");
            }

            Console.WriteLine($"\n[4] Збереження колекції у файл '{filePath}'...");
            bool saved = await service.SaveAsync();
            Console.WriteLine(saved ? "   [Успіх] Дані збережено." : "   [Помилка] Не вдалося зберегти.");

            // перевірка, що файл існує
            if (System.IO.File.Exists(filePath))
            {
                Console.WriteLine($"   Файл існує, розмір: {new System.IO.FileInfo(filePath).Length} байт.");
            }
        }
    }
}