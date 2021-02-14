using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class CustomersProducts : BaseEntity
    {
        public int CostumersId { get; set; }
        public int ProductsId { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Products Product { get; set; }
    }
}
