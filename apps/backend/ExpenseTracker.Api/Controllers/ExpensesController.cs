using ExpenseTracker.Application.Service;
using Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ExpensesController : ControllerBase
  {
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
      _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Expense>>> Get() => await _expenseService.GetAllAsync();

    [HttpGet("{id:int}", Name = "GetExpense")]
    public async Task<ActionResult<Expense>> GetById(int id)
    {
      var expense = await _expenseService.GetByIdAsync(id);

      if (expense == null)
      {
        return NotFound();
      }

      return expense;
    }

    [HttpPost]
    public async Task<ActionResult<Expense>> Create(Expense expense)
    {
      await _expenseService.CreateAsync(expense);

      return CreatedAtRoute("GetExpense", new { id = expense.Id }, expense);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Expense expenseIn)
    {
      var expense = await _expenseService.GetByIdAsync(id);

      if (expense == null)
      {
        return NotFound();
      }

      await _expenseService.UpdateAsync(id, expenseIn);

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      var expense = await _expenseService.GetByIdAsync(id);

      if (expense == null)
      {
        return NotFound();
      }

      await _expenseService.RemoveAsync(id);

      return NoContent();
    }
  }
}
