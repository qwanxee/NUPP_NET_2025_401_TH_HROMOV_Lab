using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Common
{
    // Реалізація сервісу. T : Product означає, що сервіс працює тільки з продуктами
    public class SimpleCrudService<T> : ICrudService<T> where T : Product
    {
        private readonly List<T> _items = new List<T>();

        // 1. Реалізація події
        public event EventHandler<string> ProductPriceChanged;

        // 2. Метод Create
        public void Create(T item)
        {
            _items.Add(item);
            Console.WriteLine($"[Create] Об'єкт {item.Name} додано.");
        }

        // 3. Метод GetAll (якого не вистачало)
        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        // 4. Метод Update
        public void Update(T item)
        {
            var existing = _items.FirstOrDefault(x => x.Id == item.Id);
            if (existing != null)
            {
                // Перевіряємо, чи змінилась ціна, щоб запустити подію
                if (existing.Price != item.Price)
                {
                    ProductPriceChanged?.Invoke(this, $"Ціна змінилась для {item.Name}");
                }
                
                // Оновлюємо поля
                existing.Name = item.Name;
                existing.Price = item.Price;
                Console.WriteLine($"[Update] Об'єкт {item.Name} оновлено.");
            }
        }

        // 5. Метод Delete (якого не вистачало)
        public void Delete(Guid id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _items.Remove(item);
                Console.WriteLine($"[Remove] Об'єкт видалено.");
            }
        }
    }
}