using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class ComplaintsOptions : BaseEntity
    {
        public string Name { get; set; }
        public int ProductsId { get; set; }

        public virtual Products Product { get; set; }
    }
}
