using ComplaintsManagement.Domain.DTOs;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.UI.Services.Interfaces
{
    public interface ITicketTypesRepository
    {
        Task<TaskResult<List<TicketTypesDto>>> GetAllAsync();
        Task<TaskResult<TicketTypesDto>> GetAsync(int ticketId);
    }
}
