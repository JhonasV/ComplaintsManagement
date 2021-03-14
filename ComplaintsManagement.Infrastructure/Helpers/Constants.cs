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
            SOMETIDO = 1
        }
        
        public static class Roles
        {
            public static string ADMIN { get; set; } = "ADMIN";
            public static string CUSTOMER { get; set; } = "CUSTOMER";
        }
    }
}