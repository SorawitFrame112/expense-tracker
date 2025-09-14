using AutoMapper;
using ExpenseTracker.Application.Dtos.Response;
using ExpenseTracker.Infrastructure.Repository;
using Infrastructure.Domain.Models;


namespace ExpenseTracker.Application.Service
{
  public class CategoryService : ICategoryService
  {
    private readonly ICategoryRepository _repository;
    private  readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository,IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
      var response = await _repository.GetAllAsync();
      return _mapper.Map<List<CategoryDto>>(response);
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
      var entity = await _repository.GetByIdAsync(id);
      return _mapper.Map<CategoryDto>(entity);
    }

    public async Task CreateAsync(CategoryDto categoryDto)
    {
      var entity = _mapper.Map<Category>(categoryDto);
      await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(int id, CategoryDto categoryDto)
    {
      var entity = _mapper.Map<Category>(categoryDto);
      await _repository.UpdateAsync(id, entity);
    }

    public async Task RemoveAsync(int id)
    {
      await _repository.RemoveAsync(id);
    }

  }

}
