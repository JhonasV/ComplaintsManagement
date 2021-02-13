using ComplaintsManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
   
    public class CostumersProductsEntityTypeConfiguration : EntityTypeConfiguration<CostumersProducts>
    {
        public CostumersProductsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.CostumersId).IsRequired();
            this.Property(p => p.ProductsId).IsRequired();
        }
    }
}