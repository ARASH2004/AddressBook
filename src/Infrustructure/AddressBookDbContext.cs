using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
     public class AddressBookDbContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public AddressBookDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Number> numbers { get; set; }
        public DbSet<User> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString:_configuration.GetConnectionString("AddressBookConectionString"));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressBookDbContext).Assembly);
        }
       
        
    }
}
