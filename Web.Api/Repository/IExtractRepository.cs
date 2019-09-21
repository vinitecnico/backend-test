using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Api.Entities;

namespace Web.Api.Repository
{
    public interface IExtractRepository
    {
        Task<bool> UploadFileDbLog(IFormFile file);
        List<Task> Insert(List<MovementResult> movments, string type);
        Task<Extract> GetExtractAll();
        Task<List<Movement>> GetAllMovements();
        Dictionary<string, decimal> TotalByCategory();
        Dictionary<string, decimal> CustomerCategorySpentMore();
        Dictionary<string, decimal> MonthCustomerCategorySpentMore();
        Dictionary<string, decimal> MoneyCustomerSpent();
        Dictionary<string, decimal> MoneyCustomerReceived();
    }
}