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
        public ExtractRepository(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _configuration = configuration;            
        }

        // fazer o input dos dados acima, usando um arquivo no formato de log;
        // chamar os recursos de API e combinar os dados para processamento.
        public async Task<bool> UploadFileDbLog(IFormFile file)
        {
            string baseURL = _configuration.GetSection("BackendTest:BaseURL").Value;
            LoadingFileRepository loadingfile = new LoadingFileRepository();
            var extract = loadingfile.Handle(file);
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

        public async Task<Extract> GetExtractAll(string baseURL)
        {
            var extract = new Extract();
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
            var resul = await GetExtractAll(_configuration.GetSection("BackendTest:BaseURL").Value);
            var extractList = resul.pagamentos.Union(resul.recebimentos);
            var util = new UtilHelp();
            var list = util.ConvertMovementResultToMovement(extractList.ToList());
            return list;
        }

        public async Task<List<Movement>> GetMovementAll(string type, string baseURL)
        {
            var result = await _client.GetAsync($"{baseURL}/{type}");
            var list = new List<Movement>();

            if (result.IsSuccessStatusCode)
            {
                var movements = await result.Content.ReadAsAsync<IEnumerable<MovementResult>>();
                var util = new UtilHelp();
                list = util.ConvertMovementResultToMovement(movements.ToList());
            }

            return list;
        }

        // informar o total de gastos por categoria;
        public async Task<Dictionary<string, double>> TotalByCategory()
        {
            var list = await GetMovementAll("pagamentos", _configuration.GetSection("BackendTest:BaseURL").Value);

            var items = list.GroupBy(x => x.categoria)
            .Select(x => new
            {
                Key = !string.IsNullOrEmpty(x.Key) ? x.Key : "sem categoria",
                value = Math.Round(x.Sum(y => y.valor), 2, MidpointRounding.AwayFromZero)
            })
            .OrderBy(x => x.value)
            .ToDictionary(x => x.Key, x => x.value);
            return items;
        }

        // informar qual categoria cliente gastou mais;
        public async Task<KeyValuePair<string, double>> CustomerCategorySpentMore()
        {
            var list = await GetMovementAll("pagamentos", _configuration.GetSection("BackendTest:BaseURL").Value);
            var items = list.GroupBy(x => x.categoria)
            .Select(x => new
            {
                Key = !string.IsNullOrEmpty(x.Key) ? x.Key : "sem categoria",
                value = Math.Round(x.Sum(y => y.valor), 2, MidpointRounding.AwayFromZero)
            })
            .OrderBy(x => x.value)
            .ToDictionary(x => x.Key, x => x.value);

            var result = items.FirstOrDefault();
            return result;
        }

        // informar qual foi o mês que cliente mais gastou;
        public async Task<KeyValuePair<string, double>> MonthCustomerCategorySpentMore()
        {
            var list = await GetMovementAll("pagamentos", _configuration.GetSection("BackendTest:BaseURL").Value);
            var items = list
            .Select(x => new
            {
                Month = x.data.Month,
                value = x.valor
            })
            .GroupBy(x => x.Month)
            .Select(x => new
            {
                Key = Convert.ToDateTime($"{DateTime.Now.Year}-{x.Key}-1").ToString("MMMM"),
                value = Math.Round(x.Sum(y => y.value), 2, MidpointRounding.AwayFromZero)
            })
            .OrderBy(x => x.value)
            .ToDictionary(x => x.Key, x => x.value);

            var result = items.FirstOrDefault();
            return result;
        }

        // quanto de dinheiro o cliente gastou;
        public async Task<double> MoneyCustomerSpent()
        {
            var list = await GetMovementAll("pagamentos", _configuration.GetSection("BackendTest:BaseURL").Value);
            var result = list.Sum(x => Math.Round(x.valor, 2, MidpointRounding.AwayFromZero));
            return result;
        }

        // quanto de dinheiro o cliente recebeu;
        public async Task<double> MoneyCustomerReceived()
        {
            var list = await GetMovementAll("recebimentos", _configuration.GetSection("BackendTest:BaseURL").Value);
            var result = list.Sum(x => Math.Round(x.valor, 2, MidpointRounding.AwayFromZero));
            return result;
        }

        // saldo total de movimentações do cliente.
        public async Task<double> TotalMovementCustomer()
        {
            var listRecebimentos = await GetMovementAll("recebimentos", _configuration.GetSection("BackendTest:BaseURL").Value);
            var totalRecebimentos = listRecebimentos.Sum(x => Math.Round(x.valor, 2, MidpointRounding.AwayFromZero));

            var listPagamentos = await GetMovementAll("pagamentos", _configuration.GetSection("BackendTest:BaseURL").Value);
            var totalPagamentos = listPagamentos.Sum(x => Math.Round(x.valor, 2, MidpointRounding.AwayFromZero));
            var result = totalPagamentos + totalRecebimentos;
            return result;
        }
    }
}