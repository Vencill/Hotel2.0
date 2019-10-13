using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hotel2._0.Startup))]
namespace Hotel2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
