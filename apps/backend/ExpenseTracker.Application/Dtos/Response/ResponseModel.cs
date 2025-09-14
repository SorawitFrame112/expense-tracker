namespace ExpenseTracker.Application.Dtos.Response
{
  public class ResponseModel<T>
  {
    public bool Success { get; set; } = false;
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    public string Message { get; set; }
  }
}
