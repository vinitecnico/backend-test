using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Web.Api.Entities;

namespace Web.Api.Repository
{
    public class ExtractRepository : IExtractRepository
    {
        private HttpClient _client;
        private IConfiguration _configuration;
        private readonly ILoadingFileRepository _loadingFileRepository;
        public ExtractRepository(IConfiguration configuration, HttpClient client, ILoadingFileRepository loadingFileRepository)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _configuration = configuration;
            _loadingFileRepository = loadingFileRepository;
        }

        public async Task<bool> UploadFileDbLog(IFormFile file)
        {
            string baseURL = _configuration.GetSection("BackendTest:BaseURL").Value;
            var extract = _loadingFileRepository.Handle(file);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(extract), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync($"{baseURL}/db", content);
            return true;
        }

        public async Task<List<Extract>> GetAllMovements()
        {
            var list = new List<Extract>();
            string baseURL = _configuration.GetSection("BackendTest:BaseURL").Value;
            var result = await _client.GetAsync($"{baseURL}/db");

            if (result.IsSuccessStatusCode)
            {
               var response = await result.Content.ReadAsAsync<Extract>();
            }

            return list;
        }

        public Dictionary<string, decimal> CustomerCategorySpentMore()
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
    }
}