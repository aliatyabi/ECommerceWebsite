using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class Product_SizesController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Product_Sizes
        public ActionResult Index()
        {
            var product_Sizes = db.Product_Sizes.Include(p => p.SizeGroups);
            return View(product_Sizes.ToList());
        }

        // GET: Admin/Product_Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Sizes product_Sizes = db.Product_Sizes.Find(id);
            if (product_Sizes == null)
            {
                return HttpNotFound();
            }
            return View(product_Sizes);
        }

        // GET: Admin/Product_Sizes/Create
        public ActionResult Create()
        {
            var sizeGroups = db.SizeGroups.ToArray().Select(s => new
            {
                SizeGroup = s.SizeGroup,
                //BrandAndCategory = string.Format("{0}/{1}", s.Brand, s.Category)
                Category = s.Category
            });

            ViewBag.SizeGroup = new SelectList(sizeGroups, "SizeGroup", "Category");
            return View();
        }

        // POST: Admin/Product_Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeId,SizeUK,SizeIR,SizeGroup")] Product_Sizes product_Sizes)
        {
            if (ModelState.IsValid)
            {
                db.Product_Sizes.Add(product_Sizes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SizeGroup = new SelectList(db.SizeGroups, "SizeGroup", "Category", product_Sizes.SizeGroup);
            return View(product_Sizes);
        }

        // GET: Admin/Product_Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Sizes product_Sizes = db.Product_Sizes.Find(id);
            if (product_Sizes == null)
            {
                return HttpNotFound();
            }
            ViewBag.SizeGroup = new SelectList(db.SizeGroups, "SizeGroup", "Category", product_Sizes.SizeGroup);
            return View(product_Sizes);
        }

        // POST: Admin/Product_Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeId,SizeUK,SizeIR,SizeGroup")] Product_Sizes product_Sizes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Sizes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SizeGroup = new SelectList(db.SizeGroups, "SizeGroup", "Category", product_Sizes.SizeGroup);
            return View(product_Sizes);
        }

        // GET: Admin/Product_Sizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Sizes product_Sizes = db.Product_Sizes.Find(id);
            if (product_Sizes == null)
            {
                return HttpNotFound();
            }
            return View(product_Sizes);
        }

        // POST: Admin/Product_Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Sizes product_Sizes = db.Product_Sizes.Find(id);
            db.Product_Sizes.Remove(product_Sizes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
