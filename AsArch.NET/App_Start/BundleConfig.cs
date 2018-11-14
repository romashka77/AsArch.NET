using System.Web.Optimization;

namespace AsArch.NET
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/node_modules/react-bootstrap-table/dist/react-bootstrap-table.min.css ",
                "~/node_modules/scanner-js/dist/scanner.css",
                "~/node_modules/filepond/dist/filepond.min.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/remarkable").Include(
                      "~/Scripts/remarkable/remarkable.js"));

            bundles.Add(new ScriptBundle("~/bundles/iskovoezajvlenie").Include(
                "~/node_modules/scanner-js/dist/scanner.js",
                "~/Scripts/iskovoezajvlenie.bundle.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/storona-processa").Include(
                "~/Scripts/storona-processa.js"
            ));

        }
    }
}
