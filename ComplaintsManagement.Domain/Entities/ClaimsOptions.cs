using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class ClaimsOptions : BaseEntity
    {
        public string Name { get; set; }
        public int DepartmentsId { get; set; }
    }
}
