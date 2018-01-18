using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRApp.Startup))]
namespace CRApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
