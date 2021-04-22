using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplaintsManagement.Domain.DTOs
{
    public class ComplaintsOptionsDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int? ProductsId { get; set; }
        [Required]
        public int? DepartmentsId { get; set; }

        public virtual ProductsDto Product { get; set; }
        public virtual DepartmentsDto Departments { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
