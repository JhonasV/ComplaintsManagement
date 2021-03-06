﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class ComplaintsOptions : BaseEntity
    {
        public string Name { get; set; }
        public int? ProductsId { get; set; }
        public int? DepartmentsId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Departments Department { get; set; }
    }
}
