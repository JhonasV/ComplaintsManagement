using ComplaintsManagement.Infrastructure.DTOs;
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
            this.Costumers = new List<CustomersDto>();
        }
        public List<CustomersDto> Costumers { get; set; }
        public CustomersDto Costumer { get; set; }
        public string Message { get; set; }
    }
}