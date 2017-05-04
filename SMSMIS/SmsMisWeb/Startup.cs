using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ValencyWeb.Startup))]
namespace ValencyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
