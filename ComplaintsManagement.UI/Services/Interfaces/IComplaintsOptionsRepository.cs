using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IComplaintsOptionsRepository
    {
        Task<TaskResult<ComplaintsOptionsDto>> SaveAsync(ComplaintsOptionsDto complaints);
        Task<TaskResult<ComplaintsOptionsDto>> GetAsync(int Id);
        Task<TaskResult<List<ComplaintsOptionsDto>>> GetAllAsync();
        Task<TaskResult<ComplaintsOptionsDto>> DeleteAsync(int Id);
        Task<TaskResult<ComplaintsOptionsDto>> UpdateAsync(ComplaintsOptionsDto complaints);
    }
}
