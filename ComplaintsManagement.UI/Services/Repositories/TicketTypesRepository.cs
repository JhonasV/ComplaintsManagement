using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.UI.Services.Repositories
{
    public class TicketTypesRepository : ITicketTypesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TicketTypesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TaskResult<List<TicketTypesDto>>> GetAllAsync()
        {
            var result = new TaskResult<List<TicketTypesDto>>();
            try
            {
                var ticketTypes = await _dbContext.ComplaintsTypes.ToListAsync();
                result.Data = AutoMapper.Mapper.Map<List<TicketTypesDto>>(ticketTypes);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException.Message;
            }

            return result;
        }

        public async Task<TaskResult<TicketTypesDto>> GetAsync(int ticketId)
        {
            var result = new TaskResult<TicketTypesDto>();
            try
            {
                var ticketTypes = await _dbContext.ComplaintsTypes.FirstOrDefaultAsync(e => e.Id == ticketId);
                result.Data = AutoMapper.Mapper.Map<TicketTypesDto>(ticketTypes);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.InnerException.Message;
            }

            return result;
        }
    }
}