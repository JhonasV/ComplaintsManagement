using ComplaintsManagement.Domain.Entities;
using ComplaintsManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace ComplaintsManagement.Infrastructure.Repositories
{
    public class RolesRepository : Repository<Roles>, IRolesRepository
    {
        public RolesRepository(DbSet<Roles> roles) : base(roles)
        { }
    }
}