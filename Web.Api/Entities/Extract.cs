using System.Collections.Generic;

namespace Web.Api.Entities
{
    public class Extract 
    {
        public List<MovementResult> pagamentos { get; set; }
        public List<MovementResult> recebimentos { get; set; }        
    }
}