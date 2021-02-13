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
        Task<TaskResult<CostumersDto>> SaveAsync(CostumersDto costumers);
        Task<TaskResult<CostumersDto>> GetAsync(int Id);
        Task<TaskResult<List<CostumersDto>>> GetAllAsync();
        Task<TaskResult<CostumersDto>> DeleteAsync(int Id);
        Task<TaskResult<CostumersDto>> UpdateAsync(CostumersDto costumers);
    }
}
