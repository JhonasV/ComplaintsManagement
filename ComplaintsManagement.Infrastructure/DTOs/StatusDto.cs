using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class StatusDto
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
