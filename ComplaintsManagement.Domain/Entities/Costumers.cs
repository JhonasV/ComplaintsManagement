using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class Costumers : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public string PhoneNumber { get; set; }

    }
}
