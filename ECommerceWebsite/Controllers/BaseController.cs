using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Controllers
{
    /// <summary>
    /// Defines the base controller.
    /// </summary>
    /// <remarks>
    /// This is the base class for all site's controllers.
    /// </remarks>
    public class BaseController : Controller
    {
        /**initilizing culture on controller initialization*/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["CurrentCulture"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CurrentCulture"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CurrentCulture"].ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fa-IR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-IR");

                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }

        /** changing culture*/
        public ActionResult ChangeCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Session["CurrentCulture"] = culture;
            return Redirect(Request.UrlReferrer.AbsoluteUri);

            //return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The data context.
        /// </summary>
        protected ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        /// <summary>
        /// Dispose the used resource.
        /// </summary>
        /// <param name="disposing">The disposing flag.</param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Called when an unhandled exception occurs in the action.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is UnauthorizedAccessException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("Home", "Index");
            }
            //
            base.OnException(filterContext);
        }

    }
}