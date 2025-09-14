using ExpenseTracker.Application.Service;
using ExpenseTracker.Application.Dtos.Response;
using ExpenseTracker.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
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
    public async Task<ActionResult<ResponseModel<List<CategoryDto>>>> Get()
    {
      return await HandleRequestAsync(() => _categoryService.GetAllAsync());
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<ResponseModel<CategoryDto>>> GetById(int id)
    {
      try
      {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null)
        {
          return NotFound(new ResponseModel<CategoryDto>
          {
            Success = false,
            Message = "Category not found"
          });
        }

        return new ResponseModel<CategoryDto>
        {
          Data = category,
          Success = true
        };
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<CategoryDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to retrieve category."
        });
      }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<CategoryDto>>> Create(CategoryDto categoryDto)
    {
      try
      {
        await _categoryService.CreateAsync(categoryDto);
        return CreatedAtRoute("GetCategory", new { id = categoryDto.Id }, new ResponseModel<CategoryDto>
        {
          Data = categoryDto,
          Success = true,
          Message = "Category created successfully"
        });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<CategoryDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to create category."
        });
      }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryDto categoryDto)
    {
      try
      {
        var existing = await _categoryService.GetByIdAsync(id);
        if (existing == null)
        {
          return NotFound(new ResponseModel<CategoryDto>
          {
            Success = false,
            Message = "Category not found"
          });
        }

        await _categoryService.UpdateAsync(id, categoryDto);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<CategoryDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to update category."
        });
      }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var existing = await _categoryService.GetByIdAsync(id);
        if (existing == null)
        {
          return NotFound(new ResponseModel<CategoryDto>
          {
            Success = false,
            Message = "Category not found"
          });
        }

        await _categoryService.RemoveAsync(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new ResponseModel<CategoryDto>
        {
          Success = false,
          Errors = new[] { ex.Message },
          Message = "Failed to delete category."
        });
      }
    }
  }
}
