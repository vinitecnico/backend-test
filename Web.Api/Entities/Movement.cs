using System.Collections.Generic;

namespace Web.Api.Entities
{
    public class Movement 
    {
        public string data { get; set; }
        public string descricao { get; set; }
        public string moeda { get; set; }
        public string valor { get; set; }
        public string categoria { get; set; }
    }
}