using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Complaints : BaseEntity
    {
        [ForeignKey(nameof(Users))]
        public int CustomersId { get; set; }
        public int UsersId { get; set; }
        public int ClaimsOptionsId { get; set; }
        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public DateTime CompletedAt { get; set; }
        public string Commment { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ClaimsOptions ClaimsOptions { get; set; }
        public virtual ComplaintsOptions ComplaintsOptions { get; set; }
        public virtual Status Status { get; set; }


    }
}
