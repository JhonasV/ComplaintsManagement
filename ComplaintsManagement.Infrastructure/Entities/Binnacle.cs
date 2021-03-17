using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class Binnacle : BaseEntity
    {

        public string ApplicationId { get; set; }
        public int AgentId { get; set; }
        public int ClaimsOrComplaintsId { get; set; }
        public int StatusId { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }

        public virtual Complaints Complaint { get; set; }
    }
}
