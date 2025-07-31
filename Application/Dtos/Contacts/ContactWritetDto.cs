using Application.Dtos.Addresses;
using Application.Dtos.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Contacts
{
     public class ContactWriteDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AddressWriteDto> Addresses { get; set; }
        public List<NumberWriteDto> Numbers { get; set; }
    }
}
