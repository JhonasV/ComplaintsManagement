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
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskResult<DepartmentsDto>> DeleteAsync(int Id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
            var result = new TaskResult<DepartmentsDto>();
            try
            {
                if (department != null)
                {
                    department.Active = false;
                    await _context.SaveChangesAsync();
                    result.Message = "Departamento borrado exitosamente!";
                    result.Data = new DepartmentsDto
                    {
                        Active = department.Active,
                        CreatedAt = department.CreatedAt,
                        Id = department.Id,
                        Name = department.Name,
                        UpdatedAt = department.UpdatedAt
                    };
                }
                else
                {
                    result.Message = "No se pudo encontrar el departamento con el Id enviado";
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<DepartmentsDto>>> GetAllAsync()
        {
            List<DepartmentsDto> deparmentsDto = new List<DepartmentsDto>();
            var result = new TaskResult<List<DepartmentsDto>>();
            try
            {
                var departments = await _context.Departments.Where(e => e.Active).ToListAsync();
                departments.ForEach((department) =>
                {
                    deparmentsDto.Add(new DepartmentsDto
                    {
                        Active = department.Active,
                        CreatedAt = department.CreatedAt,
                        Id = department.Id,
                        Name = department.Name,
                        UpdatedAt = department.UpdatedAt,
                        Description = department.Description
                    });
                });
                result.Data = deparmentsDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de departamentos: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<DepartmentsDto>> GetAsync(int Id)
        {
            var result = new TaskResult<DepartmentsDto>();

            try
            {
                var departments = await _context.Departments.FirstOrDefaultAsync(e => e.Id == Id && e.Active);
                result.Data = new DepartmentsDto
                {
                    Active = departments.Active,
                    CreatedAt = departments.CreatedAt,
                    Id = departments.Id,
                    Name = departments.Name,
                    UpdatedAt = departments.UpdatedAt,
                    Description = departments.Description
                };
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar la información del status: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<DepartmentsDto>> SaveAsync(DepartmentsDto DepartmentsDto)
        {
            var department = new Departments
            {
                Active = DepartmentsDto.Active,
                CreatedAt = DepartmentsDto.CreatedAt,
                Id = DepartmentsDto.Id,
                Name = DepartmentsDto.Name,
                UpdatedAt = DepartmentsDto.UpdatedAt,
                Description = DepartmentsDto.Description
            };
            var result = new TaskResult<DepartmentsDto>();
            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó el departamento {department.Name}";

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar al departamento: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<DepartmentsDto>> UpdateAsync(DepartmentsDto departmentDto)
        {
            var department = new Departments
            {
                Active = departmentDto.Active,
                CreatedAt = departmentDto.CreatedAt,
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                UpdatedAt = departmentDto.UpdatedAt,
                Description = departmentDto.Description
            };

            var result = new TaskResult<DepartmentsDto>();
            try
            {
                _context.Departments.Add(department);
                _context.Entry(department).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = departmentDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información del departamentos: {e.Message}";
            }
            return result;
        }
    }
}