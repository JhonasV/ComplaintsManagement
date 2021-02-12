

using ComplaintsManagement.Domain.DTOs;

namespace ComplaintsManagement.Domain.Services
{
    public interface IUsersService : IService<UsersDto>, IService<RolesDto>, IService<UsersRolesDto>
    {
    }
}
