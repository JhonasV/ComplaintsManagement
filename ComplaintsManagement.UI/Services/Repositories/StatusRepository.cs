using ComplaintsManagement.Infrastructure.Database;
using ComplaintsManagement.Domain.DTOs;
using ComplaintsManagement.Domain.Entities;
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

                    result.Data = AutoMapper.Mapper.Map<StatusDto>(status);

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
            var result = new TaskResult<List<StatusDto>>();
            try
            {
                var statuses = await _context.Status.Where(e => e.Active).ToListAsync();
                result.Data = AutoMapper.Mapper.Map<List<StatusDto>>(statuses);          
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
                result.Data = AutoMapper.Mapper.Map<StatusDto>(status);
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del status: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<StatusDto>> GetByNameAsync(string name)
        {
            var status = await _context.Status.FirstOrDefaultAsync(e => e.Name.ToUpper() == name.ToUpper() && e.Active);


            var result = new TaskResult<StatusDto>();

            result.Success = status != null;
            result.Data = AutoMapper.Mapper.Map<StatusDto>(status);

            return result;
        }

        public async Task<TaskResult<StatusDto>> SaveAsync(StatusDto statusDto)
        {
            var status = AutoMapper.Mapper.Map<Status>(statusDto);

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
            var status = AutoMapper.Mapper.Map<Status>(statusDto);
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