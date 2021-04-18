using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.UI.Models
{
    public class ReportPorcentageModel
    {
        public string Status { get; set; }
        public decimal StatusPorcentage{ get; set; }
    }

    public class ServiceRatePortenageModel
    {
        public string Rate { get; set; }
        public decimal RatePorcentage { get; set; }
    }
}