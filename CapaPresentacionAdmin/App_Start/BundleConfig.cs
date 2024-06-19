using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionAdmin
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // aca van los ficheros .js
            bundles.Add(new ScriptBundle("~/bundles/complementosJs").Include(
                       "~/Scripts/fontawesome/all.js",
                       "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                       "~/Scripts/DataTables/jquery.dataTables.js",
                       "~/Scripts/DataTables/dataTables.responsive.js",
                       "~/Scripts/sweetalert.min.js",
                       "~/Scripts/jquery.validate.js",
                       "~/Scripts/scripts.js"

                       
                        ));


            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-3.7.1.js"));
           


            // bootstrap
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            // aca van todos los ficheros .css

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/responsive.dataTables.css",
                      "~/Content/sweetalert.css"));

            
        }
    }
}
