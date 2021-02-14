using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class ComplaintsAgents : BaseEntity
    {
        [ForeignKey(nameof(Customers))]
        public int AgentId { get; set; }
        public int ComplaintsId { get; set; }

        public virtual Customers Agent { get; set; }
        public virtual Complaints Complaint { get; set; }
    }
}
