using System.Web;
using System.Web.Optimization;

namespace SastoMithoMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/open-iconic-bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/magnific-popup.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/jquery.timepicker.css",
                      "~/Content/icomoon.css",
                      "~/Content/style(cart).css",
                      "~/Content/style(extended).css",
                      "~/Content/style(login).css",
                      "~/Content/style(signup).css",
                      "~/Content/style.css"));
        }
    }
}
