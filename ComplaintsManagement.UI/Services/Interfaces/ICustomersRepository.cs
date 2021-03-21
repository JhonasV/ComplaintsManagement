using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface ICustomersRepository
    {
        Task<TaskResult<UsersDto>> SaveAsync(UsersDto costumers, ApplicationUserManager userManager);
        Task<TaskResult<UsersDto>> GetAsync(string Id);
        TaskResult<UsersDto> Get(string Id);
        Task<TaskResult<List<UsersDto>>> GetAllAsync();
        Task<TaskResult<List<UsersDto>>> GetAllAsync(ApplicationUserManager userManage);
        Task<TaskResult<UsersDto>> DeleteAsync(string Id);
        Task<TaskResult<UsersDto>> UpdateAsync(UsersDto costumers);
        Task<TaskResult<UsersDto>> GetByDocumentNumberAsync(string documentNumber);
    }
}
