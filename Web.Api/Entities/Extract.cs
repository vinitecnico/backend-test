using System.Collections.Generic;

namespace Web.Api.Entities
{
    public class Extract 
    {
        public List<Movement> pagamentos { get; set; }
        public List<Movement> recebimentos { get; set; }
    }
}