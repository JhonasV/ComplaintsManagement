﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class Binnacle : BaseEntity
    {

        public int CustomerId { get; set; }
        public int AgentId { get; set; }
        public int ComplaintsId { get; set; }
        public int StatusId { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }


        public virtual Costumers Customer { get; set; }
        public virtual Costumers Agent { get; set; }
        public virtual Complaints Complaint { get; set; }
    }
}
