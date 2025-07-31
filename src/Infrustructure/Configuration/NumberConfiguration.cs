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
    public class NumberConfiguration : IEntityTypeConfiguration<Number>
    {
        public void Configure(EntityTypeBuilder<Number> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.value).IsRequired().HasMaxLength(15);
            builder.HasOne(p=>p.Contact).WithMany(a=>a.Numbers).HasForeignKey(c=>c.ContactId).OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}
