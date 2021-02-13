using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class CostumersProducts : BaseEntity
    {
        public int CostumersId { get; set; }
        public int ProductsId { get; set; }

        public virtual Costumers Costumers { get; set; }
        public virtual Products Product { get; set; }
    }
}
