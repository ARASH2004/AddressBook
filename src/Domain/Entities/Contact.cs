using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact:BaseEntitie
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address > Addresses { get; set; }
        public List<Number> Numbers { get; set; }


    }
}
