using AutoMapper;
using ExpenseTracker.Application.Dtos;
using ExpenseTracker.Application.Dtos.Response;
using ExpenseTracker.Application.Service;
using ExpenseTracker.Infrastructure.Repository;
using Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Application.Service
{
  public class ExpenseService : IExpenseService
  {
    private readonly IExpenseRepository _repository;
    private readonly IShareRepository<Expense> _shareRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public ExpenseService(
        IExpenseRepository repository,
        IMapper mapper,
        ICategoryRepository categoryRepo,
        IShareRepository<Expense> shareRepo)
    {
      _repository = repository;
      _mapper = mapper;
      _categoryRepo = categoryRepo;
      _shareRepo = shareRepo;
    }

    public async Task<List<ExpenseDto>> GetAllAsync()
    {
      var entities = await _repository.GetAllAsync();
      var categories = await _categoryRepo.GetAllAsync();
      var categoryDict = categories.ToDictionary(c => c.Id, c => c.Name);

      var dtos = entities.Select(e => new ExpenseDto
      {
        Id = e.Id,
        Amount = e.Amount,
        Description = e.Description,
        CategoryId = e.CategoryId,
        CategoryName = categoryDict.TryGetValue(e.CategoryId, out var name) ? name : "Unknown",
        Date = e.Date
      }).ToList();

      return dtos;
    }

    public async Task<ExpenseDto> GetByIdAsync(int id)
    {
      var entity = await _repository.GetByIdAsync(id);
      if (entity == null) return null;

      var category = await _categoryRepo.GetByIdAsync(entity.CategoryId);
      var categoryName = category?.Name ?? "Unknown";

      return new ExpenseDto
      {
        Id = entity.Id,
        Amount = entity.Amount,
        Description = entity.Description,
        CategoryId = entity.CategoryId,
        CategoryName = categoryName,
        Date = entity.Date
      };
    }

    public async Task CreateAsync(ExpenseDto expenseDto)
    {
      var entity = _mapper.Map<Expense>(expenseDto);
      await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(int id, ExpenseDto expenseDto)
    {
      var entity = _mapper.Map<Expense>(expenseDto);
      await _repository.UpdateAsync(id, entity);
    }

    public async Task RemoveAsync(int id)
    {
      await _repository.RemoveAsync(id);
    }

    public async Task<List<ExpenseDto>> GetTopExpensesByCategoryAsync(int topN)
    {
      var expenses = await _shareRepo
          .GetQueryable()
          .OrderByDescending(e => e.Amount)
          .Take(topN)
          .AsNoTracking()
          .ToListAsync();

      var categories = await _categoryRepo.GetAllAsync();
      var categoryDict = categories.ToDictionary(c => c.Id, c => c.Name);

      var dtos = expenses.Select(e => new ExpenseDto
      {
        Id = e.Id,
        Amount = e.Amount,
        Description = e.Description,
        CategoryId = e.CategoryId,
        CategoryName = categoryDict.TryGetValue(e.CategoryId, out var name) ? name : "Unknown",
        Date = e.Date
      }).ToList();

      return dtos;
    }

    public async Task<List<ExpenseDto>> GetExpensesByDateAsync(DateTime date)
    {
      var expenses = await _shareRepo
          .GetQueryable()
          .Where(e => e.Date == date.Date)
          .AsNoTracking()
          .ToListAsync();

      var categories = await _categoryRepo.GetAllAsync();
      var categoryDict = categories.ToDictionary(c => c.Id, c => c.Name);

      var dtos = expenses.Select(e => new ExpenseDto
      {
        Id = e.Id,
        Amount = e.Amount,
        Description = e.Description,
        CategoryId = e.CategoryId,
        CategoryName = categoryDict.TryGetValue(e.CategoryId, out var name) ? name : "Unknown",
        Date = e.Date
      }).ToList();

      return dtos;
    }

    public async Task<List<ExpenseGroupDto>> GetGroupedExpensesByCategoryAsync(int categoryId)
    {
      var expenses = await _shareRepo
          .GetQueryable()
          .Where(e => e.CategoryId == categoryId)
          .AsNoTracking()
          .ToListAsync();

      var categories = await _categoryRepo.GetAllAsync();
      var categoryDict = categories.ToDictionary(c => c.Id, c => c.Name);

      var dtos = expenses.Select(e => new ExpenseDto
      {
        Id = e.Id,
        Amount = e.Amount,
        Description = e.Description,
        CategoryId = e.CategoryId,
        CategoryName = categoryDict.TryGetValue(e.CategoryId, out var name) ? name : "Unknown",
        Date = e.Date
      }).ToList();

      var grouped = dtos
          .GroupBy(e => e.CategoryName)
          .Select(g => new ExpenseGroupDto
          {
            CategoryName = g.Key,
            Expenses = g.ToList()
          })
          .ToList();

      return grouped;
    }

  }
}
