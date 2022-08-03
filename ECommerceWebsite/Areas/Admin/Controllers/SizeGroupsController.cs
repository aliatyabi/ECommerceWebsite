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
    public class SizeGroupsController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/SizeGroups
        public ActionResult Index()
        {
            return View(db.SizeGroups.ToList());
        }

        // GET: Admin/SizeGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGroups sizeGroups = db.SizeGroups.Find(id);
            if (sizeGroups == null)
            {
                return HttpNotFound();
            }
            return View(sizeGroups);
        }

        // GET: Admin/SizeGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SizeGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeGroup,Brand,Category")] SizeGroups sizeGroups)
        {
            if (ModelState.IsValid)
            {
                db.SizeGroups.Add(sizeGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sizeGroups);
        }

        // GET: Admin/SizeGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGroups sizeGroups = db.SizeGroups.Find(id);
            if (sizeGroups == null)
            {
                return HttpNotFound();
            }
            return View(sizeGroups);
        }

        // POST: Admin/SizeGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeGroup,Brand,Category")] SizeGroups sizeGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sizeGroups);
        }

        // GET: Admin/SizeGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGroups sizeGroups = db.SizeGroups.Find(id);
            if (sizeGroups == null)
            {
                return HttpNotFound();
            }
            return View(sizeGroups);
        }

        // POST: Admin/SizeGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SizeGroups sizeGroups = db.SizeGroups.Find(id);
            db.SizeGroups.Remove(sizeGroups);
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
