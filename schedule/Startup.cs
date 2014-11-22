using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(schedule.Startup))]
namespace schedule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
