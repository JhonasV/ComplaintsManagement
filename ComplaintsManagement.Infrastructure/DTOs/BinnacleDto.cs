using System;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class BinnacleDto
    {
        public int Id { get; set; }
        public int CustomersId { get; set; }
        public int UsersId { get; set; }
        public int ClaimsOptionsId { get; set; }
        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public string Commment { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
