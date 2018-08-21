using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Implantagraf.Startup))]
namespace Implantagraf
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
