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
    public class ServiceRateRepository : IServiceRateRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRateRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<TaskResult<ServiceRateDto>> DeleteAsync(int Id)
        {
            var serviceRate = await _context.ServiceRate.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<ServiceRateDto>();
            try
            {
                if (serviceRate != null)
                {
                    serviceRate.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Puntuacion borrada exitosamente!";
                    result.Data = AutoMapper.Mapper.Map<ServiceRateDto>(serviceRate);
                }
                else
                {
                    result.Message = "No se pudo encontrar la puntuacion";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<ServiceRateDto>>> GetAllAsync()
        {

            List<ServiceRateDto> serviceRateDtos = new List<ServiceRateDto>();
            var result = new TaskResult<List<ServiceRateDto>>();
            try
            {
                var serviceRates = await _context
                    .ServiceRate
                    .Include(e => e.Complaint)
                    .Where(e => e.Active)
                    .ToListAsync();

                var serviceRateDto = AutoMapper.Mapper.Map<List<ServiceRateDto>>(serviceRates);
                result.Data = serviceRateDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de puntuaciones: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<ServiceRateDto>> GetAsync(int Id)
        {
            var result = new TaskResult<ServiceRateDto>();

            try
            {
                var serviceRates = await _context
                    .ServiceRate
                    .Include(e => e.Complaint)
                    .Where(e => e.Active)
                    .FirstOrDefaultAsync(e => e.Id == Id && e.Active);

                result.Data = AutoMapper.Mapper.Map<ServiceRateDto>(serviceRates);
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información de la puntuacion: {e.Message}";
            }
            return result;

        }


        public async Task<TaskResult<ServiceRateDto>> GetByComplaintsIdAsync(int Id)
        {
            var result = new TaskResult<ServiceRateDto>();

            try
            {
                var serviceRates = await _context
                    .ServiceRate
                    .Include(e => e.Complaint)
                    .Where(e => e.Active)
                    .FirstOrDefaultAsync(e => e.ComplaintsId == Id && e.Active);

                result.Data = AutoMapper.Mapper.Map<ServiceRateDto>(serviceRates);
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información de la puntuacion: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<ServiceRateDto>> SaveAsync(ServiceRateDto serviceRateDto)
        {

            var serviceRate = AutoMapper.Mapper.Map<ServiceRate>(serviceRateDto);


            var result = new TaskResult<ServiceRateDto>();
            try
            {
                _context.ServiceRate.Add(serviceRate);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó la puntuacion #{serviceRate.Id}";
                result.Data = AutoMapper.Mapper.Map<ServiceRateDto>(serviceRate);
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la puntuacion: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<ServiceRateDto>> UpdateAsync(ServiceRateDto serviceRateDto)
        {

            var serviceRate = AutoMapper.Mapper.Map<ServiceRate>(serviceRateDto);
            serviceRate.Complaint = null;
            var result = new TaskResult<ServiceRateDto>();
            try
            {
 
                //_context.ServiceRate.Add(serviceRate);
                //_context.Entry(serviceRate).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = AutoMapper.Mapper.Map<ServiceRateDto>(serviceRate);
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información de la puntuacion: {e.Message}";
            }
            return result;
        }


    }
}