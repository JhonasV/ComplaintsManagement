using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class ServiceRateDto
    {
        public int Id { get; set; }
        [Required]
        public int ComplaintsId { get; set; }
        [Required]
        public EnumRate Rate { get; set; }
        public ComplaintsDto Complaint { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }


    public enum EnumRate
    {
        SELECCIONAR,
        MALO,
        REGULAR,
        BUENO
    };
}