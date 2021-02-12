using ComplaintsManagement.Domain.Repositories;
using ComplaintsManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace ComplaintsManagement.Infrastructure.Configurations
{
    public static class IoCConfiguration
    {
        public static void Init(UnityContainer container)
        {
            //      private readonly ComplaintsManagementContext _context;

            //public IBinnacleRepository BinnacleRepository { get; set; }
            //public IClaimsOptionsRepository ClaimsOptionsRepository { get; set; }
            //public IComplaintsAgentsRepository ComplaintsAgentsRepository { get; set; }
            //public IComplaintsOptionsRepository ComplaintsOptionsRepository { get; set; }
            //public IComplaintsRepository ComplaintsRepository { get; set; }
            //public ICustomersRepository CustomersRepository { get; set; }
            //public IProductsRepository ProductsRepository { get; set; }
            //public IRolesRepository RolesRepository { get; set; }
            //public IStatusRepository StatusRepository { get; set; }
            //public IUsersRepository UsersRepository { get; set; }
            //public IUsersRolesRepository UsersRolesRepository { get; set; }

            container.RegisterType<ComplaintsManagementContext>();
            //container.RegisterType<IClaimsOptionsRepository, ClaimsOptionsRepository>();
        }
    }
}