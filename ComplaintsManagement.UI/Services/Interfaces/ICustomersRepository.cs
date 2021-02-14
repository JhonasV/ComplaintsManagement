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
        Task<TaskResult<CustomersDto>> SaveAsync(CustomersDto costumers);
        Task<TaskResult<CustomersDto>> GetAsync(int Id);
        TaskResult<CustomersDto> Get(int Id);
        Task<TaskResult<List<CustomersDto>>> GetAllAsync();
        Task<TaskResult<CustomersDto>> DeleteAsync(int Id);
        Task<TaskResult<CustomersDto>> UpdateAsync(CustomersDto costumers);
    }
}
