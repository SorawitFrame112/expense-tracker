


namespace ExpenseTracker.Application.Service

{
  public interface IBaseService<T>
  {
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T item);
    Task UpdateAsync(int id, T item);
    Task RemoveAsync(int id);
  }
}
