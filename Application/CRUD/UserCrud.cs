using Domain.Entities;
using Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CRUD
{
    public class UserCrud
    {
        private readonly AddressBookDbContext _DbContext;

        public UserCrud(AddressBookDbContext addressBookDbContext)
        {
            this._DbContext = addressBookDbContext;
        }
        public void Add(User user)
        {
            _DbContext.users.Add(user);
            _DbContext.SaveChanges();
        }
        public void Update(User user)
        {
            _DbContext.users.Update(user);
            _DbContext.SaveChanges();
        }
        public void Delete(User user)
        {
            _DbContext.Remove(user);
            _DbContext.SaveChanges();
        }
        public User GetUserByUserName(string username)
        {
            return _DbContext.users.FirstOrDefault(p => p.UserName == username);
        }
    }

}
