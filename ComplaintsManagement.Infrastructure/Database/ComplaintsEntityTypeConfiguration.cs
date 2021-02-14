using ComplaintsManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class ComplaintsEntityTypeConfiguration : EntityTypeConfiguration<Complaints>
    {
        public ComplaintsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.StatusId).IsRequired();
            this.Property(p => p.ProductsId).IsRequired();
            this.Property(p => p. Comment).IsRequired();
        }
    }
}