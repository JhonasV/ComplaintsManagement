using ComplaintsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class CustomersEntityTypeConfiguration : EntityTypeConfiguration<Customers>
    {
        public CustomersEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.FullName).IsRequired().HasMaxLength(50);
            this.Property(p => p.DocumentNumber).IsRequired().HasMaxLength(20);
            this.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
            this.Property(p => p.Email).IsRequired().HasMaxLength(50);
        }
    }

    public class CustomersProductsEntityTypeConfiguration : EntityTypeConfiguration<CustomersProducts>
    {
        public CustomersProductsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.CustomersId).IsRequired();
            this.Property(p => p.ProductsId).IsRequired();
        }
    }
}