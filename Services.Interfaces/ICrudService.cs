using Models;

namespace Services.Interfaces
{
    public interface ICrudService<T> where T : Entity
    {
        Task<int> CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAsync();
        Task<T?> ReadAsync(int id);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
