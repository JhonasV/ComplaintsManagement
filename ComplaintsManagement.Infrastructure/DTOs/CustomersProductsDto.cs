using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class CustomersProductsDto
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public int ProductsId { get; set; }
        public bool Active { get; set; }


        public CustomersDto Costumer { get; set; }
        public ProductsDto Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
