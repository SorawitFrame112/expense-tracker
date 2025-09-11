using ExpenseTracker.Infrastructure.Repository;
using Infrastructure.Domain.Models;


namespace ExpenseTracker.Application.Service { 
  public class ExpenseService : IExpenseService
  {
    private readonly IExpenseRepository _repository;

    public ExpenseService(IExpenseRepository repository)
    {
      _repository = repository;
    }

    public Task<List<Expense>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Expense> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task CreateAsync(Expense expense) => _repository.CreateAsync(expense);
    public Task UpdateAsync(int id, Expense expenseIn) => _repository.UpdateAsync(id, expenseIn);
    public Task RemoveAsync(int id) => _repository.RemoveAsync(id);
  }
}
