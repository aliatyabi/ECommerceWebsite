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
    public class WarehousesController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Warehouses
        public ActionResult Index()
        {
            return View(db.Warehouses.ToList());
        }

        public ActionResult ShowWarehouses()
        {
            return PartialView("_PartialShowWarehouses",db.Warehouses.ToList());
        }

        // GET: Admin/Warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouses warehouses = db.Warehouses.Find(id);
            if (warehouses == null)
            {
                return HttpNotFound();
            }
            return View(warehouses);
        }

        // GET: Admin/Warehouses/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WarehouseId,WarehouseName,Status")] Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                warehouses.CreatedDate = DateTime.Now;
                warehouses.Stock = 0;
                db.Warehouses.Add(warehouses);
                db.SaveChanges();
                return PartialView("_PartialShowWarehouses", db.Warehouses.ToList());
            }

            return PartialView("_PartialShowWarehouses",db.Warehouses.ToList());
        }

        // GET: Admin/Warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouses warehouses = db.Warehouses.Find(id);
            if (warehouses == null)
            {
                return HttpNotFound();
            }
            return PartialView(warehouses);
        }

        // POST: Admin/Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WarehouseId,WarehouseName,Status,CreatedDate")] Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouses).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_PartialShowWarehouses", db.Warehouses.ToList());
            }
            return PartialView("_PartialShowWarehouses", db.Warehouses.ToList());
        }

        //// GET: Admin/Warehouses/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Warehouses warehouses = db.Warehouses.Find(id);
        //    if (warehouses == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(warehouses);
        //}

        //// POST: Admin/Warehouses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Warehouses warehouses = db.Warehouses.Find(id);
        //    db.Warehouses.Remove(warehouses);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
