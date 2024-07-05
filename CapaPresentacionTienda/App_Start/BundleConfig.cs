using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionTienda
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
                      // "~/Scripts/DataTables/jquery.dataTables.js",
                      // "~/Scripts/DataTables/dataTables.responsive.js",
                       "~/Scripts/sweetalert.min.js",
                       "~/Scripts/jquery.validate.js",
                       "~/Scripts/jquery-ui-1.12.1.js",
                       "~/Scripts/scripts.js"


                        ));


         

            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-3.7.1.js"));

            // bootstrap
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            //// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/fontawesome/all.min.js",
                      "~/Scripts/loadingoverlay.min.js",
                      "~/Scripts/sweetalert.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/sweetalert.css"));
        }
    }
}