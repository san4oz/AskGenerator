using System.Web;
using System.Web.Optimization;

namespace AskGenerator.Mvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/Scripts/admin/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/pages").Include(
                        "~/Scripts/pages/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqmeter").Include(
                        "~/Scripts/jqmeter/jqmeter.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular/angular.min.js",
                        "~/Scripts/angular/angular-route.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/controllers").Include(
                        "~/Scripts/controllers/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/models").Include(
                        "~/Scripts/models/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/twitter-bootstrap-hover-dropdown.js"
                    ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Css/select2.css",
                "~/Css/Site.css"                
                ));

            bundles.Add(new ScriptBundle("~/bundles/controls").Include(
                "~/Scripts/gtm.js",
                "~/Scripts/select2.js",
                "~/Scripts/controls.js"
                ));

            bundles.Add(new StyleBundle("~/Content/Theme/base/css").Include(
                        "~/Css/Theme/base/jquery.ui.core.css",
                        "~/Css/Theme/base/jquery.ui.resizable.css",
                        "~/Css/Theme/base/jquery.ui.selectable.css",
                        "~/Css/Theme/base/jquery.ui.accordion.css",
                        "~/Css/Theme/base/jquery.ui.autocomplete.css",
                        "~/Css/Theme/base/jquery.ui.button.css",
                        "~/Css/Theme/base/jquery.ui.dialog.css",
                        "~/Css/Theme/base/jquery.ui.slider.css",
                        "~/Css/Theme/base/jquery.ui.tabs.css",
                        "~/Css/Theme/base/jquery.ui.datepicker.css",
                        "~/Css/Theme/base/jquery.ui.progressbar.css",
                        "~/Css/Theme/base/jquery.ui.theme.css"));



            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
  "~/Css/Theme/vendors/uniform/jquery.uniform.js"
  , "~/Css/Theme/vendors/chosen.jquery.js"
  , "~/Css/Theme/vendors/bootstrap-datepicker/js/bootstrap-datepicker.js"
  , "~/Css/Theme/vendors/bootstrap-wysihtml5-rails-b3/vendor/assets/javascripts/bootstrap-wysihtml5/wysihtml5.js"
  , "~/Css/Theme/vendors/bootstrap-wysihtml5-rails-b3/vendor/assets/javascripts/bootstrap-wysihtml5/core-b3.js"
  , "~/Css/Theme/vendors/twitter-bootstrap-wizard/jquery.bootstrap.wizard-for.bootstrap3.js"
  , "~/Css/Theme/vendors/boostrap3-typeahead/bootstrap3-typeahead.js"
  , "~/Css/Theme/vendors/easypiechart/jquery.easy-pie-chart.js"
  , "~/Css/Theme/vendors/ckeditor/ckeditor.js"
  , "~/Css/Theme/vendors/tinymce/js/tinymce/tinymce.js"
  , "~/Css/Theme/vendors/bootstrap-wysihtml5-rails-b3/vendor/assets/javascripts/bootstrap-wysihtml5/wysihtml5.js"
  , "~/Css/Theme/vendors/bootstrap-wysihtml5-rails-b3/vendor/assets/javascripts/bootstrap-wysihtml5/core-b3.js"
  , "~/Css/Theme/vendors/jGrowl/jquery.jgrowl.js"
  , "~/Css/Theme/vendors/bootstrap-datepicker/js/bootstrap-datepicker.js"
  , "~/Css/Theme/vendors/sparkline/jquery.sparkline.js"
  , "~/Css/Theme/vendors/tablesorter/js/jquery.tablesorter.js"
  , "~/Css/Theme/vendors/flot/jquery.flot.js"
  , "~/Css/Theme/vendors/flot/jquery.flot.selection.js"
  , "~/Css/Theme/vendors/flot/jquery.flot.resize.js"
  , "~/Css/Theme/vendors/fullcalendar/fullcalendar.js"
                 ));



            bundles.Add(new StyleBundle("~/Content/Theme").Include(
                      "~/Css/bootstrap.css",
                      "~/Css/bootstrap-theme.css",
                      "~/Css/Theme/css/bootstrap-admin-theme.css",
                       "~/Css/Theme/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/Vendors").Include(
                "~/Css/Theme/vendors/bootstrap-datepicker/css/datepicker.css"
   , "~/Css/Theme/css/datepicker.fixes.css"
   , "~/Css/Theme/vendors/uniform/themes/default/css/uniform.default.min.css"
   , "~/Css/Theme/css/uniform.default.fixes.css"
   , "~/Css/Theme/vendors/chosen.min.css"
   , "~/Css/Theme/vendors/bootstrap-wysihtml5-rails-b3/vendor/assets/stylesheets/bootstrap-wysihtml5/core-b3.css"
   , "~/Css/Theme/vendors/easypiechart/jquery.easy-pie-chart.css"
   , "~/Css/Theme/vendors/easypiechart/jquery.easy-pie-chart_custom.css"
   , "~/Css/Theme/vendors/bootstrap-wysihtml5-rails-b3/vendor/assets/stylesheets/bootstrap-wysihtml5/core-b3.css"
   , "~/Css/Theme/vendors/jGrowl/jquery.jgrowl.css"
   , "~/Css/Theme/vendors/bootstrap-datepicker/css/datepicker.css"
    , "~/Css/Theme/vendors/fullcalendar/fullcalendar.css"));


        }
    }
}