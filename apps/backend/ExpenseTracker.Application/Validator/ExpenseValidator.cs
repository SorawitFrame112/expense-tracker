using ExpenseTracker.Application.Dtos;
using ExpenseTracker.Application.Dtos.Response;
using FluentValidation;

namespace ExpenseTracker.Application.Validator
{
  public class ExpenseValidator : AbstractValidator<ExpenseDto>
  {
    public ExpenseValidator()
    {
      RuleFor(x => x.Id)
      .GreaterThanOrEqualTo(0).WithMessage("Id must be zero or positive");

      RuleFor(x => x.Amount)
        .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than 0");

      RuleFor(x => x.Description)
        .NotEmpty().WithMessage("Description is required")
        .MaximumLength(100).WithMessage("Description must be less than 100 characters");

      RuleFor(x => x.Date)
        .LessThanOrEqualTo(DateTime.Today).WithMessage("Date cannot be in the future");
    }
  }
}
