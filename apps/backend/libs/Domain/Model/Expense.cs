namespace Domain;

public class Expense
{
  public string Id { get; set; }
  public string Description { get; set; }
  public decimal Amount { get; set; }
  public DateTime Date { get; set; }
  public string CategoryId { get; set; }
}
