using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
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
        public int TicketTypesId { get; set; }


        public Products Product { get; set; }
        public ComplaintsOptions ComplaintsOption { get; set; }
        public Status Status { get; set; }
        public Departments Deparment { get; set; }
        public TicketTypes TicketType { get; set; }
        public List<Binnacle> Binnacles { get; set; }

    }
}
