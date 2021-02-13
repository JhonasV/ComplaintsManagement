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
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskResult<CostumersDto>> DeleteAsync(int Id)
        {
            var customer = await _context.Costumers.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<CostumersDto>();
            try
            {
                if (customer != null)
                {
                    customer.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Cliente borrado exitosamente!";
                    result.Data = new CostumersDto { Active = customer.Active, CreatedAt = customer.CreatedAt, Email = customer.Email, Id = customer.Id, LastName = customer.LastName, Name = customer.Name, UpdatedAt = customer.UpdatedAt };
                }
                else
                {
                    result.Message = "No se pudo encontrar cliente con el Id enviado";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<CostumersDto>>> GetAllAsync()
        {
           
            List<CostumersDto> costumersDtos = new List<CostumersDto>();
            var result = new TaskResult<List<CostumersDto>>();
            try
            {
                var customers = await _context.Costumers.Where(e => e.Active).ToListAsync();
                customers.ForEach((customer) =>
                {
                    costumersDtos.Add(new CostumersDto { Active = customer.Active, CreatedAt = customer.CreatedAt, Email = customer.Email, Id = customer.Id, LastName = customer.LastName, Name = customer.Name, UpdatedAt = customer.UpdatedAt, PhoneNumber = customer.PhoneNumber, DocumentNumber = customer.DocumentNumber });
                });
                result.Data = costumersDtos;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de clientes: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<CostumersDto>> GetAsync(int Id)
        {
            var result = new TaskResult<CostumersDto>();

            try
            {
                var costumer = await _context.Costumers.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new CostumersDto { Active = costumer.Active, CreatedAt = costumer.CreatedAt, Email = costumer.Email, Id = costumer.Id, LastName = costumer.LastName, Name = costumer.Name, UpdatedAt = costumer.UpdatedAt };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;
             
        }

        public async Task<TaskResult<CostumersDto>> SaveAsync(CostumersDto costumerDto)
        {
            var costumer = new Costumers { Active = costumerDto.Active, CreatedAt = costumerDto.CreatedAt, DocumentNumber = costumerDto.DocumentNumber, Email = costumerDto.Email, Id = costumerDto.Id, LastName = costumerDto.LastName, Name = costumerDto.Name, PhoneNumber = costumerDto.PhoneNumber, UpdatedAt = costumerDto.UpdatedAt };
            var result = new TaskResult<CostumersDto>();
            try
            {
                _context.Costumers.Add(costumer);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó el cliente {costumer.Name} {costumer.LastName}";
                
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar al cliente: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<CostumersDto>> UpdateAsync(CostumersDto costumerDto)
        {
            var costumer = new Costumers { Active = costumerDto.Active, CreatedAt = costumerDto.CreatedAt, DocumentNumber = costumerDto.DocumentNumber, Email = costumerDto.Email, Id = costumerDto.Id, LastName = costumerDto.LastName, Name = costumerDto.Name, PhoneNumber = costumerDto.PhoneNumber, UpdatedAt = costumerDto.UpdatedAt };
            var result = new TaskResult<CostumersDto>();
            try
            {
                _context.Costumers.Add(costumer);
                _context.Entry(costumer).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = costumerDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información del cliente: {e.Message}";
            }
            return result;
        }
    }
}