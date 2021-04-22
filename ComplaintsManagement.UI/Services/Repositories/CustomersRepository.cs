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
           
            List<UsersDto> customersDto = new List<UsersDto>();
            var result = new TaskResult<List<UsersDto>>();
            try
            {
                var customers = await _context
                    .Users
                    .Include(e => e.Department)
                    .Where(e => e.Deleted == false).ToListAsync();

                customersDto = AutoMapper.Mapper.Map<List<UsersDto>>(customers);

                //customers.ForEach((customer) =>
                //{
                //    var departmentDto = new DepartmentsDto();
                //    if(customer.Department != null)
                //    {
                //        //departmentDto = new DepartmentsDto
                //        //{
                //        //    Id = customer.Department.Id,
                //        //    Active = customer.Department.Active,
                //        //    CreatedAt = customer.Department.CreatedAt,
                //        //    Description = customer.Department.Description,
                //        //    Name = customer.Department.Name,
                //        //    DeletedAt = customer.Department.DeletedAt,
                //        //    Deleted = customer.Department.Deleted,
                //        //    UpdatedAt = customer.Department.UpdatedAt
                //        //};

                //        departmentDto = AutoMapper.Mapper.Map<DepartmentsDto>(customer.Department);

                //    }

                //    costumersDtos.Add(
                //        AutoMapper.Mapper.Map<UsersDto>(customer)
                //        );
                //});
                result.Data = customersDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de clientes: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<List<UsersDto>>> GetAllAsync(ApplicationUserManager userManager)
        {

            List<UsersDto> costumersDtos = new List<UsersDto>();
            var result = new TaskResult<List<UsersDto>>();
            try
            {
                var customers = await _context
                    .Users
                    .Include(e => e.Department)
                    .Where(e => e.Deleted == false).ToListAsync();
                customers.ForEach( (customer) =>
                {
                    var departmentDto = new DepartmentsDto();
                    if (customer.Department != null)
                    {
                        departmentDto = new DepartmentsDto
                        {
                            Id = customer.Department.Id,
                            Active = customer.Department.Active,
                            CreatedAt = customer.Department.CreatedAt,
                            Description = customer.Department.Description,
                            Name = customer.Department.Name,
                            DeletedAt = customer.Department.DeletedAt,
                            Deleted = customer.Department.Deleted,
                            UpdatedAt = customer.Department.UpdatedAt
                        };

                    }

                    costumersDtos.Add(
                        new UsersDto
                        {
                            Active = customer.Active,
                            CreatedAt = customer.CreatedAt,
                            Email = customer.Email,
                            Id = customer.Id,
                            LastName = customer.LastName,
                            Name = customer.Name,
                            UpdatedAt = customer.UpdatedAt,
                            PhoneNumber = customer.PhoneNumber,
                            DocumentNumber = customer.DocumentNumber,
                            Department = departmentDto                          
                        });
                });


                foreach (var customer in costumersDtos)
                {
                    var roleNames = await userManager.GetRolesAsync(customer.Id);
                    var roleName = roleNames.Count > 0 ? roleNames[0] : "N/A";
                    customer.RoleName = roleName;
                }

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
                var customer = await _context.Users.SingleOrDefaultAsync(e => e.Id == Id && e.Deleted == false);
                result.Data = new UsersDto 
                { 
                    PasswordHash = customer.PasswordHash,
                    Active = customer.Active,
                    CreatedAt = customer.CreatedAt,
                    Email = customer.Email, 
                    Id = customer.Id,
                    LastName = customer.LastName, 
                    Name = customer.Name, 
                    UpdatedAt = customer.UpdatedAt, 
                    DocumentNumber = customer.DocumentNumber, 
                    PhoneNumber = customer.PhoneNumber,
                    DepartmentId = customer.DepartmentId
                };
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
                var customer =  _context.Users.SingleOrDefault(e => e.Id == Id && e.Deleted == false);
                result.Data = AutoMapper.Mapper.Map<UsersDto>(customer);
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del cliente: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<UsersDto>> SaveAsync(UsersDto customerDto, ApplicationUserManager userManager)
        {
            var customer = new ApplicationUser 
            { 
                Active = customerDto.Active,
                CreatedAt = customerDto.CreatedAt,
                DocumentNumber = customerDto.DocumentNumber,
                Email = customerDto.Email,
                LastName = customerDto.LastName,
                Name = customerDto.Name,
                PhoneNumber = customerDto.PhoneNumber,
                UpdatedAt = customerDto.UpdatedAt,
                DepartmentId = customerDto.DepartmentId,
                UserName = customerDto.Email
            };
            var result = new TaskResult<UsersDto>();
            try
            {
                var identityResult = await userManager.CreateAsync(customer, $"@{customer.DocumentNumber}Prueba.");

                if (identityResult.Succeeded)
                {
                    result.Message = "Cliente creado exitosamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = String.Join(", ", identityResult.Errors);
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al crear el cliente: {e.InnerException.Message}";
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
                UserName = customerDto.Email,
                DepartmentId = customerDto.DepartmentId
            };
           
            
            var result = new TaskResult<UsersDto>();
            try
            {
                var oldValues = await this.GetAsync(customer.Id);
                customer.PasswordHash = oldValues.Data.PasswordHash;
                _context.Users.AddOrUpdate(customer);
                //_context.Users.Add(customer);
                //_context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
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