using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IRolesRepository
    {
        Task<TaskResult<RolesDto>> SaveAsync(RolesDto role);
        Task<TaskResult<RolesDto>> GetAsync(string Id);
        Task<TaskResult<List<RolesDto>>> GetAllAsync();
        Task<TaskResult<RolesDto>> DeleteAsync(string Id);
        Task<TaskResult<RolesDto>> UpdateAsync(RolesDto statusDto);
        Task<TaskResult<RolesDto>> AddUserRoleAsync(string userId, string roleName);
        Task<TaskResult<List<RolesDto>>> GetUserRolesAsync(string userId, ApplicationUserManager userManager);
    }
}
