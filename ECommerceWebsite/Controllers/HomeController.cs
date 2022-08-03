using ECommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Product;
using Microsoft.AspNet.Identity;

namespace ECommerceWebsite.Controllers
{
    public class HomeController : BaseController
    {
         //GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            return PartialView("_PartialSlider", db.Sliders.ToList());
        }

        public ActionResult NewArrivals()
        {
            List<Products> bestSellers = new List<Products>();

            foreach (var item in db.BestSellers().Take(15).ToList())
            {
                bestSellers.Add(db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault());
            }

            ViewBag.BestSeller = bestSellers;

            List<Products> rating = new List<Products>();

            foreach (var item in db.Rating().Take(15).ToList())
            {
                rating.Add(db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault());
            }

            ViewBag.Rating = rating;

            return PartialView(db.Products.OrderByDescending(p => p.CreatedDate).Take(15).ToList());
        }

        public ActionResult Menu()
        {
            var categories = db.Product_Categories.ToList();

            ViewBag.Brands = db.Brands();

            return PartialView("_PartialMenu", categories);
        }
    }
}