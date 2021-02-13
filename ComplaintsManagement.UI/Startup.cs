using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComplaintsManagement.UI.Startup))]
namespace ComplaintsManagement.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
