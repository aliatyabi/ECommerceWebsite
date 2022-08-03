using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace ECommerceWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;   //enable CDN support

            //add link to jquery on the CDN
            var jqueryCdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.min.js";

            bundles.Add(new ScriptBundle("~/bundles/jquery",
                        jqueryCdnPath).Include(
                        "~/assets/js/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/assets/js/owl.carousel.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/assets/js/jquery.themepunch.revolution.min.js",
                      "~/assets/js/jquery.themepunch.plugins.min.js",
                      "~/assets/js/engo-plugins.min.js",
                      "~/assets/js/price-range.min.js",
                      "~/assets/js/store.min.js",
                      "~/Scripts/My Scripts/ShowCart.js",
                      "~/Scripts/My Scripts/RemoveCartItem.js",
                      "~/Scripts/My Scripts/UpdateSmallCart.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/assets/js/ion.rangeSlider.min.js",
                      "~/Scripts/My Scripts/SelectBrand.js",
                      "~/Scripts/My Scripts/SelectCategory.js",
                      "~/Scripts/My Scripts/FilterProducts.js",
                      "~/Scripts/My Scripts/SelectBrandFunc.js",
                      "~/Scripts/My Scripts/AddToWishList.js"));

            bundles.Add(new ScriptBundle("~/bundles/productdetails").Include(
                      "~/assets/js/slick.min.js",
                      "~/assets/js/jquery.zoom.js",
                      "~/Scripts/My Scripts/ShowProductDetails.js"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/assets/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/stylefa").Include(
                      "~/Content/bootstrap.rtl.full.min.css",
                      "~/assets/css/styleFa.css",
                      "~/Content/SiteFa.css"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/assets/vendor/owl-slider.css",
                      "~/assets/vendor/slick.css",
                      "~/assets/vendor/settings.css",
                      "~/assets/vendor/range-price.css",
                      "~/assets/images/favicon.png",
                      "~/Content/InStockSlider.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/index").Include(
                      "~/Content/assets/css/plugins.min.css",
                      "~/assets/css/ion.rangeSlider.css",
                      "~/assets/css/ion.rangeSlider.skinHTML5.css",
                      "~/assets/css/normalize.css"));

            bundles.Add(new StyleBundle("~/Content/indexRTL").Include(
                      "~/Content/assets/css/plugins.min.css",
                      "~/assets/css/ion.rangeSlider.css",
                      "~/assets/css/ion.rangeSlider.skinHTML5.css",
                      "~/assets/css/normalize.css",
                      "~/assets/css/indexRTL.css"));
        }
    }
}
