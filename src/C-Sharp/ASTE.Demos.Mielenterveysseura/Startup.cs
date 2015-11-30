using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASTE.Demos.Mielenterveysseura.Startup))]
namespace ASTE.Demos.Mielenterveysseura
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
