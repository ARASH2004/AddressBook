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
    public class ContactCrud
    {
        private readonly AddressBookDbContext _dbContext;

        public ContactCrud(AddressBookDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Add(Contact contact)
        {
            _dbContext.contacts.Add(contact);
            _dbContext.SaveChanges();
        }
        public void Delete(Contact contact) 
        { 
            _dbContext.contacts.Remove(contact);
            _dbContext.SaveChanges();
        }
        public void Update(Contact contact)
        {
            _dbContext.contacts.Update(contact);
            _dbContext.SaveChanges();
        }
        public List<Contact> GetAll()
        {
            return _dbContext.contacts
                .Include(p => p.Addresses)
                .Include(p => p.Numbers).ToList();
        }
        public Contact GetById(Guid Id)
        {
            return _dbContext.contacts
                .Include(p=>p.Addresses)
                .Include(p=>p.Numbers)
                .Where(p=>p.Id == Id).First();
        }
        
        public Contact GetByName( string firstname ,string lastname)
        {
            return _dbContext.contacts
                .Include(p => p.Addresses)
                .Include(p => p.Numbers)
                .Where(p => p.FirstName == firstname && p.LastName == lastname).First();
        }


    }
}
