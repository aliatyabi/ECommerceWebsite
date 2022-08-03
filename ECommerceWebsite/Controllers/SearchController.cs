using ECommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ECommerceWebsite.Controllers
{
    public class SearchController : BaseController
    {
        //[HttpPost]
        public ActionResult Index(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Product", new { search = search });
        }

        public ActionResult Menu()
        {
            var categories = db.Product_Categories.ToList();

            ViewBag.Brands = db.Brands().ToList();

            return PartialView("_PartialMenu", categories);
        }
    }
}