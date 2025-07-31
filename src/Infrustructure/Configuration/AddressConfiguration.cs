using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b=>b.Detail).IsRequired().HasMaxLength(500);
            builder.HasOne(p=>p.Contact).WithMany(b=>b.Addresses).HasForeignKey(c=>c.ContactId).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
