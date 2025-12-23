using System;
using System.Collections.Generic;

namespace ECommerce.Common
{
    public interface ICrudService<T>
    {
        event EventHandler<string> ProductPriceChanged;

        void Create(T item);        // Створити
        IEnumerable<T> GetAll();    // Отримати список
        void Update(T item);        // Оновити
        void Delete(Guid id);       // Видалити
    }
}