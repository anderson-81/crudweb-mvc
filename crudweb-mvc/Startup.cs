using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(crudweb_mvc.Startup))]
namespace crudweb_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
