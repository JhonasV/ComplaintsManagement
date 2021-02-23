using ComplaintsManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class DepartmentsEntityTypeConfiguration : EntityTypeConfiguration<Deparments>
    {
        public DepartmentsEntityTypeConfiguration()
        {
            this.HasKey(e => e.Id);
            this.Property(e => e.Description).IsRequired().HasMaxLength(50);
        }
    }
}