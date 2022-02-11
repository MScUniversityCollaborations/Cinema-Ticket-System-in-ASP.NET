using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieTickets.Startup))]
namespace MovieTickets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
