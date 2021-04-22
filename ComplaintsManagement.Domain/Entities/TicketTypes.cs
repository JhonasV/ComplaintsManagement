using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Domain.Entities
{
    public class TicketTypes : BaseEntity
    {
        public string Description { get; set; }
    }
}