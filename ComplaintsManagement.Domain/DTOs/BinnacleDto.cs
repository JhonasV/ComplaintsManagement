using System;
using System.ComponentModel.DataAnnotations;

namespace ComplaintsManagement.Domain.DTOs
{
    public class BinnacleDto
    {

        public string ApplicationUserId { get; set; }
        public int StatusId { get; set; }
        [StringLength(200)]
        [Required]
        public string Comment { get; set; }
        public int? ComplaintsId { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public StatusDto Status { get; set; }
        public UsersDto User { get; set; }
    }
}
