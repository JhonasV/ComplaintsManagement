using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
