using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class ComplaintsDto
    {
        public int Id { get; set; }
        //public int CustomersId { get; set; }
        [Required]
        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public string Type { get; set; }
        public int DepartmentsId { get; set; }
        [Required]
        public int ProductsId { get; set; }
        public string UsersId { get; set; }
        public DateTime? CompletedAt { get; set; } 
        [Required]
        public string Comment { get; set; }
        public ProductsDto Product { get; set; }
        public ComplaintsOptionsDto ComplaintsOption { get; set; }
        public StatusDto Status { get; set; }
        public DepartmentsDto Department { get; set; }
        public UsersDto Customer { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
