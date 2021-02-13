using ComplaintsManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IBinnacleRepository BinnacleRepository { get; set; }
        IClaimsOptionsRepository ClaimsOptionsRepository { get; set; }
        IComplaintsAgentsRepository ComplaintsAgentsRepository { get; set; }
        IComplaintsOptionsRepository ComplaintsOptionsRepository { get; set; }
        IComplaintsRepository ComplaintsRepository { get; set; }

        IProductsRepository ProductsRepository { get; set; }
        IRolesRepository RolesRepository { get; set; }
        IStatusRepository StatusRepository { get; set; }
        IUsersRepository UsersRepository { get; set; }
        IUsersRolesRepository UsersRolesRepository { get; set; }
        Task<int> SaveAsync();
    }
}
