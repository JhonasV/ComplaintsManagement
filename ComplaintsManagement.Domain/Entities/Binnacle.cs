using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Binnacle : BaseEntity
    {

        public string ApplicationUserId { get; set; }

        public int StatusId { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }


        public int? ComplaintsId { get; set; }


        public  Status Status { get; set; }
        public  Complaints Complaints { get; set; }
    }
}
