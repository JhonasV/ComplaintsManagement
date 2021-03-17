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
    public class ComplaintsOptionsRepository : IComplaintsOptionsRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintsOptionsRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<TaskResult<ComplaintsOptionsDto>> DeleteAsync(int Id)
        {
            var products = await _context.ComplaintsOptions.FirstOrDefaultAsync(e => e.Id == Id && e.Deleted == false);
            var result = new TaskResult<ComplaintsOptionsDto>();
            try
            {
                if (products != null)
                {
                    products.Deleted = true;
                    await _context.SaveChangesAsync();
                    result.Message = "Opción borrada exitosamente!";
                    result.Data = new ComplaintsOptionsDto {
                                    Active = products.Active,
                                    CreatedAt = products.CreatedAt,
                                    Id = products.Id,
                                    ProductsId = products.ProductsId,
                                    Name = products.Name,
                                    UpdatedAt = products.UpdatedAt
                                };
                }
                else
                {
                    result.Message = "No se pudo encontrar producto con el Id enviado";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<ComplaintsOptionsDto>>> GetAllAsync()
        {

            List<ComplaintsOptionsDto> complaintsOptions = new List<ComplaintsOptionsDto>();
            var result = new TaskResult<List<ComplaintsOptionsDto>>();
            try
            {
                var complaintsOps = await _context
                    .ComplaintsOptions
                    .Include(e => e.Product)
                    .Where(e => e.Deleted == false).ToListAsync();
                complaintsOps.ForEach((option) =>
                {
                    var productDto = new ProductsDto {
                        Active = option.Product.Active,
                        CreatedAt = option.Product.CreatedAt,
                        Id = option.Product.Id,
                        Name = option.Product.Name,
                        UpdatedAt = option.Product.UpdatedAt,
                        Description = option.Product.Description,
                        Price = option.Product.Price
                    };
                    complaintsOptions.Add(new ComplaintsOptionsDto
                    {
                        Active = option.Active,
                        CreatedAt = option.CreatedAt,
                        Id = option.Id,
                        ProductsId = option.ProductsId,
                        Name = option.Name,
                        UpdatedAt = option.UpdatedAt,
                        Product = productDto
                    });
                });
                result.Data = complaintsOptions;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de clientes: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<ComplaintsOptionsDto>> GetAsync(int Id)
        {
            var result = new TaskResult<ComplaintsOptionsDto>();

            try
            {
                var option = await _context.ComplaintsOptions.FirstOrDefaultAsync(e => e.Id == Id && e.Deleted == false);
                result.Data =  new ComplaintsOptionsDto
                {
                    Active = option.Active,
                    CreatedAt = option.CreatedAt,
                    Id = option.Id,
                    ProductsId = option.ProductsId,
                    Name = option.Name,
                    UpdatedAt = option.UpdatedAt,
                    Deleted = option.Deleted
                };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del producto: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<ComplaintsOptionsDto>> SaveAsync(ComplaintsOptionsDto optionDto)
        {
            var option = new ComplaintsOptions
            {
                Active = optionDto.Active,
                CreatedAt = optionDto.CreatedAt,
                Id = optionDto.Id,
                ProductsId = optionDto.ProductsId,
                Name = optionDto.Name,
                UpdatedAt = optionDto.UpdatedAt
            };
            var result = new TaskResult<ComplaintsOptionsDto>();
            try
            {
                _context.ComplaintsOptions.Add(option);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó la opción {optionDto.Name}";

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar al Producto: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<ComplaintsOptionsDto>> UpdateAsync(ComplaintsOptionsDto optionDto)
        {
            var option = new ComplaintsOptions
            {
                Active = optionDto.Active,
                CreatedAt = optionDto.CreatedAt,
                Id = optionDto.Id,
                ProductsId = optionDto.ProductsId,
                Name = optionDto.Name,
                UpdatedAt = optionDto.UpdatedAt
            };
            var result = new TaskResult<ComplaintsOptionsDto>();
            try
            {
                _context.ComplaintsOptions.Add(option);
                _context.Entry(option).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = optionDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información del producto: {e.Message}";
            }
            return result;
        }
    }
}