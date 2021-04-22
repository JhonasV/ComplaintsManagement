
using ComplaintsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class ProductsEntityTypeConfiguration : EntityTypeConfiguration<Products>
    {
        public ProductsEntityTypeConfiguration()
        { 
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(100);
            this.Property(p => p.Price).IsRequired();
            this.Property(p => p.Description).IsRequired().HasMaxLength(500);
        }
    }
}