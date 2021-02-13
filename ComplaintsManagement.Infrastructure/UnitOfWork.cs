using ComplaintsManagement.Domain;
using ComplaintsManagement.Domain.Repositories;
using ComplaintsManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ComplaintsDbContext _context;

        public IBinnacleRepository BinnacleRepository { get; set; }
        public IClaimsOptionsRepository ClaimsOptionsRepository { get; set; }
        public IComplaintsAgentsRepository ComplaintsAgentsRepository { get; set; }
        public IComplaintsOptionsRepository ComplaintsOptionsRepository { get; set; }
        public IComplaintsRepository ComplaintsRepository { get; set; }

        public IProductsRepository ProductsRepository { get; set; } 
        public IRolesRepository RolesRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public IUsersRepository UsersRepository { get; set; }
        public IUsersRolesRepository UsersRolesRepository { get; set; }

        public UnitOfWork(ComplaintsDbContext context, IBinnacleRepository binnacleRepository,
                           IClaimsOptionsRepository claimsOptionsRepository, IComplaintsAgentsRepository complaintsAgentsRepository,
                           IComplaintsOptionsRepository complaintsOptionsRepository, IComplaintsRepository complaintsRepository,

                           IRolesRepository rolesRepository, IStatusRepository statusRepository,
                           IUsersRepository usersRepository, IUsersRolesRepository usersRolesRepository)
        {
            _context = context;
            BinnacleRepository = binnacleRepository;
            ClaimsOptionsRepository = claimsOptionsRepository;
            ComplaintsAgentsRepository = complaintsAgentsRepository;
            ComplaintsOptionsRepository = complaintsOptionsRepository;
            ComplaintsRepository = complaintsRepository;


            RolesRepository = rolesRepository;
            StatusRepository = statusRepository;
            UsersRepository = usersRepository;
            UsersRolesRepository = usersRolesRepository;
        }
        public void Dispose() => _context.Dispose();

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        
    }
}