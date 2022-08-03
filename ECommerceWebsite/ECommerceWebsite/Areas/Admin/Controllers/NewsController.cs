using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;
using Utilities;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Title,ShortDescription,Text,IsActive")] News news, HttpPostedFileBase imageName)
        {
            if (ModelState.IsValid)
            {
                if (imageName != null)
                {
                    if (imageName.IsImage())
                    {
                        news.ImageName = imageName.FileName;

                        imageName.SaveAs(Server.MapPath("~/Images/News/Original/" + imageName.FileName));

                        news.CreatedDate = DateTime.Now;
                        news.NumberOfVisits = 0;
                        db.News.Add(news);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("ImageName", Resources.Resource.IsNotImage);
                        return View(news);
                    }
                }
                return View(news);
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "NewsId,Title,ShortDescription,Text,NumberOfVisits,CreatedDate,ImageName,IsActive")] News news, HttpPostedFileBase imageName)
        {
            if (ModelState.IsValid)
            {
                if (imageName != null)
                {
                    if (imageName.IsImage())
                    {
                        if(news.ImageName != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Images/News/Original/" + imageName.FileName));
                        }

                        news.ImageName = imageName.FileName;

                        imageName.SaveAs(Server.MapPath("~/Images/News/Original/" + imageName.FileName));
                    }
                    else
                    {
                        ModelState.AddModelError("ImageName", Resources.Resource.IsNotImage);
                    }
                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return PartialView(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
