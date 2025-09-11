using ExpenseTracker.Infrastructure.Context;
using Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repository
{
  public class CategoryRepository: ICategoryRepository
  {
    private readonly ExpenseTrackerDbContext _context;

    public CategoryRepository(ExpenseTrackerDbContext context)
    {
      _context = context;
    }

    public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();
    public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    public async Task CreateAsync(Category category)
    {
      await _context.Categories.AddAsync(category);
      await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, Category categoryIn)
    {
      var category = await _context.Categories.FindAsync(id);
      if (category != null)
      {
        category.Name = categoryIn.Name;
        await _context.SaveChangesAsync();
      }
    }
    public async Task RemoveAsync(int id)
    {
      var category = await _context.Categories.FindAsync(id);
      if (category != null)
      {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
      }
    }
  }
}
