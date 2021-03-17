﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.DTOs
{
    public class CustomersProductsDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductsId { get; set; }



        //public CustomersDto Customer { get; set; }
        public ProductsDto Product { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
