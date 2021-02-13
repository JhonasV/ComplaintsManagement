using ComplaintsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Database
{
    public class DBInitializer : DropCreateDatabaseAlways<ComplaintsDbContext>
    {
        protected override void Seed(ComplaintsDbContext context)
        {
            IList<Roles> defaultRoles = new List<Roles>();
            defaultRoles.Add(new Roles { Name = "Admin" });
            defaultRoles.Add(new Roles { Name = "Agent" });
            defaultRoles.Add(new Roles { Name = "Client" });

            context.Roles.AddRange(defaultRoles);

            IList<Users> defaultUsers = new List<Users>();
            defaultUsers.Add(new Users { Name = "Nelson", LastName = "Veras", Email = "nveras@complaints.com", Password = "@Hola1234", DocumentNumber="12345465432" });
            defaultUsers.Add(new Users { Name = "Jose", LastName = "Perez", Email = "jperez@complaints.com", Password = "@Hola1234", DocumentNumber = "34324123123" });
            defaultUsers.Add(new Users { Name = "Juan", LastName = "Perez", Email = "juanperez@gmail.com", Password = "@Hola1234", DocumentNumber = "40208743738" });

            context.Users.AddRange(defaultUsers);

            IList<UsersRoles> defaultUsersRoles = new List<UsersRoles>();
            defaultUsersRoles.Add(new UsersRoles { RolesId = 1, UsersId = 1 });
            defaultUsersRoles.Add(new UsersRoles { RolesId = 2, UsersId = 1 });
            defaultUsersRoles.Add(new UsersRoles { RolesId = 2, UsersId = 2 });
            defaultUsersRoles.Add(new UsersRoles { RolesId = 3, UsersId = 3 });

            context.UsersRoles.AddRange(defaultUsersRoles);

            IList<Products> defaultProducts = new List<Products>();

            defaultProducts.Add(new Products { Name = "Internet", Description = "Plan Hogar 20 MB/s", Price = 1000 });
            defaultProducts.Add(new Products { Name = "Television por cable", Description = "Plan basico", Price = 800 });
            defaultProducts.Add(new Products { Name = "Teléfono", Description = "Plan basico", Price = 500 });

            context.Products.AddRange(defaultProducts);

            IList<UsersProducts> defaultUsersProducts = new List<UsersProducts>();

            defaultUsersProducts.Add(new UsersProducts { UsersId = 3, ProductsId = 1 });
            defaultUsersProducts.Add(new UsersProducts { UsersId = 3, ProductsId = 2 });

            context.UsersProducts.AddRange(defaultUsersProducts);

            IList<ComplaintsOptions> defaultComplaintsOptions = new List<ComplaintsOptions>();

            defaultComplaintsOptions.Add(new ComplaintsOptions { Name = "El internet es muy inestable", ProductsId = 1 });
            defaultComplaintsOptions.Add(new ComplaintsOptions { Name = "La velocidad de internet no es la contratada", ProductsId = 1 });
            defaultComplaintsOptions.Add(new ComplaintsOptions { Name = "Los canales de television se quedan frizados", ProductsId = 2 });
            defaultComplaintsOptions.Add(new ComplaintsOptions { Name = "No hay servicio de television cuando se muestran nubes en el cielo", ProductsId = 2 });
            defaultComplaintsOptions.Add(new ComplaintsOptions { Name = "No hay servicio de television cuando se muestran nubes en el cielo", ProductsId = 2 });

            context.ComplaintsOptions.AddRange(defaultComplaintsOptions);

            base.Seed(context);
        }
    }
}