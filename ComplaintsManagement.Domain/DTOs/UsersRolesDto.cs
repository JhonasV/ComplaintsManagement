using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.DTOs
{
    public class UsersRolesDto
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int RolesId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
