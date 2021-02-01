using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Complaints : BaseEntity
    {
        [ForeignKey(nameof(Users))]
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public int StatusId { get; set; }
        public DateTime CompletedAt { get; set; }
        public string Description { get; set; }

        public virtual Users Customer { get; set; }
        public virtual Category Category { get; set; }
        public virtual Status Status { get; set; }


    }
}
