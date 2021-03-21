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
        private readonly ICustomersRepository _customersRepository;

        public ComplaintsRepository(ApplicationDbContext context, ICustomersRepository customersRepository)
        {
            _context = context;
            _customersRepository = customersRepository;
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
                    result.Data = new ComplaintsDto { Active = complaints.Active, CreatedAt = complaints.CreatedAt,  Id = complaints.Id,  StatusId = complaints.StatusId, ProductsId = complaints.ProductsId, ComplaintsOptionsId = complaints.ComplaintsOptionsId };
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
                var complaints = await _context
                    .Complaints
                    .Include(e => e.ComplaintsOption)
                    .Include(e => e.Status)
                    .Include(e => e.Product)
                    .Include(e => e.Deparment)
                    .Where(e => e.Active)
                    .ToListAsync();

                complaints.ForEach((complaint) =>
                {
                    var statusDto = new StatusDto
                    {
                        Active = complaint.Status.Active,
                        CreatedAt = complaint.Status.CreatedAt,
                        Id = complaint.Status.Id,
                        Name = complaint.Status.Name,
                        UpdatedAt = complaint.Status.UpdatedAt
                    };


    

                    var complaintsOptionsDto = new ComplaintsOptionsDto
                    {
                        Active = complaint.ComplaintsOption.Active,
                        CreatedAt = complaint.ComplaintsOption.CreatedAt,
                        Id = complaint.ComplaintsOption.Id,
                        ProductsId = complaint.ComplaintsOption.ProductsId,
                        Name = complaint.ComplaintsOption.Name,
                        UpdatedAt = complaint.ComplaintsOption.UpdatedAt
                    };

                    var productsDto = new ProductsDto
                    {
                        Active = complaint.Product.Active,
                        CreatedAt = complaint.Product.CreatedAt,
                        Id = complaint.Product.Id,
                        Name = complaint.Product.Name,
                        UpdatedAt = complaint.Product.UpdatedAt,
                        Description = complaint.Product.Description,
                        Price = complaint.Product.Price
                    };

                    var departmensDto = new DepartmentsDto
                    {
                        Id = complaint.Deparment.Id,
                        Name = complaint.Deparment.Name,
                        Description = complaint.Deparment.Description,
                        Active = complaint.Deparment.Active,
                        Deleted = complaint.Deparment.Deleted,
                        CreatedAt = complaint.Deparment.CreatedAt,
                        UpdatedAt = complaint.Deparment.UpdatedAt,
                        DeletedAt = complaint.Deparment.DeletedAt
                    };

                    var customer = _customersRepository.Get(complaint.UsersId);
                    complaintsDtos.Add(new ComplaintsDto {
                        Active = complaint.Active,
                        CreatedAt = complaint.CreatedAt,

                        Id = complaint.Id,
                        ComplaintsOptionsId = complaint.ComplaintsOptionsId,
                        StatusId = complaint.StatusId,
                        ProductsId = complaint.ProductsId,
                        Status = statusDto,
                        ComplaintsOption = complaintsOptionsDto,
                        Product = productsDto,
                        Customer = customer.Data,
                        Department = departmensDto
                    });
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
                var complaints = 
                    await _context
                    .Complaints
                    .Include(e => e.ComplaintsOption)
                    .Include(e => e.Status)
                    .Include(e => e.Product)
                    .Include(e => e.Deparment)
                    .FirstOrDefaultAsync(e => e.Id == Id && e.Active);




                var complaintsOptionsDto = new ComplaintsOptionsDto
                {
                    Active = complaints.ComplaintsOption.Active,
                    CreatedAt = complaints.ComplaintsOption.CreatedAt,
                    Id = complaints.ComplaintsOption.Id,
                    ProductsId = complaints.ComplaintsOption.ProductsId,
                    Name = complaints.ComplaintsOption.Name,
                    UpdatedAt = complaints.UpdatedAt
                };

                var statusDto = new StatusDto
                {
                    Active = complaints.Status.Active,
                    CreatedAt = complaints.Status.CreatedAt,
                    Id = complaints.Status.Id,
                    Name = complaints.Status.Name,
                    UpdatedAt = complaints.Status.UpdatedAt
                };

                var productsDto = new ProductsDto
                {
                    Active = complaints.Product.Active,
                    CreatedAt = complaints.Product.CreatedAt,
                    Id = complaints.Product.Id,
                    Name = complaints.Product.Name,
                    UpdatedAt = complaints.Product.UpdatedAt,
                    Description = complaints.Product.Description,
                    Price = complaints.Product.Price
                };


                var departmensDto = new DepartmentsDto
                {
                    Id = complaints.Deparment.Id,
                    Name = complaints.Deparment.Name,
                    Description = complaints.Deparment.Description,
                    Active = complaints.Deparment.Active,
                    Deleted = complaints.Deparment.Deleted,
                    CreatedAt = complaints.Deparment.CreatedAt,
                    UpdatedAt = complaints.Deparment.UpdatedAt,
                    DeletedAt = complaints.Deparment.DeletedAt
                };

                var customer = await _customersRepository.GetAsync(complaints.UsersId);
                result.Data = new ComplaintsDto { 
                        Active = complaints.Active,
                        CreatedAt = complaints.CreatedAt,
                        Id = complaints.Id,
                        ComplaintsOptionsId = complaints.ComplaintsOptionsId,
                        StatusId = complaints.StatusId,
                        ProductsId = complaints.ProductsId,
                        ComplaintsOption = complaintsOptionsDto,
                        Comment = complaints.Comment,
                        Status = statusDto,
                        Product = productsDto,
                        Customer = customer.Data,
                        Department = departmensDto

                };
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
            var complaints = new Complaints {
                Active = complaintsDto.Active,
                Comment = complaintsDto.Comment,
                Id = complaintsDto.Id,           
                ComplaintsOptionsId = complaintsDto.ComplaintsOptionsId,
                StatusId = complaintsDto.StatusId,
                ProductsId = complaintsDto.ProductsId,
                UsersId = complaintsDto.UsersId,
                DepartmentsId = complaintsDto.DepartmentsId
            };
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
            var complaints = new Complaints
            {
                Active = complaintsDto.Active,
                Comment = complaintsDto.Comment,
                Id = complaintsDto.Id,
                ComplaintsOptionsId = complaintsDto.ComplaintsOptionsId,
                StatusId = complaintsDto.StatusId,
                ProductsId = complaintsDto.ProductsId,
                UsersId = complaintsDto.UsersId
            };
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