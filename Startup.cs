using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hattmakarens_system.Startup))]
namespace Hattmakarens_system
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
