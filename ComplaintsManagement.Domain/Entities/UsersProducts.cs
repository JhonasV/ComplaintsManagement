using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class UsersProducts : BaseEntity
    {
        public int UsersId { get; set; }
        public int ProductsId { get; set; }

        public virtual Users User { get; set; }
        public virtual Products Product { get; set; }
    }
}
