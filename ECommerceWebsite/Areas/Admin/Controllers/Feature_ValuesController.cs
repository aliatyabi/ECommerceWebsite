using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;
using ViewModels.Product;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class Feature_ValuesController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Feature_Values
        public ActionResult Index()
        {
            return View(db.Feature_Values.ToList());
        }

        public ActionResult ShowFeatureValuesList()
        {
            return PartialView("_PartialFeatureValuesTable", db.Feature_Values.ToList());
        }

        // GET: Admin/Feature_Values/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature_Values feature_Values = db.Feature_Values.Find(id);
            if (feature_Values == null)
            {
                return HttpNotFound();
            }
            return View(feature_Values);
        }

        // GET: Admin/Feature_Values/Create
        public ActionResult Create()
        {
            ViewBag.FeatureId = new SelectList(db.Features.Distinct(), "FeatureId", "FeatureTitle");
            return PartialView();
        }

        // POST: Admin/Feature_Values/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeatureId,FeatureValueId,FeatureValue")] Feature_Values feature_Values)
        {
            if (ModelState.IsValid)
            {
                ViewBag.FeatureId = new SelectList(db.Features.Distinct(), "FeatureId", "FeatureTitle", feature_Values.FeatureId);
                db.Feature_Values.Add(feature_Values);
                db.SaveChanges();
                return PartialView("_PartialFeatureValuesTable", db.Feature_Values.ToList());
            }

            return PartialView("_PartialFeatureValuesTable", db.Feature_Values.ToList());
        }

        // GET: Admin/Feature_Values/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature_Values feature_Values = db.Feature_Values.Find(id);
            //Feature_ValuesViewModel model = new Feature_ValuesViewModel();

            //model.FeatureValueId = (int)id;
            //model.FeatureValue = feature_Values.FeatureValue;

            if (feature_Values == null)
            {
                return HttpNotFound();
            }
            return PartialView(feature_Values);
        }

        // POST: Admin/Feature_Values/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeatureId,FeatureValueId,FeatureValue")] Feature_Values feature_Values)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature_Values).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_PartialFeatureValuesTable", db.Feature_Values.ToList());
            }
            return PartialView("_PartialFeatureValuesTable", db.Feature_Values.ToList());
        }

        // GET: Admin/Feature_Values/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feature_Values feature_Values = db.Feature_Values.Find(id);
            if (feature_Values == null)
            {
                return HttpNotFound();
            }
            return PartialView(feature_Values);
        }

        // POST: Admin/Feature_Values/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature_Values feature_Values = db.Feature_Values.Find(id);
            db.Feature_Values.Remove(feature_Values);
            db.SaveChanges();
            return PartialView("_PartialFeatureValuesTable", db.Feature_Values.ToList());
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
