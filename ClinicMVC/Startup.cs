using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicMVC.Startup))]
namespace ClinicMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
