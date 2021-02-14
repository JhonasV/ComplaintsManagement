using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
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
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskResult<StatusDto>> DeleteAsync(int Id)
        {
            var status = await _context.Status.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<StatusDto>();
            try
            {
                if (status != null)
                {
                    status.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Status borrado exitosamente!";
                    result.Data = new StatusDto
                    {
                        Active = status.Active,
                        CreatedAt = status.CreatedAt,
                        Id = status.Id,
                        Name = status.Name,
                        UpdatedAt = status.UpdatedAt
                    };

            }
                else
                {
                    result.Message = "No se pudo encontrar el status con el Id enviado";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<StatusDto>>> GetAllAsync()
        {

            List<StatusDto> statusDto = new List<StatusDto>();
            var result = new TaskResult<List<StatusDto>>();
            try
            {
                var statuses = await _context.Status.Where(e => e.Active).ToListAsync();
                statuses.ForEach((status) =>
                {
                    statusDto.Add(new StatusDto
                        {
                            Active = status.Active,
                            CreatedAt = status.CreatedAt,
                            Id = status.Id,
                            Name = status.Name,
                            UpdatedAt = status.UpdatedAt
                        });
                });
                result.Data = statusDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de status: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<StatusDto>> GetAsync(int Id)
        {
            var result = new TaskResult<StatusDto>();

            try
            {
                var status = await _context.Status.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new StatusDto
                {
                    Active = status.Active,
                    CreatedAt = status.CreatedAt,
                    Id = status.Id,
                    Name = status.Name,
                    UpdatedAt = status.UpdatedAt
                };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del status: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<StatusDto>> SaveAsync(StatusDto statusDto)
        {
            var status = new Status
            {
                Active = statusDto.Active,
                CreatedAt = statusDto.CreatedAt,
                Id = statusDto.Id,
                Name = statusDto.Name,
                UpdatedAt = statusDto.UpdatedAt
            };
            var result = new TaskResult<StatusDto>();
            try
            {
                _context.Status.Add(status);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó el status {status.Name}";

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar al Status: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<StatusDto>> UpdateAsync(StatusDto statusDto)
        {
            var status = new Status
            {
                Active = statusDto.Active,
                CreatedAt = statusDto.CreatedAt,
                Id = statusDto.Id,
                Name = statusDto.Name,
                UpdatedAt = statusDto.UpdatedAt
            };

            var result = new TaskResult<StatusDto>();
            try
            {
                _context.Status.Add(status);
                _context.Entry(status).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = statusDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información del status: {e.Message}";
            }
            return result;
        }
    }
}