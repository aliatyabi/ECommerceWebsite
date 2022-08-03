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
    public class TTController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/TT
        public ActionResult Index()
        {
            var product_Categories = db.Product_Categories.Include(p => p.Product_Categories2);
            return View(product_Categories.ToList());
        }

        // GET: Admin/TT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Categories product_Categories = db.Product_Categories.Find(id);
            if (product_Categories == null)
            {
                return HttpNotFound();
            }
            return View(product_Categories);
        }

        // GET: Admin/TT/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Product_Categories, "CategoryId", "CategoryTitle");
            return View();
        }

        // POST: Admin/TT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryTitle,ParentId")] Product_Categories product_Categories)
        {
            if (ModelState.IsValid)
            {
                db.Product_Categories.Add(product_Categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Product_Categories, "CategoryId", "CategoryTitle", product_Categories.ParentId);
            return View(product_Categories);
        }

        // GET: Admin/TT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Categories product_Categories = db.Product_Categories.Find(id);
            if (product_Categories == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Product_Categories, "CategoryId", "CategoryTitle", product_Categories.ParentId);
            return View(product_Categories);
        }

        // POST: Admin/TT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryTitle,ParentId")] Product_Categories product_Categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Product_Categories, "CategoryId", "CategoryTitle", product_Categories.ParentId);
            return View(product_Categories);
        }

        // GET: Admin/TT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Categories product_Categories = db.Product_Categories.Find(id);
            if (product_Categories == null)
            {
                return HttpNotFound();
            }
            return View(product_Categories);
        }

        // POST: Admin/TT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Categories product_Categories = db.Product_Categories.Find(id);
            db.Product_Categories.Remove(product_Categories);
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
