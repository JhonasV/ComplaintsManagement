using ComplaintsManagement.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class DepartmentsEntityTypeConfiguration : EntityTypeConfiguration<Departments>
    {
        public DepartmentsEntityTypeConfiguration()
        {
            this.HasKey(e => e.Id);
            this.Property(e => e.Description).IsRequired().HasMaxLength(50);
        }
    }
}