using ComplaintsManagement.Domain.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IProductsRepository
    {
        Task<TaskResult<ProductsDto>> SaveAsync(ProductsDto products);
        Task<TaskResult<ProductsDto>> GetAsync(int Id);
        Task<TaskResult<List<ProductsDto>>> GetAllAsync();
        Task<TaskResult<ProductsDto>> DeleteAsync(int Id);
        Task<TaskResult<ProductsDto>> UpdateAsync(ProductsDto products);
    }
}
