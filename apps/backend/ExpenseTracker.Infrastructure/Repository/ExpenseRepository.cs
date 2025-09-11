using ExpenseTracker.Infrastructure.Context;
using Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace ExpenseTracker.Infrastructure.Repository
{
  public class ExpenseRepository:IExpenseRepository
  {
    private readonly ExpenseTrackerDbContext _context;

    public ExpenseRepository(ExpenseTrackerDbContext context)
    {
      _context = context;
    }

    public async Task<List<Expense>> GetAllAsync() => await _context.Expenses.ToListAsync();
    public async Task<Expense> GetByIdAsync(int id) => await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    public async Task CreateAsync(Expense expense)
    {
      await _context.Expenses.AddAsync(expense);
      await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, Expense expenseIn)
    {
      var expense = await _context.Expenses.FindAsync(id);
      if (expense != null)
      {
        expense.Description = expenseIn.Description;
        expense.Amount = expenseIn.Amount;
        expense.Date = expenseIn.Date;
        expense.CategoryId = expenseIn.CategoryId;
        await _context.SaveChangesAsync();
      }
    }
    public async Task RemoveAsync(int id)
    {
      var expense = await _context.Expenses.FindAsync(id);
      if (expense != null)
      {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
      }
    }
  }
}
