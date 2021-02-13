﻿using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IComplaintsRepository
    {
        Task<TaskResult<ComplaintsDto>> SaveAsync(ComplaintsDto complaints);
        Task<TaskResult<ComplaintsDto>> GetAsync(int Id);
        Task<TaskResult<List<ComplaintsDto>>> GetAllAsync();
        Task<TaskResult<ComplaintsDto>> DeleteAsync(int Id);
        Task<TaskResult<ComplaintsDto>> UpdateAsync(ComplaintsDto complaints);
    }
}
