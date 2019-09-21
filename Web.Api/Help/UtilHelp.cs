using System;
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

namespace Web.Api.Help
{
    public class UtilHelp
    {
        public UtilHelp()
        {

        }

        public DateTime ConvertStringByDate(string value)
        {
            var dateList = value.Trim().Split("/");
            var day = dateList[0];
            var month = dateList[1];
            var year = DateTime.Now.ToString("yyyy");

            return DateTime.Parse($"{month} {day}, {year}");
        }

        public List<Movement> ConvertMovementResultToMovement(List<MovementResult> result)
        {
            var list = new List<Movement>();
            foreach (var item in result)
            {
                var movement = new Movement()
                {
                    data = !string.IsNullOrEmpty(item.data) ? ConvertStringByDate(item.data) : DateTime.Now,
                    descricao = !string.IsNullOrEmpty(item.descricao) ? item.descricao : null,
                    moeda = !string.IsNullOrEmpty(item.moeda) ? item.moeda : "R$",
                    valor = !string.IsNullOrEmpty(item.valor) ? Convert.ToDouble(item.valor.Replace(" ", "")) : 0,
                    categoria = !string.IsNullOrEmpty(item.categoria) ? item.categoria : null
                };

                list.Add(movement);
            }
            return list.OrderByDescending(x => x.data).ToList();
        }
    }
}