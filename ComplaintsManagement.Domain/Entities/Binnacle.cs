using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComplaintsManagement.Domain.Entities
{
    public class Binnacle : BaseEntity
    {
        [ForeignKey(nameof(Users))]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(Users))]
        public int AgentId { get; set; }
        public int ComplaintsId { get; set; }
        public int StatusId { get; set; }
        [StringLength(200)]
        public string Comment { get; set; }
    }
}
