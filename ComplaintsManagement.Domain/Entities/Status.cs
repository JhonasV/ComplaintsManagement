using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
