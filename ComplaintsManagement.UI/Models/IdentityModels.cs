using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.Infrastructure.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace ComplaintsManagement.UI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            Id = Guid.NewGuid().ToString();
            return userIdentity;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public int? DepartmentId { get; set; }

        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public virtual Departments Department { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

  

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ComplaintsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new StatusEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new CostumersProductsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ClaimsOptionsEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ComplaintsOptionsEntityTypeConfiguration());
        }

        public virtual DbSet<Complaints> Complaints { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        public virtual DbSet<CustomersProducts> CustomersProducts { get; set; }
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<ClaimsOptions> ClaimsOptions { get; set; }
        public virtual DbSet<ComplaintsOptions> ComplaintsOptions { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<TicketTypes> ComplaintsTypes { get; set; }
        public virtual DbSet<Binnacle> Binnacles { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}