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
        public async Task<TaskResult<UsersDto>> DeleteAsync(string Id)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<UsersDto>();
            try
            {
                if (customer != null)
                {
                    customer.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Cliente borrado exitosamente!";
                    result.Data = new UsersDto { Active = customer.Active, CreatedAt = customer.CreatedAt, Email = customer.Email, Id = customer.Id, LastName = customer.LastName, Name = customer.Name, UpdatedAt = customer.UpdatedAt };
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

        public async Task<TaskResult<List<UsersDto>>> GetAllAsync()
        {
           
            List<UsersDto> costumersDtos = new List<UsersDto>();
            var result = new TaskResult<List<UsersDto>>();
            try
            {
                var customers = await _context.Users.Where(e => e.Active).ToListAsync();
                customers.ForEach((customer) =>
                {
                    costumersDtos.Add(new UsersDto { Active = customer.Active, CreatedAt = customer.CreatedAt, Email = customer.Email, Id = customer.Id, LastName = customer.LastName, Name = customer.Name, UpdatedAt = customer.UpdatedAt, PhoneNumber = customer.PhoneNumber, DocumentNumber = customer.DocumentNumber });
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

        public async Task<TaskResult<UsersDto>> GetAsync(string Id)
        {
            var result = new TaskResult<UsersDto>();

            try
            {
                var costumer = await _context.Users.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new UsersDto { Active = costumer.Active, CreatedAt = costumer.CreatedAt, Email = costumer.Email, Id = costumer.Id, LastName = costumer.LastName, Name = costumer.Name, UpdatedAt = costumer.UpdatedAt, DocumentNumber = costumer.DocumentNumber, PhoneNumber = costumer.PhoneNumber };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;
             
        }

        public  TaskResult<UsersDto> Get(string Id)
        {
            var result = new TaskResult<UsersDto>();

            try
            {
                var costumer =  _context.Users.FirstOrDefault(e => e.Id == Id && e.Active);
                result.Data = new UsersDto { Active = costumer.Active, CreatedAt = costumer.CreatedAt, Email = costumer.Email, Id = costumer.Id, LastName = costumer.LastName, Name = costumer.Name, UpdatedAt = costumer.UpdatedAt, DocumentNumber = costumer.DocumentNumber, PhoneNumber = costumer.PhoneNumber };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<UsersDto>> SaveAsync(UsersDto costumerDto)
        {
            var costumer = new ApplicationUser { Active = costumerDto.Active, CreatedAt = costumerDto.CreatedAt, DocumentNumber = costumerDto.DocumentNumber, Email = costumerDto.Email, Id = costumerDto.Id, LastName = costumerDto.LastName, Name = costumerDto.Name, PhoneNumber = costumerDto.PhoneNumber, UpdatedAt = costumerDto.UpdatedAt };
            var result = new TaskResult<UsersDto>();
            try
            {
                _context.Users.Add(costumer);
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

        public async Task<TaskResult<UsersDto>> UpdateAsync(UsersDto customerDto)
        {
            var customer = new ApplicationUser {
                Active = customerDto.Active,
                CreatedAt = customerDto.CreatedAt,
                DocumentNumber = customerDto.DocumentNumber,
                Email = customerDto.Email,
                Id = customerDto.Id,
                LastName = customerDto.LastName,
                Name = customerDto.Name,
                PhoneNumber = customerDto.PhoneNumber,
                UpdatedAt = customerDto.UpdatedAt,
                UserName = customerDto.Email
            };
           
            
            var result = new TaskResult<UsersDto>();
            try
            {
                _context.Users.Add(customer);
                _context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = customerDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información del cliente: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<UsersDto>> GetByDocumentNumberAsync(string documentNumber)
        {
            var customers = await this.GetAllAsync();
            var customer = customers.Data.Where(e => e.DocumentNumber.Equals(documentNumber)).FirstOrDefault();
            var result = new TaskResult<UsersDto>
            {
                Data = customer,
                Message = customers.Message,
                Success = customers.Success
            };

            return result;
        }
    }
}