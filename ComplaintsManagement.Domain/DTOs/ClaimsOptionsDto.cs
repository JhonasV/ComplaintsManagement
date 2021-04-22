using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.DTOs
{
    public class ClaimsOptionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentsId { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
