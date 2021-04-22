using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Domain.Entities
{
    public class Claims
    {
        public int Id { get; set; }
        public int ClaimsOptionsId { get; set; }
        public int StatusId { get; set; }
        public int ProductsId { get; set; }
        public int DepartmentsId { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Comment { get; set; }


        public virtual Products Product { get; set; }
        public virtual ClaimsOptions ClaimsOptions { get; set; }
        public virtual Status Status { get; set; }
        public virtual Departments Deparment { get; set; }
    }
}