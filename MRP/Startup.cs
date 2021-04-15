using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MRP.Startup))]
namespace MRP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
