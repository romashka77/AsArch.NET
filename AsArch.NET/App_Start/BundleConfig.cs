﻿using System.Web.Optimization;
using System.Web.Optimization.React;

namespace AsArch.NET
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

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
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                      "~/Scripts/react/react.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/react-dom").Include(
                      "~/Scripts/react/react-dom.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/remarkable").Include(
                      "~/Scripts/remarkable/remarkable.js"));

            //bundles.Add(new Bundle("~/bundles/app", new IBundleTransform[]
            //{
            //    new BabelTransform(),
            //    new JsMinify(),
            //})
            //.Include(
            //    "~/Script/jsx/CommentBox.jsx",
            //    "~/Scripts/jsx/app.jsx"
            //));


            bundles.Add(new BabelBundle("~/bundles/appComment").Include(
                "~/Scripts/jsx/Component/Comment.jsx",
                "~/Scripts/jsx/Component/CommentList.jsx",
                "~/Scripts/jsx/Component/CommentForm.jsx",
               "~/Scripts/jsx/Component/CommentBox.jsx",
               "~/Scripts/jsx/appComment.jsx"
            ));

            bundles.Add(new BabelBundle("~/bundles/appDopPredIsk").Include(
                "~/Scripts/jsx/Component/DopPredIskSelect.jsx",
                "~/Scripts/jsx/Component/DopPredIsk.jsx",
                "~/Scripts/jsx/Component/DopPredIskList.jsx",
                "~/Scripts/jsx/Component/DopPredIskForm.jsx",
               "~/Scripts/jsx/Component/DopPredIskTable.jsx",
               "~/Scripts/jsx/appDopPredIsk.jsx"
            ));

            
            // Forces files to be combined and minified in debug mode
            // Only used here to demonstrate how combination/minification works
            // Normally you would use unminified versions in debug mode.
            BundleTable.EnableOptimizations = true;

        }
    }
}
