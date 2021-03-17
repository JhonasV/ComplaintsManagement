using ComplaintsManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
   
    public class CostumersProductsEntityTypeConfiguration : EntityTypeConfiguration<CustomersProducts>
    {
        public CostumersProductsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.ApplicationUserId).IsRequired();
            this.Property(p => p.ProductsId).IsRequired();
        }
    }
}