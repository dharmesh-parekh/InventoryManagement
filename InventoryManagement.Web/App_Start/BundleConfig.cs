using System.Web;
using System.Web.Optimization;

namespace InventoryManagement.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jQuery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                          "~/Scripts/jquery.validate.unobtrusive.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"

                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/bootstrap-dialog.min.css",
                      "~/Content/jquery.jgrowl.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                     "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/bootstrap-dialog.min.js",
                      "~/Scripts/jquery.jgrowl.min.js",
                      "~/Scripts/bootbox.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                  "~/Scripts/Custom/Common.js",
                   "~/Scripts/Custom/User.js",
                   "~/Scripts/Custom/Car.js"
              ));
        }
    }
}
