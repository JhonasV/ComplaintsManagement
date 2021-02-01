using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class ComplaintsAgent : BaseEntity
    {
        [ForeignKey(nameof(Users))]
        public int AgentId { get; set; }
        public int ComplaintsId { get; set; }

        public virtual Users Agent { get; set; }
        public virtual Complaints Complaint { get; set; }
    }
}
