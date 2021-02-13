﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ComplaintsManagement.Infrastructure.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
