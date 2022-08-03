using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Utilities.Helpers
{
    public static class WebPImageDrawer
    {
        public static MvcHtmlString DrawWebPImage(this HtmlHelper helper, string className, string id, string imageUrl, string alt)
        {
            if (CanBrowserHandleWebPImages())
            {
                // Get the file type
                string fileType = Path.GetExtension(imageUrl);
                string fileName = Path.GetFileNameWithoutExtension(imageUrl) + ".webp";
                if (fileType != null)
                {
                    if (File.Exists(fileName))
                    {
                        imageUrl = imageUrl.Replace(fileType, ".webp");
                    }
                }

                return new MvcHtmlString(String.Format("<img class=\"{0}\" " + "id=\"{1}\" " + "src=\"{2}\" " + "alt=\"{3}\" /> ", className, id, imageUrl, alt));
            }


            return new MvcHtmlString(String.Format("<img class=\"{0}\" " + "id=\"{1}\" " + "src=\"{2}\" " + "alt=\"{3}\" /> ", className, id, imageUrl, alt));
        }

        /// <summary>
        /// Detect if the current browser is capable
        /// of handling WebP images
        /// </summary>
        /// <returns>A boolean based on the result.</returns>
        private static bool CanBrowserHandleWebPImages()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpBrowserCapabilities browser = httpRequest.Browser;

            if (browser.Type.Contains("Chrome") || browser.Type.Contains("Opera") || browser.Type.Contains("Android"))
            {
                return true;
            }

            return false;
        }
    }
}
