using ComplaintsManagement.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.UI.Models
{
    public class CustomersViewModel
    {
        public CustomersViewModel()
        {
            this.Customers = new List<UsersDto>();
        }
        public List<UsersDto> Customers { get; set; }
        public UsersDto Customer { get; set; }
        public string Message { get; set; }
    }
}