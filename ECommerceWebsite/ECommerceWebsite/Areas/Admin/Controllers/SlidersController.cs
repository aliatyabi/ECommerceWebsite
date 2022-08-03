using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class SlidersController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }

        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // GET: Admin/Sliders/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideTitle,Link,NewTab")] Sliders sliders,HttpPostedFileBase slideImage)
        {
            if (ModelState.IsValid)
            {
                if(slideImage != null)
                {
                    sliders.Image = Guid.NewGuid().ToString() + Path.GetExtension(slideImage.FileName);

                    slideImage.SaveAs(Server.MapPath("~/Images/Sliders/Original/" + sliders.Image));

                    sliders.ClickCount = 0;
                    db.Sliders.Add(sliders);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Image", "Error");
                }
            }

            return View(sliders);
        }

        // GET: Admin/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return PartialView(sliders);
        }

        // POST: Admin/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideId,SlideTitle,Link,Image,ClickCount,NewTab")] Sliders sliders, HttpPostedFileBase slideImage)
        {
            if (ModelState.IsValid)
            {
                if (slideImage != null)
                {
                    System.IO.File.Delete(Server.MapPath("~/Images/Sliders/Original/" + sliders.Image));

                    sliders.Image = Guid.NewGuid().ToString() + Path.GetExtension(slideImage.FileName);

                    slideImage.SaveAs(Server.MapPath("~/Images/Sliders/Original/" + sliders.Image));

                    db.Entry(sliders).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(sliders);
        }

        // GET: Admin/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sliders sliders = db.Sliders.Find(id);
            db.Sliders.Remove(sliders);
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
