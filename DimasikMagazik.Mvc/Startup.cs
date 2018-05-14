using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(DimasikMagazik.Startup))]
namespace DimasikMagazik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
