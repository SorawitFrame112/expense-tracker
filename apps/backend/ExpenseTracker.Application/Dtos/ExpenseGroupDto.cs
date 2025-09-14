
namespace ExpenseTracker.Application.Dtos.Response
{
  public class ExpenseGroupDto
  {
    public string CategoryName { get; set; }
    public List<ExpenseDto> Expenses { get; set; }
    public decimal TotalAmount => Expenses.Sum(e => e.Amount);
  }
}
