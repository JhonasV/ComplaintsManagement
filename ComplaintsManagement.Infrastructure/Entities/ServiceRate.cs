using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class ServiceRate : BaseEntity
    {
        [Required]
        public int ComplaintsId { get; set; }
        [Required]
        public int Rate  { get; set; }


        public Complaints Complaint { get; set; }
    }
}