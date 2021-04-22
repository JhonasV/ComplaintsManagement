using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class CustomersProducts : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public int ProductsId { get; set; }

        //public virtual ComplaintsManagement. ApplicationUser Customers { get; set; }
        public virtual Products Product { get; set; }
    }
}
