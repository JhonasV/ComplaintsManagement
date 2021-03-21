using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class Complaints : BaseEntity
    {

        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public int ProductsId { get; set; }
        public int DepartmentsId { get; set; }
        public string UsersId { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Comment { get; set; }


        public virtual Products Product { get; set; }
        public virtual ComplaintsOptions ComplaintsOption { get; set; }
        public virtual Status Status { get; set; }
        public virtual Departments Deparment { get; set; }


    }
}
