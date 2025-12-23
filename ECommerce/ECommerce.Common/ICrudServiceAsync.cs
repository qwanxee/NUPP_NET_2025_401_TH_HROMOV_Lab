using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Common
{
    // Інтерфейс з завдання
    public interface ICrudServiceAsync<T> : IEnumerable<T>
    {
        Task<bool> CreateAsync(T element);
        Task<T> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<IEnumerable<T>> ReadAllAsync(int page, int amount); // Пагінація
        Task<bool> UpdateAsync(T element);
        Task<bool> RemoveAsync(T element);
        Task<bool> SaveAsync(); // Збереження у файл
    }
}