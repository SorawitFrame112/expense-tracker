using ExpenseTracker.Application.Dtos.Response;
using ExpenseTracker.Infrastructure.Context;

using Infrastructure.Domain.Models;



namespace ExpenseTracker.Application.Service
{
  public interface IExpenseService : IBaseService<ExpenseDto>
  {
    Task<List<ExpenseDto>> GetTopExpensesByCategoryAsync(int topN);
    Task<List<ExpenseDto>> GetExpensesByDateAsync(DateTime date);
    Task<List<ExpenseGroupDto>> GetGroupedExpensesByCategoryAsync(int categoryId);
  }
}
