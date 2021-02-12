using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.DTOs
{
    public class CustomersDto 
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int DocumentNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
