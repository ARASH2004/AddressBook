using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class LoginResponceDto
    {
        public bool IsResponce { get; set; }
        public string ErorMessage { get; set; }
        public string Token { get; set; }
        public Guid id { get; set; }
    }
}
