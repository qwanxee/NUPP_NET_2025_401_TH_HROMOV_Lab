using System;
using System.Collections.Generic;

namespace ECommerce.Common
{
    // Інтерфейс - це список команд, які програма зобов'язана вміти виконувати
    public interface ICrudService<T>
    {
        // Подія: коли змінюється ціна
        event EventHandler<string> ProductPriceChanged;

        // Методи (дії)
        void Create(T item);        // Створити
        IEnumerable<T> GetAll();    // Отримати список
        void Update(T item);        // Оновити
        void Delete(Guid id);       // Видалити
    }
}