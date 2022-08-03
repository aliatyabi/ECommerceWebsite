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
    public class FeaturesController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Features
        public ActionResult Index()
        {
            return View(db.Features.ToList());
        }

        public ActionResult ShowFeaturesList()
        {
            return PartialView("_PartialFeaturesTable", db.Features.ToList());
        }

        // GET: Admin/Features/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Features features = db.Features.Find(id);
            if (features == null)
            {
                return HttpNotFound();
            }
            return View(features);
        }

        // GET: Admin/Features/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/Features/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeatureId,FeatureTitle")] Features features)
        {
            if (ModelState.IsValid)
            {
                db.Features.Add(features);
                db.SaveChanges();
                return PartialView("_PartialFeaturesTable", db.Features.ToList());
            }

            return PartialView("_PartialFeaturesTable", db.Features.ToList());
        }

        // GET: Admin/Features/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Features features = db.Features.Find(id);
            if (features == null)
            {
                return HttpNotFound();
            }
            return PartialView(features);
        }

        // POST: Admin/Features/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeatureId,FeatureTitle")] Features features)
        {
            if (ModelState.IsValid)
            {
                db.Entry(features).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_PartialFeaturesTable", db.Features.ToList());
            }
            return PartialView("_PartialFeaturesTable", db.Features.ToList());
        }

        // GET: Admin/Features/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Features features = db.Features.Find(id);
            if (features == null)
            {
                return HttpNotFound();
            }
            return PartialView(features);
        }

        // POST: Admin/Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Features features = db.Features.Find(id);
            db.Features.Remove(features);
            db.SaveChanges();
            return PartialView("_PartialFeaturesTable", db.Features.ToList());
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
