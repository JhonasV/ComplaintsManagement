using ComplaintsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class UsersEntityTypeConfiguration : EntityTypeConfiguration<Users>
    {
        public UsersEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(50);
            this.Property(p => p.Password).IsRequired().HasMaxLength(1000);
        }
    }

    public class UsersRolesEntityTypeConfiguration : EntityTypeConfiguration<UsersRoles>
    {
        public UsersRolesEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.RolesId).IsRequired();
            this.Property(p => p.UsersId).IsRequired();
        }
    }

    public class RolesEntityTypeConfiguration : EntityTypeConfiguration<Roles>
    {
        public RolesEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}