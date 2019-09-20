using System.Collections.Generic;
using System.Threading.Tasks;
using backend_test.Entities;

namespace backend_test.Repository
{
    public class MovementRepository : IRepository
    {
        public MovementRepository()
        {
        }

        public Dictionary<string, decimal> CustomerCategorySpentMore()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movement> GetAllMovements()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, decimal> MoneyCustomerReceived()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, decimal> MoneyCustomerSpent()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, decimal> MonthCustomerCategorySpentMore()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, decimal> TotalByCategory()
        {
            throw new System.NotImplementedException();
        }

        public bool UploadFile()
        {
            throw new System.NotImplementedException();
        }
    }
}