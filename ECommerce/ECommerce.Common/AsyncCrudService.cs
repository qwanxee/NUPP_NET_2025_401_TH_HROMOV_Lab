using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json; // Для запису у файл
using System.Threading; // Для семафорів (Lock)
using System.Threading.Tasks;

namespace ECommerce.Common
{
    public class AsyncCrudService<T> : ICrudServiceAsync<T> where T : Product
    {
        private readonly List<T> _items; // Вбудована колекція
        private readonly string _filePath;
        
        // Примітив синхронізації (завдання №4).
        // SemaphoreSlim дозволяє обмежувати доступ до ресурсу. (1, 1) працює як async lock.
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public AsyncCrudService(string filePath)
        {
            _items = new List<T>();
            _filePath = filePath;
        }

        // 1. Create (з блокуванням потоків)
        public async Task<bool> CreateAsync(T element)
        {
            await _semaphore.WaitAsync(); // "Зачиняємо двері" перед іншими потоками
            try
            {
                _items.Add(element);
                return true;
            }
            finally
            {
                _semaphore.Release(); // "Відчиняємо двері"
            }
        }

        // 2. Read
        public async Task<T> ReadAsync(Guid id)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _items.FirstOrDefault(x => x.Id == id);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // 3. ReadAll
        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                // Повертаємо копію списку, щоб уникнути помилок при зміні в іншому потоці
                return new List<T>(_items);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // 4. Пагінація (сторінки)
        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _items
                    .Skip((page - 1) * amount)
                    .Take(amount)
                    .ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                var existing = _items.FirstOrDefault(x => x.Id == element.Id);
                if (existing != null)
                {
                    _items.Remove(existing);
                    _items.Add(element);
                    return true;
                }
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                var existing = _items.FirstOrDefault(x => x.Id == element.Id);
                if (existing != null)
                {
                    _items.Remove(existing);
                    return true;
                }
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // 5. Збереження у файл (Серіалізація)
        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                // Налаштування, щоб JSON був красивим і читабельним
                var options = new JsonSerializerOptions { WriteIndented = true };
                
                using (FileStream createStream = File.Create(_filePath))
                {
                    await JsonSerializer.SerializeAsync(createStream, _items, options);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запису: {ex.Message}");
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // Реалізація IEnumerable (для перебору foreach без async)
        public IEnumerator<T> GetEnumerator()
        {
            // Увага: цей метод не асинхронний і не потокобезпечний "з коробки", 
            // тому краще використовувати ReadAllAsync у коді.
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}