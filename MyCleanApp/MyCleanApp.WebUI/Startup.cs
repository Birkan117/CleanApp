using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCleanApp.WebUI.Startup))]
namespace MyCleanApp.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
