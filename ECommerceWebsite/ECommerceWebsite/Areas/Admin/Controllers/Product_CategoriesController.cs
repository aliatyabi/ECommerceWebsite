using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;
using ECommerceWebsite.Areas.Admin.Models;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class Product_CategoriesController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Product_Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nodes()
        {
            var nodes = new List<JsTreeModel>();

            foreach (var item in db.Product_Categories)
            {
                var jsTreeModel = new JsTreeModel();

                jsTreeModel.id = item.CategoryId.ToString();
                jsTreeModel.text = item.CategoryTitle;
                jsTreeModel.selected = false;
                jsTreeModel.state = new JsTreeNodeState() { opened = true,selected = false };

                if (item.ParentId == null)
                {
                    jsTreeModel.parent = "#";
                }
                else
                {
                    jsTreeModel.parent = item.ParentId.ToString();   
                }

                nodes.Add(jsTreeModel);
            }

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Product_Categories/Details/5
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

        // GET: Admin/Product_Categories/Create
        public ActionResult Create(int? id)
        {
            return PartialView();
        }

        // POST: Admin/Product_Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryTitle")] Product_Categories product_Categories, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    product_Categories.ParentId = id;
                }
                db.Product_Categories.Add(product_Categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView();
        }

        // GET: Admin/Product_Categories/Edit/5
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
            
            return PartialView(product_Categories);
        }

        // POST: Admin/Product_Categories/Edit/5
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
            
            return PartialView(product_Categories);
        }

        // GET: Admin/Product_Categories/Delete/5
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
            return PartialView(product_Categories);
        }

        // POST: Admin/Product_Categories/Delete/5
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
