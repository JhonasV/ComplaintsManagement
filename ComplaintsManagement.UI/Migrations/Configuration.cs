namespace ComplaintsManagement.UI.Migrations
{
    using ComplaintsManagement.Infrastructure.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ComplaintsManagement.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ComplaintsManagement.UI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Products.Any())
            {
                IList<Products> defaultProducts = new List<Products>
                {
                    new Products { Name = "Internet", Description = "Plan Hogar 20 MB/s", Price = 1000 },
                    new Products { Name = "Television por cable", Description = "Plan basico", Price = 800 },
                    new Products { Name = "Teléfono", Description = "Plan basico", Price = 500 }
                };

                context.Products.AddRange(defaultProducts);
                context.Products.AddOrUpdate();
                context.SaveChanges();
            }


            if (!context.ComplaintsOptions.Any())
            {
                IList<ComplaintsOptions> defaultComplaintsOptions = new List<ComplaintsOptions>
                {
                    new ComplaintsOptions { Name = "El internet es muy inestable", ProductsId = 1 },
                    new ComplaintsOptions { Name = "La velocidad de internet no es la contratada", ProductsId = 1 },
                    new ComplaintsOptions { Name = "Los canales de television se quedan frizados", ProductsId = 2 },
                    new ComplaintsOptions { Name = "No hay servicio de television cuando se muestran nubes en el cielo", ProductsId = 2 },
                    new ComplaintsOptions { Name = "No hay servicio de television cuando se muestran nubes en el cielo", ProductsId = 2 }
                };

                context.ComplaintsOptions.AddRange(defaultComplaintsOptions);
                context.ComplaintsOptions.AddOrUpdate();

                context.SaveChanges();
            } 
        }
    }
}
