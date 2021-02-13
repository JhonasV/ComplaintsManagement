using ComplaintsManagement.Domain.Entities;
using ComplaintsManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Repositories
{
    public class ComplaintsRepository : Repository<Complaints>, IComplaintsRepository
    {
        public ComplaintsRepository(DbSet<Complaints> entities) : base(entities)
        {
        }
    }
}