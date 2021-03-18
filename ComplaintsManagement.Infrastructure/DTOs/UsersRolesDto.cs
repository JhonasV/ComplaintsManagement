using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class UsersRolesDto
    {
        public string UsersId { get; set; }
        public string RolesId { get; set; }
        public string RoleName { get; set; }
    }
}