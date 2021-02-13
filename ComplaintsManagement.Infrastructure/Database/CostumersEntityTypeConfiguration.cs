using ComplaintsManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class CostumersEntityTypeConfiguration : EntityTypeConfiguration<Costumers>
    {
        public CostumersEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(50);
            this.Property(p => p.Email).IsRequired().HasMaxLength(50);
            this.Property(p => p.DocumentNumber).IsRequired().HasMaxLength(50);
            this.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            this.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(50);
        }
    }
}