using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.DTOs
{
    public class ComplaintsDto
    {
        public int Id { get; set; }
        public int CustomersId { get; set; }
        public int UsersId { get; set; }
        public int ClaimsOptionsId { get; set; }
        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public int ProductsId { get; set; }
        public DateTime CompletedAt { get; set; }
        public string Commment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
