using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineTaskList.Web.Startup))]
namespace OnlineTaskList.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
