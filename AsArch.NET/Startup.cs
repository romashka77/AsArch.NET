using Microsoft.Owin;
using Owin;
using React;

[assembly: OwinStartupAttribute(typeof(AsArch.NET.Startup))]
namespace AsArch.NET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //React.AssemblyRegistration.Container.Register<IJavaScriptEngineFactory, JavaScriptEngineFactory>().AsSingleton();
            ConfigureAuth(app);
        }
    }
}
