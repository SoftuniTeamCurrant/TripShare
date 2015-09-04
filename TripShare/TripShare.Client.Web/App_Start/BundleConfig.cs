namespace TripShare.Client.Web.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;

    public static class BundleConfig
    {
        public static void RegisterScriptBundles(BundleCollection bundles)
        {
            const string AngularAppRoot = "~/Scripts/app/";
            const string VirtualBundlePath = AngularAppRoot + "main.js";
            const string LibrariesPath = "~/Scripts/vendor/";
            
            var scriptBundle = new ScriptBundle(VirtualBundlePath)
                .Include(LibrariesPath + "bootstrap.js")
                .Include(LibrariesPath + "jquery-1.9.0.js")
                .Include(LibrariesPath + "modernizr-2.6.2.js")
                .Include(LibrariesPath + "angular.js")
                .Include(AngularAppRoot + "app.js")
                .IncludeDirectory(AngularAppRoot, "*.js", searchSubdirectories: true);

            var styleBundle = new StyleBundle("~/Content/style.css")
                .Include("~/Content/Site.css")
                .Include("~/Content/bootstrap.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
        }
    }
}