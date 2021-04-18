using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IServiceRateRepository
    {
        Task<TaskResult<ServiceRateDto>> SaveAsync(ServiceRateDto serviceRate);
        Task<TaskResult<ServiceRateDto>> GetAsync(int Id);
        Task<TaskResult<ServiceRateDto>> GetByComplaintsIdAsync(int Id);
        Task<TaskResult<List<ServiceRateDto>>> GetAllAsync();
        Task<TaskResult<ServiceRateDto>> DeleteAsync(int Id);
        Task<TaskResult<ServiceRateDto>> UpdateAsync(ServiceRateDto serviceRate);
    }
}
