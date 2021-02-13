using ComplaintsManagement.Domain.Entities;
using ComplaintsManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Repositories
{
    public class CustomersProductsRepository : Repository<UsersProducts>, IUsersProductsRepository
    {
        public CustomersProductsRepository(DbSet<UsersProducts> entities) : base(entities)
        {
        }
    }
}