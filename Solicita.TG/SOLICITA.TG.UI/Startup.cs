using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Solicita.TG.UI.Startup))]
namespace Solicita.TG.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
