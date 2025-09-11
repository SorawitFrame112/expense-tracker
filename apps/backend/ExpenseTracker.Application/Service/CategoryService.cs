using ExpenseTracker.Infrastructure.Repository;
using Infrastructure.Domain.Models;


namespace ExpenseTracker.Application.Service
{
  public class CategoryService : ICategoryService
  {
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
      _repository = repository;
    }
      
    public Task<List<Category>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Category> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task CreateAsync(Category category) => _repository.CreateAsync(category);
    public Task UpdateAsync(int id, Category categoryIn) => _repository.UpdateAsync(id, categoryIn);
    public Task RemoveAsync(int id) => _repository.RemoveAsync(id);
  }

}
