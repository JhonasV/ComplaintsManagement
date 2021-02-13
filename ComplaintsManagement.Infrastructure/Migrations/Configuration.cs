namespace ComplaintsManagement.Infrastructure.Migrations
{
    using ComplaintsManagement.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ComplaintsManagement.Infrastructure.Database.ComplaintsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ComplaintsManagement.Infrastructure.Database.ComplaintsDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            IList<Roles> defaultRoles = new List<Roles>
            {
                new Roles { Name = "Admin" },
                new Roles { Name = "Agent" },
                new Roles { Name = "Client" }
            };

            context.Roles.AddRange(defaultRoles);
            context.Roles.AddOrUpdate();
            context.SaveChanges();

            IList<Users> defaultUsers = new List<Users>
            {
                new Users { Name = "Nelson", LastName = "Veras", Email = "nveras@complaints.com", Password = "@Hola1234", DocumentNumber = "12345465432" },
                new Users { Name = "Jose", LastName = "Perez", Email = "jperez@complaints.com", Password = "@Hola1234", DocumentNumber = "34324123123" },
                new Users { Name = "Juan", LastName = "Perez", Email = "juanperez@gmail.com", Password = "@Hola1234", DocumentNumber = "40208743738" }
            };

            context.Users.AddRange(defaultUsers);
            context.Users.AddOrUpdate();
            context.SaveChanges();

            IList<UsersRoles> defaultUsersRoles = new List<UsersRoles>
            {
                new UsersRoles { RolesId = 1, UsersId = 1 },
                new UsersRoles { RolesId = 2, UsersId = 1 },
                new UsersRoles { RolesId = 2, UsersId = 2 },
                new UsersRoles { RolesId = 3, UsersId = 3 }
            };

            context.UsersRoles.AddRange(defaultUsersRoles);
            context.UsersRoles.AddOrUpdate();
            context.SaveChanges();

            IList<Products> defaultProducts = new List<Products>
            {
                new Products { Name = "Internet", Description = "Plan Hogar 20 MB/s", Price = 1000 },
                new Products { Name = "Television por cable", Description = "Plan basico", Price = 800 },
                new Products { Name = "Teléfono", Description = "Plan basico", Price = 500 }
            };

            context.Products.AddRange(defaultProducts);
            context.Products.AddOrUpdate();
            context.SaveChanges();

            IList<UsersProducts> defaultUsersProducts = new List<UsersProducts>
            {
                new UsersProducts { UsersId = 3, ProductsId = 1 },
                new UsersProducts { UsersId = 3, ProductsId = 2 }
            };

            context.UsersProducts.AddRange(defaultUsersProducts);
            context.UsersProducts.AddOrUpdate();
            context.SaveChanges();

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
