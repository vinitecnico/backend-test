using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Web.Api.Entities;

namespace Web.Api.Repository
{
    public interface IExtractRepository
    {
        bool UploadFileDbLog(IFormFile file);
        IEnumerable<Extract> GetAllMovements();
        Dictionary<string, decimal> TotalByCategory();
        Dictionary<string, decimal> CustomerCategorySpentMore();
        Dictionary<string, decimal> MonthCustomerCategorySpentMore();
        Dictionary<string, decimal> MoneyCustomerSpent();
        Dictionary<string, decimal> MoneyCustomerReceived();
    }
}