using ComplaintsManagement.Domain.Entities;
using System.Data.Entity.ModelConfiguration;


namespace ComplaintsManagement.Infrastructure.Database
{
   
    public class CostumersProductsEntityTypeConfiguration : EntityTypeConfiguration<CustomersProducts>
    {
        public CostumersProductsEntityTypeConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.ApplicationUserId).IsRequired();
            this.Property(p => p.ProductsId).IsRequired();
        }
    }
}