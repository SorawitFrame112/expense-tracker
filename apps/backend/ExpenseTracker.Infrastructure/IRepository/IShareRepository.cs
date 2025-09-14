


namespace ExpenseTracker.Infrastructure.Repository
{
  public interface IShareRepository<T>
  {
    IQueryable<T> GetQueryable();
    
  }
}
