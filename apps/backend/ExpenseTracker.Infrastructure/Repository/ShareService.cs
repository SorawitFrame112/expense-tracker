using ExpenseTracker.Infrastructure.Context;



namespace ExpenseTracker.Infrastructure.Repository
{

  public class ShareRepository<T> : IShareRepository<T> where T : class
  {
    private readonly ExpenseTrackerDbContext _context;

    public ShareRepository(ExpenseTrackerDbContext context)
    {
      _context = context;
    }

    public IQueryable<T> GetQueryable()
    {
      return _context.Set<T>();
    }
  }


}

