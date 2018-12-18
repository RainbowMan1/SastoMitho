using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SastoMithoMVC.Startup))]
namespace SastoMithoMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
