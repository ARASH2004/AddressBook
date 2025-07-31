using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User.Register
{
    public class RegisterResponceDto
    {
        public bool IsResponce { get; set; }
        public string ErorMessage { get; set; }
        public Guid id { get; set; }
    }
}
