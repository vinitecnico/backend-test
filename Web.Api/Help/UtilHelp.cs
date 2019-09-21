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

namespace Web.Api.Help
{
    public class UtilHelp
    {
        public UtilHelp() {

        }

        public DateTime ConvertStringByDate(string value) {
            var dateList = value.Trim().Split("-");
            var day = dateList[0];
            var month = dateList[1];
            var year = DateTime.Now.ToString("yyyy");

            return DateTime.Parse($"{month} {day}, {year}");
        }
    }
}