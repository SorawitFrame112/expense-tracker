using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Dtos.Response
{
  public class ExpenseSummaryByTopDto
  {
    public int totalAmount { get; set; }
    public List<CategoryDto> Categories { get; set; }

  }
}
