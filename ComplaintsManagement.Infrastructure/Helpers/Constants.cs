using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Helpers
{
    public static class Constants
    {
        public enum StatusConstants
        {
            SOMETIDO = 1,
            TRANSFERIDO = 2

        }
        
        public static class Roles
        {
            public static string ADMIN { get; set; } = "ADMIN";
            public static string GUEST { get; set; } = "GUEST";
        }

        public static class ComplaintClaimType
        {
            public static string COMPLAINT { get; set; } = "RECLAMACION";
            public static string CLAIM { get; set; } = "QUEJA";
        }
    }
}