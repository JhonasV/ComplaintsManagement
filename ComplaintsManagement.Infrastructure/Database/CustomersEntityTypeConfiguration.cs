using ComplaintsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
   
    public class UsersProductsEntityTypeConfiguration : EntityTypeConfiguration<UsersProducts>
    {
        public UsersProductsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.UsersId).IsRequired();
            this.Property(p => p.ProductsId).IsRequired();
        }
    }
}