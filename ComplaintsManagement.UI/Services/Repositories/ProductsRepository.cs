using ComplaintsManagement.Infrastructure.Database;
using ComplaintsManagement.Domain.DTOs;
using ComplaintsManagement.Domain.Entities;
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
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskResult<ProductsDto>> DeleteAsync(int Id)
        {
            var products = await _context.Products.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<ProductsDto>();
            try
            {
                if (products != null)
                {
                    products.Deleted = true;
                    await _context.SaveChangesAsync();
                    result.Message = "Producto borrado exitosamente!";
                    result.Data = AutoMapper.Mapper.Map<ProductsDto>(products);
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

        public async Task<TaskResult<List<ProductsDto>>> GetAllAsync()
        {
           
            List<ProductsDto> productsDto = new List<ProductsDto>();
            var result = new TaskResult<List<ProductsDto>>();
            try
            {
                var products = await _context.Products.Where(e => e.Deleted == false).ToListAsync();
                products.ForEach((product) =>
                {
                productsDto.Add(new ProductsDto { Active = product.Active, CreatedAt = product.CreatedAt, Id = product.Id, Name = product.Name, UpdatedAt = product.UpdatedAt, Description = product.Description, Price = product.Price });
                });
                result.Data = productsDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de clientes: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<ProductsDto>> GetAsync(int Id)
        {
            var result = new TaskResult<ProductsDto>();

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new ProductsDto { Active = product.Active, CreatedAt = product.CreatedAt, Id = product.Id, Name = product.Name, UpdatedAt = product.UpdatedAt, Description = product.Description, Price = product.Price };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del producto: {e.Message}";
            }
            return result;
             
        }

        public async Task<TaskResult<ProductsDto>> SaveAsync(ProductsDto productsDto)
        {
            var products = new Products { Active = productsDto.Active, CreatedAt = productsDto.CreatedAt, Id = productsDto.Id, Name = productsDto.Name, UpdatedAt = productsDto.UpdatedAt, Description = productsDto.Description, Price = productsDto.Price };
            var result = new TaskResult<ProductsDto>();
            try
            {
                _context.Products.Add(products);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó el producto {products.Name}";
                
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar al Producto: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<ProductsDto>> UpdateAsync(ProductsDto productsDto)
        {
            var products = new Products { Active = productsDto.Active, CreatedAt = productsDto.CreatedAt, Id = productsDto.Id, Name = productsDto.Name, UpdatedAt = productsDto.UpdatedAt, Description = productsDto.Description, Price = productsDto.Price };
            var result = new TaskResult<ProductsDto>();
            try
            {
                _context.Products.Add(products);
                _context.Entry(products).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = productsDto;
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