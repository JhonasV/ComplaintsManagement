using ComplaintsManagement.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.UI.Models
{
    public class ComplaintsViewModel
    {
        public TaskResult<ComplaintsDto> Complaint { get; set; }
        public BinnacleDto Binnacle { get; set; }
        public ServiceRateDto ServiceRate { get; set; }
    }
}