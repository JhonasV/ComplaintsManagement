using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Users : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
