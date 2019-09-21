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
        Task<List<Movement>> GetMovementAll();
        Task<List<Movement>> GetAllMovements();
        Task<Dictionary<string, double>> TotalByCategory();
        Task<KeyValuePair<string, double>> CustomerCategorySpentMore();
        Task<KeyValuePair<string, double>> MonthCustomerCategorySpentMore();
        Dictionary<string, double> MoneyCustomerSpent();
        Dictionary<string, double> MoneyCustomerReceived();
    }
}