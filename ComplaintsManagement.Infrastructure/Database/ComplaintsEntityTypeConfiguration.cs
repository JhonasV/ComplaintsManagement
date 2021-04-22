using ComplaintsManagement.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

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