using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsArch.NET.Startup))]
namespace AsArch.NET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
