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
            this.Costumers = new List<CostumersDto>();
        }
        public List<CostumersDto> Costumers { get; set; }
        public CostumersDto Costumer { get; set; }
        public string Message { get; set; }
    }
}