using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
     public class Address:BaseEntitie
    {
        public string Detail {  get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
