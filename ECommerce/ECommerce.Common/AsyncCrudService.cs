using System;
using System.Collections; // <--- Додали це для IEnumerable
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Common
{
    public class AsyncCrudService<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public AsyncCrudService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _repository.AddAsync(element);
            return true;
        }

        public async Task<T> ReadAsync(Guid id)
        {
            // У реальному житті тут має бути пошук по Guid
            return null; 
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _repository.UpdateAsync(element);
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _repository.DeleteAsync(element);
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        // --- ДОДАЛИ ЦІ МЕТОДИ, ЩОБ ВИПРАВИТИ ПОМИЛКУ ---
        public IEnumerator<T> GetEnumerator()
        {
            // Це синхронний метод, тому ми змушені чекати результат асинхронного
            return _repository.GetAllAsync().GetAwaiter().GetResult().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}