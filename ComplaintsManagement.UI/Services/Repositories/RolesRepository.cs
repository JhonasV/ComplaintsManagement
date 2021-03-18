using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.UI.Services.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _context;


        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;

        }
 
        public async Task<TaskResult<RolesDto>> AddUserRoleAsync(string userId, string roleName)
        {
           
            var result = new TaskResult<RolesDto>();
            try
            {
                // var identityResult = await _userManager.AddToRoleAsync(userId, roleName);
                //if (identityResult.Succeeded)
                //{
                //    result.Message = "Se asignó el role correctamente.";
                //}
                //else
                //{
                //    result.Success = false;
                //    result.Message = String.Join(", ", identityResult.Errors); 
                //}
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<RolesDto>> DeleteAsync(string Id)
        {
            var status = await _context.Roles.FirstOrDefaultAsync(e => e.Id == Id);
            var result = new TaskResult<RolesDto>();
            try
            {
                if (status != null)
                {

                    await _context.SaveChangesAsync();
                    //result.Message = "Status borrado exitosamente!";
                    //result.Data = new StatusDto
                    //{
                    //    Active = status.Active,
                    //    CreatedAt = status.CreatedAt,
                    //    Id = status.Id,
                    //    Name = status.Name,
                    //    UpdatedAt = status.UpdatedAt
                    //};

                }
                else
                {
                    result.Message = "No se pudo encontrar el rol con el Id enviado";
                }
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Ha ocurrido un error: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<List<RolesDto>>> GetAllAsync()
        {

            List<RolesDto> rolesDto = new List<RolesDto>();
            var result = new TaskResult<List<RolesDto>>();
            try
            {
                var roles = await _context.Roles.ToListAsync();
                roles.ForEach((role) =>
                {
                    rolesDto.Add(new RolesDto
                    {
                        Id = role.Id,
                        Name = role.Name,
                    });
                });
                result.Data = rolesDto;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al recuperar el listado de roles: {e.Message}";
            }

            return result;
        }

        public async Task<TaskResult<RolesDto>> GetAsync(string Id)
        {
            var result = new TaskResult<RolesDto>();

            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == Id);
                result.Data = new RolesDto
                {
                    Id = role.Id,
                    Name = role.Name
                };
            }
            catch (Exception e)
            {

                result.Success = false;
                result.Message = $"Error al recuperar la información del role: {e.Message}";
            }
            return result;

        }

        public async Task<TaskResult<RolesDto>> SaveAsync(RolesDto roleDto)
        {
            var guid = Guid.NewGuid();
            var role = new IdentityRole
            {
                Id = guid.ToString(),
                Name = roleDto.Name
            };

            var result = new TaskResult<RolesDto>();
            try
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                result.Message = $"Se agregó el role {role.Name}";

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar al Role: {e.Message}";
            }
            return result;
        }

        public async Task<TaskResult<RolesDto>> UpdateAsync(RolesDto roleDto)
        {
            var roles = new IdentityRole
            {
                Id = roleDto.Id,
                Name = roleDto.Name
            };

            var result = new TaskResult<RolesDto>();
            try
            {
                _context.Roles.Add(roles);
                _context.Entry(roles).State = System.Data.Entity.EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Data = roleDto;
                result.Message = "El registro fue actualizado correctamente";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar información del role: {e.Message}";
            }
            return result;
        }
    }
}