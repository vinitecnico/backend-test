using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Web.Api.Entities;
using System.Linq;
using Web.Api.Help;
using System;

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

        // fazer o input dos dados acima, usando um arquivo no formato de log;
        // chamar os recursos de API e combinar os dados para processamento.
        public async Task<bool> UploadFileDbLog(IFormFile file)
        {
            string baseURL = _configuration.GetSection("BackendTest:BaseURL").Value;
            var extract = _loadingFileRepository.Handle(file);
            var resultPagamento = Insert(extract.pagamentos, "pagamentos");
            var resultRecebimento = Insert(extract.pagamentos, "recebimentos");
            var unionList = resultPagamento.Union(resultRecebimento);
            await Task.WhenAll(resultPagamento);
            return true;
        }

        public List<Task> Insert(List<MovementResult> movments, string type)
        {
            var tasks = new List<Task>();
            foreach (var item in movments)
            {
                string baseURL = _configuration.GetSection("BackendTest:BaseURL").Value;
                HttpContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var result = _client.PostAsync($"{baseURL}/{type}", content);
                tasks.Add(result);
            }

            return tasks;
        }

        public async Task<Extract> GetExtractAll()
        {
            var extract = new Extract();
            string baseURL = _configuration.GetSection("BackendTest:BaseURL").Value;
            var result = await _client.GetAsync($"{baseURL}/db");

            if (result.IsSuccessStatusCode)
            {
                extract = await result.Content.ReadAsAsync<Extract>();
            }

            return extract;
        }

        // exibir o log de movimentações de forma ordenada
        public async Task<List<Movement>> GetAllMovements()
        {
            var resul = await GetExtractAll();
            var extractList = resul.pagamentos.Union(resul.recebimentos);
            var util = new UtilHelp();
            var list = util.ConvertMovementResultToMovement(extractList.ToList());
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