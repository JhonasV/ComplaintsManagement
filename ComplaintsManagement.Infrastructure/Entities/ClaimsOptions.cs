using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class ClaimsOptions : BaseEntity
    {
        public string Name { get; set; }
        public int ComplaintsOptionsId { get; set; }
    }
}
