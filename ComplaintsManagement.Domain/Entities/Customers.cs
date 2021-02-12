using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Customers : BaseEntity
    {
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
