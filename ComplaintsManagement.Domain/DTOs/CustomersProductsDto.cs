﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Domain.DTOs
{
    public class CustomersProductsDto
    {
        public int Id { get; set; }
        public int CustomersId { get; set; }
        public int ProductsId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
