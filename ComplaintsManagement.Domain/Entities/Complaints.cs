using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Complaints : BaseEntity
    {
        public int CustomersId { get; set; }
        public int AgentId { get; set; }
        public int ClaimsOptionsId { get; set; }
        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public int ProductsId { get; set; }
        public DateTime CompletedAt { get; set; }
        public string Commment { get; set; }


        public virtual Products Product { get; set; }
        public virtual ClaimsOptions ClaimsOption { get; set; }
        public virtual ComplaintsOptions ComplaintsOption { get; set; }
        public virtual Status Status { get; set; }


    }
}
