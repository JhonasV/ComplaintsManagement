﻿using ComplaintsManagement.Domain.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface IComplaintsRepository
    {
        Task<TaskResult<ComplaintsDto>> SaveAsync(ComplaintsDto complaints);
        Task<TaskResult<ComplaintsDto>> GetAsync(int Id);
        Task<TaskResult<List<ComplaintsDto>>> GetAllAsync();
        Task<TaskResult<List<ComplaintsDto>>> GetAllByUserIdAsync(string userId);
        Task<TaskResult<ComplaintsDto>> DeleteAsync(int Id);
        Task<TaskResult<ComplaintsDto>> UpdateAsync(ComplaintsDto complaints);
        Task<TaskResult<ComplaintsDto>> UpdateStatusAsync(int statusId, int complaintsId);
        Task ExportAll(HttpResponseBase response);
        Task ExportPorcentage(HttpResponseBase response);
        Task ExportServiceRate(HttpResponseBase response);
    }
}
