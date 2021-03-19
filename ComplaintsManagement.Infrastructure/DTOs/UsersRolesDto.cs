using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class UsersRolesDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<RolesDto> Roles { get; set; }
        public List<UsersDto> Users { get; set; }
        public string UsersId { get; set; }
        public string RolesId { get; set; }
        public string RoleName { get; set; }
    }
}