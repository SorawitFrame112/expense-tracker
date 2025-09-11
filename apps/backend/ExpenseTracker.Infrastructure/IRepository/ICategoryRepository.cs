using ExpenseTracker.Infrastructure.Context;
using Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repository
{
  public interface ICategoryRepository: IBaseRepository<Category>
  {
   
  }
}
