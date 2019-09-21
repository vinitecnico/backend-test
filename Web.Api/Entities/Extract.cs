using System.Collections.Generic;

namespace Web.Api.Entities
{
    public class Extract 
    {
        public List<Movement> Pagamentos { get; set; }
        public List<Movement> recebimentos { get; set; }
    }
}