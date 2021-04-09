using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class TicketTypes : BaseEntity
    {
        public string Description { get; set; }
    }
}