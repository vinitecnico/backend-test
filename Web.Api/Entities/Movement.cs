using System;

namespace Web.Api.Entities
{
    public class Movement 
    {
        public DateTime data { get; set; }
        public string descricao { get; set; }
        public string moeda { get; set; }
        public double valor { get; set; }
        public string categoria { get; set; }
    }
}