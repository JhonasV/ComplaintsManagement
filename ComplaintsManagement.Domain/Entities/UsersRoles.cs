using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class UsersRoles : BaseEntity
    {
        public int UsersId { get; set; }
        public int RolesId { get; set; }


        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }
    }
}
