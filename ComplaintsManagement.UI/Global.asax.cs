using AutoMapper;
using ComplaintsManagement.UI.App_Start;
using ComplaintsManagement.UI.Configurations.AutoMapperConfig;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace ComplaintsManagement.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfiles>());
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
