using ExpenseTracker.Application.Dtos;
using ExpenseTracker.Application.Dtos.Response;
using FluentValidation;

namespace ExpenseTracker.Application.Validator
{
  public class CategoryValidator : AbstractValidator<CategoryDto>
  {
    public CategoryValidator()
    {
      RuleFor(x => x.Id)
       .GreaterThanOrEqualTo(0).WithMessage("Id must be zero or positive");

      RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required")
        .MaximumLength(10).WithMessage("Name must be less than 10 characters");
    }
  }
}
