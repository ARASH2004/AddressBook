using Domain.Entities;
using Infrustructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CRUD
{
    public class AddressCrud
    {
        private readonly AddressBookDbContext _dbContext;
        public AddressCrud(AddressBookDbContext addressBookDbContext) 
        {
            this._dbContext = addressBookDbContext;
        }
        public void Add(Address address)
        {
            _dbContext.addresses.Add(address);
            _dbContext.SaveChanges();
        }
        public void Delete(Guid Id)
        {
            var address = GetAddressById(Id);
            _dbContext.addresses.Remove(address);
            _dbContext.SaveChanges();
        }
        public void Update(Address address)
        {
            _dbContext.addresses.Update(address);
            _dbContext.SaveChanges();
        }
        // get all adress of one user
        public List<Address> GetAlladdresses(Guid Id)
        {
           return _dbContext.addresses.Where(p=>p.ContactId == Id).ToList();
        }
        public Address GetAddressById(Guid Id)
        {
            return _dbContext.addresses.Where(p=>p.Id==Id).First();
        }
    }
}
