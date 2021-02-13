using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.UI.Services.Repositories
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskResult<ComplaintsDto>> DeleteAsync(int Id)
        {
            var complaints = await _context.Complaints.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<ComplaintsDto>();
            try
            {
                if (complaints != null)
                {
                    complaints.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Queja borrado exitosamente!";
                    result.Data = new ComplaintsDto { Active = complaints.Active, CreatedAt = complaints.CreatedAt, CustomersId = complaints.CustomersId, Id = complaints.Id, UsersId = complaints.AgentId,  StatusId = complaints.StatusId, ProductsId = complaints.ProductsId };
                }
                else
                {
                    result.Message = "No se pudo encontrar la queja con el Id enviado";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<ComplaintsDto>>> GetAllAsync()
        {
           
            List<ComplaintsDto> complaintsDtos = new List<ComplaintsDto>();
            var result = new TaskResult<List<ComplaintsDto>>();
            try
            {
                var complaints = await _context.Complaints.Where(e => e.Active).ToListAsync();
                complaints.ForEach((complaint) =>
                {
                    complaintsDtos.Add(new ComplaintsDto { Active = complaint.Active, CreatedAt = complaint.CreatedAt, CustomersId = complaint.CustomersId, Id = complaint.Id, UsersId = complaint.AgentId, StatusId = complaint.StatusId, ProductsId = complaint.ProductsId });
                });
                result.Data = complaintsDtos;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de quejas: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<ComplaintsDto>> GetAsync(int Id)
        {
            var result = new TaskResult<ComplaintsDto>();

            try
            {
                var complaints = await _context.Complaints.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new ComplaintsDto { Active = complaints.Active, CreatedAt = complaints.CreatedAt, CustomersId = complaints.CustomersId, Id = complaints.Id, UsersId = complaints.AgentId, StatusId = complaints.StatusId, ProductsId = complaints.ProductsId };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;
             
        }

        public async Task<TaskResult<ComplaintsDto>> SaveAsync(ComplaintsDto complaintsDto)
        {
            var complaints = new Complaints { Active = complaintsDto.Active, CreatedAt = complaintsDto.CreatedAt, CustomersId = complaintsDto.CustomersId, Id = complaintsDto.Id, AgentId = complaintsDto.UsersId, StatusId = complaintsDto.StatusId, ProductsId = complaintsDto.ProductsId };
            var result = new TaskResult<ComplaintsDto>();
            try
            {
                _context.Complaints.Add(complaints);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó la queja #{complaints.Id}";
                
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la queja: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<ComplaintsDto>> UpdateAsync(ComplaintsDto complaintsDto)
        {
            var complaints = new Complaints { Active = complaintsDto.Active, CreatedAt = complaintsDto.CreatedAt, CustomersId = complaintsDto.CustomersId, Id = complaintsDto.Id, AgentId = complaintsDto.UsersId, StatusId = complaintsDto.StatusId, ProductsId = complaintsDto.ProductsId };
            var result = new TaskResult<ComplaintsDto>();
            try
            {
                _context.Complaints.Add(complaints);
                _context.Entry(complaints).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = complaintsDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información de la queja: {e.Message}";
            }
            return result;
        }
    }
}