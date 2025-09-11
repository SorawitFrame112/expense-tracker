using ExpenseTracker.Application.Service;
using Infrastructure.Domain.Models;
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

    [HttpGet]
    public async Task<ActionResult<List<Category>>> Get() => await _categoryService.GetAllAsync();

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
      var category = await _categoryService.GetByIdAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      return category;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Create(Category category)
    {
      await _categoryService.CreateAsync(category);

      return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Category categoryIn)
    {
      var category = await _categoryService.GetByIdAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      await _categoryService.UpdateAsync(id, categoryIn);

      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      var category = await _categoryService.GetByIdAsync(id);

      if (category == null)
      {
        return NotFound();
      }

      await _categoryService.RemoveAsync(id);

      return NoContent();
    }
  }
}
