using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Domain.Entities
{
    public class Departments : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}