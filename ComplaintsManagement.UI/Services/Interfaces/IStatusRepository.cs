using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IStatusRepository
    {
        Task<TaskResult<StatusDto>> SaveAsync(StatusDto statusDto);
        Task<TaskResult<StatusDto>> GetAsync(int Id);
        Task<TaskResult<StatusDto>> GetByNameAsync(string name);
        Task<TaskResult<List<StatusDto>>> GetAllAsync();
        Task<TaskResult<StatusDto>> DeleteAsync(int Id);
        Task<TaskResult<StatusDto>> UpdateAsync(StatusDto statusDto);
    }
}
