using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
namespace Domain.Entities
{
     public class Number:BaseEntitie
    {
        public  string value { get; set; }
        public NumberEnums numberType { get; set; }
        public Guid   ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}
