using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Contacts
{
     public class ContactReadDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Addresses { get; set; }
        public List<string> Numbers { get; set; }
    }
}
