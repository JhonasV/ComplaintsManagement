using ComplaintsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class ComplaintsDbContext : DbContext
    {
        public ComplaintsDbContext()
            : base("ComplaintsManagement")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new ComplaintsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new StatusEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UsersEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UsersRolesEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new RolesEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UsersProductsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ClaimsOptionsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ComplaintsOptionsEntityTypeConfiguration());
        }

        public virtual DbSet<Complaints> Complaints { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UsersRoles> UsersRoles { get; set; }
        public virtual DbSet<UsersProducts> UsersProducts { get; set; }
        public virtual DbSet<ClaimsOptions> ClaimsOptions { get; set; }
        public virtual DbSet<ComplaintsOptions> ComplaintsOptions { get; set; }

    }
}