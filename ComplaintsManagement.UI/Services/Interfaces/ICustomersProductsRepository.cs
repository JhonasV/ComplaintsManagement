using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface ICustomersProductsRepository
    {
        Task<TaskResult<CustomersProductsDto>> SaveAsync(CustomersProductsDto costumers);
        Task<TaskResult<CustomersProductsDto>> GetAsync(int Id);
        Task<TaskResult<List<CustomersProductsDto>>> GetAllAsync();
        Task<TaskResult<List<CustomersProductsDto>>> GetAllByCustomerIdAsync(int customerId);
        Task<TaskResult<CustomersProductsDto>> DeleteAsync(int Id);
        Task<TaskResult<CustomersProductsDto>> UpdateAsync(CustomersProductsDto costumers);
    }
}
