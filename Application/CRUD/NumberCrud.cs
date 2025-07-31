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
    public class NumberCrud
    {
        private readonly AddressBookDbContext _DbContext;

        public NumberCrud(AddressBookDbContext addressBookDbContext)
        {
            this._DbContext = addressBookDbContext;
        }
        public void Add(Number number)
        {
            _DbContext.numbers.Add(number);
            _DbContext.SaveChanges();
        }
        public void Update(Number number)
        {
            _DbContext.numbers.Update(number);
            _DbContext.SaveChanges();
        }
        public void Delete(Number number)
        {
            _DbContext.Remove(number);
            _DbContext.SaveChanges();
        }
        public List<Number> GetAllNumbers(Guid Id)
        {
            return _DbContext.numbers.Where(p => p.ContactId == Id).ToList();
        }
        public Number GetNumberById(Guid Id)
        {
            return _DbContext.numbers.Where(p => p.Id == Id).First();
        }
    }
}
