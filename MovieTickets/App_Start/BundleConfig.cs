using System.Web.Optimization;

namespace MovieTickets
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Scripts/jquery").Include(
                                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                                "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/Scripts/bootstrap").Include(
                                "~/Scripts/umd/popper.js",
                                "~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/Styles/bootstrap").Include(
                                "~/Content/bootstrap.css",
                                "~/Content/bootstrap-social.css"));

            bundles.Add(new Bundle("~/Styles/site").Include(
                                "~/Content/Styles.css",
                                "~/Content/back-to-top-button.css"));

            bundles.Add(new Bundle("~/Styles/fontawesome").Include(
                                "~/Content/font-awesome.css",
                                "~/Content/font-awesome/css/all.css"));

            bundles.Add(new Bundle("~/Scripts/custom_js").Include(
                                "~/Scripts/navbar_dropdown_hover.js",
                                "~/Scripts/various_scripts.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
