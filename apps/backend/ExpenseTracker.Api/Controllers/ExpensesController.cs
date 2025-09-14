using ExpenseTracker.Application.Service;
using ExpenseTracker.Application.Dtos;
using ExpenseTracker.Application.Dtos.Response;
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

    private async Task<ActionResult<ResponseModel<T>>> HandleRequestAsync<T>(Func<Task<T>> func, string successMessage = null)
    {
      try
      {
        var result = await func();
        return new ResponseModel<T>
        {
          Data = result,
          Success = true,
          Message = successMessage
        };
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<T>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "An error occurred while processing your request."
        });
      }
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<List<ExpenseDto>>>> Get()
    {
      return await HandleRequestAsync(() => _expenseService.GetAllAsync());
    }

    [HttpGet("{id:int}", Name = "GetExpense")]
    public async Task<ActionResult<ResponseModel<ExpenseDto>>> GetById(int id)
    {
      try
      {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null)
        {
          return NotFound(new ResponseModel<ExpenseDto>
          {
            Success = false,
            Message = "Expense not found"
          });
        }

        return new ResponseModel<ExpenseDto>
        {
          Data = expense,
          Success = true
        };
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<ExpenseDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to retrieve expense."
        });
      }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<ExpenseDto>>> Create(ExpenseDto expenseDto)
    {
      try
      {
        await _expenseService.CreateAsync(expenseDto);
        return CreatedAtRoute("GetExpense", new { id = expenseDto.Id }, new ResponseModel<ExpenseDto>
        {
          Data = expenseDto,
          Success = true,
          Message = "Expense created successfully"
        });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<ExpenseDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to create expense."
        });
      }
    }

    [HttpGet("GetTopExpenses/{topN:int}")]
    public async Task<ActionResult<ResponseModel<List<ExpenseDto>>>> GetTopExpenses(int topN)
    {
      try
      {
        var expenseDto = await _expenseService.GetTopExpensesByCategoryAsync(topN);
        return new ResponseModel<List<ExpenseDto>>
        {
          Data = expenseDto,
          Success = true,
          Message = $"Top {topN} expenses retrieved successfully"
        };
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<List<ExpenseDto>>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to retrieve top expenses."
        });
      }
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ExpenseDto expenseDto)
    {
      try
      {
        var existing = await _expenseService.GetByIdAsync(id);
        if (existing == null)
        {
          return NotFound(new ResponseModel<ExpenseDto>
          {
            Success = false,
            Message = "Expense not found"
          });
        }

        await _expenseService.UpdateAsync(id, expenseDto);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<ExpenseDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to update expense."
        });
      }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var existing = await _expenseService.GetByIdAsync(id);
        if (existing == null)
        {
          return NotFound(new ResponseModel<ExpenseDto>
          {
            Success = false,
            Message = "Expense not found"
          });
        }

        await _expenseService.RemoveAsync(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<ExpenseDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to delete expense."
        });
      }
    }
    [HttpGet("by-date")]
    public async Task<ActionResult<ResponseModel<List<ExpenseDto>>>> GetByDate([FromQuery] DateTime date)
    {
      try
      {
        var expenses = await _expenseService.GetExpensesByDateAsync(date);
        return new ResponseModel<List<ExpenseDto>>
        {
          Data = expenses,
          Success = true,
          Message = $"Expenses for {date:yyyy-MM-dd}"
        };
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<List<ExpenseDto>>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to retrieve expenses by date."
        });
      }
    }

    [HttpGet("grouped-by-category/{categoryId:int}")]
    public async Task<ActionResult<ResponseModel<List<ExpenseGroupDto>>>> GetGroupedByCategory(int categoryId)
    {
      try
      {
        var grouped = await _expenseService.GetGroupedExpensesByCategoryAsync(categoryId);
        return new ResponseModel<List<ExpenseGroupDto>>
        {
          Data = grouped,
          Success = true,
          Message = $"Grouped expenses for category ID {categoryId}"
        };
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<List<ExpenseGroupDto>>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to group expenses by category."
        });
      }
    }
  }
}
