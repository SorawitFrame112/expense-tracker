using ExpenseTracker.Infrastructure.Context;
using Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace ExpenseTracker.Infrastructure.Repository
{
  public interface IBaseRepository<T>
  {
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T item);
    Task UpdateAsync(int id, T item);
    Task RemoveAsync(int id);
  } 
}
