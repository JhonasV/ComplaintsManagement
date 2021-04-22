using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Domain.DTOs
{
    public class TicketTypesDto
    {
        public string Description { get; set; }
        public int Id { get; set; }

        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}