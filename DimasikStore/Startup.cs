using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DimasikStore.Startup))]
namespace DimasikStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
