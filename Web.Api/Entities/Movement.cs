using System.Collections.Generic;

namespace Web.Api.Entities
{
    public class Movement 
    {
        public string Data { get; set; }
        public string Descricao { get; set; }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }
        public string categoria { get; set; }
    }
}