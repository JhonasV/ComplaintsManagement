using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IDepartmentsRepository
    {
        Task<TaskResult<DepartmentsDto>> SaveAsync(DepartmentsDto departments);
        Task<TaskResult<DepartmentsDto>> GetAsync(int Id);
        Task<TaskResult<List<DepartmentsDto>>> GetAllAsync();
        Task<TaskResult<DepartmentsDto>> DeleteAsync(int Id);
        Task<TaskResult<DepartmentsDto>> UpdateAsync(DepartmentsDto departments);
    }
}
