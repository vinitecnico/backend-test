using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Web.Api.Entities;

namespace Web.Api.Repository
{
    public class ExtractRepository : IExtractRepository
    {
        private readonly ILoadingFileRepository _loadingFileRepository;
        public ExtractRepository(ILoadingFileRepository loadingFileRepository)
        {
            _loadingFileRepository = loadingFileRepository;
        }

        public bool UploadFileDbLog(IFormFile file)
        {
            var extract = _loadingFileRepository.Handle(file);
            throw new System.NotImplementedException();
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

        IEnumerable<Extract> IExtractRepository.GetAllMovements()
        {
            throw new System.NotImplementedException();
        }
    }
}