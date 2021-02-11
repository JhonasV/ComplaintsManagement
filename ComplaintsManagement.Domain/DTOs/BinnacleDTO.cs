﻿using System;

namespace ComplaintsManagement.Domain.DTOs
{
    public class BinnacleDto
    {
        public int CustomersId { get; set; }
        public int UsersId { get; set; }
        public int ClaimsOptionsId { get; set; }
        public int ComplaintsOptionsId { get; set; }
        public int StatusId { get; set; }
        public DateTime CompletedAt { get; set; }
        public string Commment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
