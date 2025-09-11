using ExpenseTracker.Infrastructure.Context;

using Infrastructure.Domain.Models;



namespace ExpenseTracker.Application.Service
{
  public interface IExpenseService : IBaseService<Expense>
  {
  }
}
