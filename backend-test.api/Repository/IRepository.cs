using System.Collections.Generic;
using backend_test.Entities;

namespace backend_test.Repository
{
    public interface IRepository
    {
        bool UploadFile();
        IEnumerable<Movement> GetAllMovements();
        Dictionary<string, decimal> TotalByCategory();
        Dictionary<string, decimal> CustomerCategorySpentMore();
        Dictionary<string, decimal> MonthCustomerCategorySpentMore();
        Dictionary<string, decimal> MoneyCustomerSpent();
        Dictionary<string, decimal> MoneyCustomerReceived();
    }
}