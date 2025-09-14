using AutoMapper;
using ExpenseTracker.Application.Dtos;
using ExpenseTracker.Application.Dtos.Response;
using Infrastructure.Domain.Models;

namespace ExpenseTracker.Application.MappingProfile
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Category mapping
      CreateMap<Category, CategoryDto>().ReverseMap();
      CreateMap<CategoryDto, Category>().ReverseMap();

      // Expense mapping
      CreateMap<Expense, ExpenseDto>().ReverseMap();
      CreateMap<ExpenseDto, ExpenseDto>().ReverseMap();
    }
  }
}
