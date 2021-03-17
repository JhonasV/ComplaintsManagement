using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace ComplaintsManagement.UI.Services.Repositories
{
    public class CustomerProductsRepository : ICustomersProductsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomersRepository _customersRepository;

        public CustomerProductsRepository(ApplicationDbContext context, ICustomersRepository customersRepository)
        {
            _context = context;
            _customersRepository = customersRepository;
        }
        public async Task<TaskResult<CustomersProductsDto>> DeleteAsync(int Id)
        {
            var customer = await _context.CustomersProducts.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<CustomersProductsDto>();
            try
            {
                if (customer != null)
                {
                    customer.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Cliente borrado exitosamente!";
                    result.Data = new CustomersProductsDto { Active = customer.Active, CreatedAt = customer.CreatedAt,  Id = customer.Id };
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

        public async Task<TaskResult<List<CustomersProductsDto>>> GetAllAsync()
        {

            List<CustomersProductsDto> costumersDtos = new List<CustomersProductsDto>();
            var result = new TaskResult<List<CustomersProductsDto>>();
            try
            {
                var customers = await _context.CustomersProducts
                    .Include(e => e.Product)
                    .Where(e => e.Active).ToListAsync();
                customers.ForEach((customer) =>
                {
                    var customerInfo = _customersRepository.Get(customer.ApplicationUserId);
                    var customerDto = new UsersDto { Active = customerInfo.Data.Active, CreatedAt = customerInfo.Data.CreatedAt, Email = customerInfo.Data.Email, Id = customerInfo.Data.Id, LastName = customerInfo.Data.LastName, Name = customerInfo.Data.Name, UpdatedAt = customerInfo.Data.UpdatedAt, DocumentNumber = customerInfo.Data.DocumentNumber, PhoneNumber = customerInfo.Data.PhoneNumber };
                    var productsDto = new ProductsDto { Active = customer.Product.Active, CreatedAt = customer.Product.CreatedAt, Id = customer.Product.Id, Name = customer.Product.Name,  Description = customer.Product.Description, Price = customer.Product.Price };
                    costumersDtos.Add(new CustomersProductsDto { Active = customer.Active, CreatedAt = customer.CreatedAt, Id = customer.Id, ProductsId = customer.ProductsId, ApplicationUserId = customer.ApplicationUserId,  Product = productsDto });
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

        public async Task<TaskResult<List<CustomersProductsDto>>> GetAllByCustomerIdAsync(string customerId)
        {
            var customersProducts = await this.GetAllAsync();
            customersProducts.Data = customersProducts.Data.Where(e => e.ApplicationUserId == customerId).ToList();
            return customersProducts;
        }

        public async Task<TaskResult<CustomersProductsDto>> GetAsync(int Id)
        {
            var result = new TaskResult<CustomersProductsDto>();

            try
            {
                var costumer = await _context.CustomersProducts.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new CustomersProductsDto { Active = costumer.Active, CreatedAt = costumer.CreatedAt,  Id = costumer.Id};
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<CustomersProductsDto>> SaveAsync(CustomersProductsDto customersProductsDto)
        {
            var costumer = new CustomersProducts { Active = customersProductsDto.Active, CreatedAt = customersProductsDto.CreatedAt, ApplicationUserId = customersProductsDto.ApplicationUserId, ProductsId = customersProductsDto.ProductsId, Id = customersProductsDto.Id, UpdatedAt = customersProductsDto.UpdatedAt };
            var result = new TaskResult<CustomersProductsDto>();
            try
            {
                _context.CustomersProducts.Add(costumer);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó el producto al cliente de forma exitosa!";

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar el producto: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<CustomersProductsDto>> UpdateAsync(CustomersProductsDto customersProductsDto)
        {
            var costumer = new CustomersProducts { Active = customersProductsDto.Active, CreatedAt = customersProductsDto.CreatedAt, Id = customersProductsDto.Id, UpdatedAt = customersProductsDto.UpdatedAt };
            var result = new TaskResult<CustomersProductsDto>();
            try
            {
                _context.CustomersProducts.Add(costumer);
                _context.Entry(costumer).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = customersProductsDto;
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