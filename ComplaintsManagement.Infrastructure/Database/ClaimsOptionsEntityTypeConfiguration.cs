using ComplaintsManagement.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class ClaimsOptionsEntityTypeConfiguration : EntityTypeConfiguration<ClaimsOptions>
    {

        public ClaimsOptionsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }

    public class ComplaintsOptionsEntityTypeConfiguration : EntityTypeConfiguration<ComplaintsOptions>
    {
        public ComplaintsOptionsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }
}