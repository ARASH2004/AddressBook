using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:BaseEntitie
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

    }
}
