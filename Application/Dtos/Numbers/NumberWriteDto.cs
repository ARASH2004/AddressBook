using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Numbers
{
    public class NumberWriteDto
    {
        public string Value { get; set; }
        public Guid? ContactId { get; set; }

    }
}
