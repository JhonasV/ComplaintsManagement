using ComplaintsManagement.Domain.Entities;
using ComplaintsManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Repositories
{
    public class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(DbSet<Customers> customers):base(customers)
        {

        }
    }
}