using React;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AsArch.NET.ReactConfig), "Configure")]

namespace AsArch.NET
{
	public static class ReactConfig
	{
		public static void Configure()
		{
            ReactSiteConfiguration.Configuration
                //.SetReuseJavaScriptEngines(true)
                .AddScript("~/Scripts/remarkable/remarkable.min.js")
                //.AddScript("~/Scripts/jsx/Comment.jsx")
                //.AddScript("~/Scripts/jsx/CommentList.jsx")
                //.AddScript("~/Scripts/jsx/CommentForm.jsx")
                .AddScript("~/Scripts/jsx/CommentBox.jsx")
                .AddScript("~/Scripts/jsx/app.jsx");

            //JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
            //JsEngineSwitcher.Current.EngineFactories.AddV8();
            // If you want to use server-side rendering of React components, 
            // add all the necessary JavaScript files here. This includes 
            // your components as well as all of their dependencies.
            // See http://reactjs.net/ for more information. Example:
            //ReactSiteConfiguration.Configuration
            //	.AddScript("~/Scripts/First.jsx")
            //	.AddScript("~/Scripts/Second.jsx");

            // If you use an external build too (for example, Babel, Webpack,
            // Browserify or Gulp), you can improve performance by disabling 
            // ReactJS.NET's version of Babel and loading the pre-transpiled 
            // scripts. Example:
            //ReactSiteConfiguration.Configuration
            //	.SetLoadBabel(false)
            //	.AddScriptWithoutTransform("~/Scripts/bundle.server.js")

            //ReactSiteConfiguration.Configuration
            //    .AddScript("~/Scripts/remarkable/remarkable.min.js")
            //    .AddScript("~/Scripts/jsx/app.jsx");
        }
    }
}