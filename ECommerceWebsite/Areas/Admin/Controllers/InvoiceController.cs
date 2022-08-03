using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceWebsite.Models;
using ViewModels.Invoice;
using PagedList;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();

        // GET: Admin/Invoice
        public ActionResult Index(string search, string currentFilter, int? page)
        {
            if (Session["InvoiceItems"] != null)
            {
                Session["InvoiceItems"] = null;
            }

            IQueryable<Invoices> invoices = db.Invoices.OrderByDescending(i => i.InvoiceDate);

            if (!string.IsNullOrWhiteSpace(search))
            {
                page = 1;
            }
            else
            {
                currentFilter = search;
            }

            ViewBag.CurrentFilter = search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                invoices = invoices.Where(i => i.InvoiceNetAmount.ToString() == search).OrderByDescending(i => i.InvoiceDate);
            }

            ViewBag.PageSize = 10;
            int pageSize = ViewBag.PageSize;
            int pageNumber = (page ?? 1);
            return View(invoices.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ShowInvoiceItems()
        {
            List<InvoiceItemsViewModel> invoiceItems = new List<InvoiceItemsViewModel>();

            if (Session["InvoiceItems"] != null)
            {
                invoiceItems = Session["InvoiceItems"] as List<InvoiceItemsViewModel>;
            }

            return PartialView("_PartialShowInvoiceItems", invoiceItems);
        }

        public ActionResult AddInvoiceItems(int id, byte quantity, int fee, int discountFee, int productId, int sizeId)
        {
            List<InvoiceItemsViewModel> invoiceItems = new List<InvoiceItemsViewModel>();

            if (Session["InvoiceItems"] != null)
            {
                invoiceItems = Session["InvoiceItems"] as List<InvoiceItemsViewModel>;
            }

            invoiceItems.Add(new InvoiceItemsViewModel()
            {
                Id = id,
                Quantity = quantity,
                Fee = fee,
                DiscountFee = discountFee,
                ProductId = productId,
                ProductName = db.Products.Where(p => p.ProductId == productId).FirstOrDefault().Title,
                ImageName = db.Products.Where(p => p.ProductId == productId).FirstOrDefault().ImageName,
                Size = db.Product_Sizes.Where(s => s.SizeId == sizeId).Select(s => s.SizeIR).FirstOrDefault(),
                Price = quantity * fee,
                SizeId = sizeId
            });

            Session["InvoiceItems"] = invoiceItems;

            return PartialView("_PartialShowInvoiceItems", invoiceItems);
        }

        public ActionResult DeleteInvoiceItem(int id)
        {
            List<InvoiceItemsViewModel> invoiceItems = new List<InvoiceItemsViewModel>();

            if (Session["InvoiceItems"] != null)
            {
                invoiceItems = Session["InvoiceItems"] as List<InvoiceItemsViewModel>;

                int index = invoiceItems.FindIndex(i => i.Id == id);

                invoiceItems.RemoveAt(index);

                Session["InvoiceItems"] = invoiceItems;
            }

            return PartialView("_PartialShowInvoiceItems", invoiceItems);
        }

        public ActionResult GetProductImage(int id)
        {
            var res = db.Products.Where(p => p.ProductId == id).FirstOrDefault().ImageName;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillSizes(int id)
        {
            var brand = db.Products.Join(db.Product_Categories, p => p.FurthestSubCategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Where(pc => pc.p.ProductId == id).Select(pc => pc.c.CategoryTitle).Distinct().FirstOrDefault();

            var category = db.Products.Join(db.Product_Categories, p => p.SubCategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Where(pc => pc.p.ProductId == id).Select(pc => pc.c.CategoryTitle).Distinct().FirstOrDefault();

            int sizeGroup = db.SizeGroups
                .Where(s => s.Category == category).FirstOrDefault().SizeGroup;

            var sizes = db.Product_Sizes.Where(s => s.SizeGroup == sizeGroup).Select(s => new { SizeId = s.SizeId, Size = s.SizeIR });
            return Json(sizes, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItems invoiceItems = db.InvoiceItems.Find(id);
            if (invoiceItems == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItems);
        }

        // GET: Admin/Invoice/Create
        public ActionResult Create()
        {
            var transactionTypes = new List<TransactionType>();

            transactionTypes.Add(new TransactionType()
            {
                Id = 1,Value = @Resources.Resource.Purchase
            });
            transactionTypes.Add(new TransactionType()
            {
                Id = 2,
                Value = @Resources.Resource.Sale
            });
            transactionTypes.Add(new TransactionType()
            {
                Id = 3,
                Value = @Resources.Resource.ReturnOfSales
            });
            transactionTypes.Add(new TransactionType()
            {
                Id = 4,
                Value = @Resources.Resource.BackFromThePurchase
            });

            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "Title");
            ViewBag.SizeId = new SelectList(db.Product_Sizes.ToList(), "SizeId", "SizeIR");
            ViewBag.TransactionType = new SelectList(transactionTypes, "Id", "Value");
            return View();
        }

        // POST: Admin/Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionType")] Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                if (Session["InvoiceItems"] != null)
                {
                    List<InvoiceItemsViewModel> invoiceItems = Session["InvoiceItems"] as List<InvoiceItemsViewModel>;

                    Invoices invoice = new Invoices();

                    foreach (var item in invoiceItems)
                    {
                        invoice.Quantity += item.Quantity;
                        invoice.Discount += item.Fee - item.DiscountFee;
                        invoice.InvoiceNetAmount += item.DiscountFee * item.Quantity;
                        invoice.InvoiceAmount += item.Fee * item.Quantity;
                    }

                    var dateTime = DateTime.Now;
                    invoice.InvoiceDate = dateTime;
                    invoice.TransactionType = invoices.TransactionType;
                    invoice.Completed = true;
                    invoice.UserId = db.Users.Where(u => u.Username.ToLower().Trim() == User.Identity.Name.ToLower().Trim()).FirstOrDefault().UserId;

                    db.Invoices.Add(invoice);

                    foreach (var item in invoiceItems)
                    {
                        db.InvoiceItems.Add(new InvoiceItems()
                        {
                            Quantity = item.Quantity,
                            Fee = item.Fee,
                            DiscountFee = item.DiscountFee,
                            Price = item.Quantity * item.DiscountFee,
                            SizeId = item.SizeId,
                            ProductId = item.ProductId,
                            InvoiceId = invoice.InvoiceId
                        });

                        var stock = db.Stock.Where(s => s.ProductId == item.ProductId)
                                            .Where(s => s.SizeId == item.SizeId).FirstOrDefault();

                        if (stock != null)
                        {
                            stock.Stock1 = (invoice.TransactionType == 1 || invoice.TransactionType == 3) ? stock.Stock1 + item.Quantity : stock.Stock1 - item.Quantity;
                        }
                        else
                        {
                            db.Stock.Add(new Stock()
                            {
                                ProductId = item.ProductId,
                                SizeId = item.SizeId,
                                WarehouseId = db.Warehouses.Where(w => w.WarehouseName == "انبار اصلی").Select(w => w.WarehouseId).FirstOrDefault(),
                                Stock1 = item.Quantity
                            });
                        }
                    }

                    //ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Title");
                }
                db.SaveChanges();
                Session["InvoiceItems"] = null;
                return RedirectToAction("Index");
            }

            //ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", invoiceItems.InvoiceId);
            return RedirectToAction("Index");
        }

        // GET: Admin/Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            List<InvoiceItemsViewModel> invoiceItems = new List<InvoiceItemsViewModel>();

            //if (Session["InvoiceItems"] != null)
            //{
            //    invoiceItems = Session["InvoiceItems"] as List<InvoiceItemsViewModel>;
            //}

            Session["InvoiceItems"] = null;

            var itemId = 1;

            foreach (var item in db.InvoiceItems.Where(i => i.InvoiceId == id))
            {
                invoiceItems.Add(new InvoiceItemsViewModel()
                {
                    Id = itemId,
                    Quantity = item.Quantity,
                    Fee = item.Fee,
                    DiscountFee = item.DiscountFee,
                    ProductId = item.ProductId,
                    ProductName = db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault().Title,
                    ImageName = db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault().ImageName,
                    Size = db.Product_Sizes.Where(s => s.SizeId == item.SizeId).Select(s => s.SizeIR).FirstOrDefault(),
                    Price = item.Quantity * item.Fee,
                    SizeId = item.SizeId
                });
                itemId++;
            }

            Session["InvoiceItems"] = invoiceItems;

            var transactionTypes = new List<TransactionType>();

            transactionTypes.Add(new TransactionType()
            {
                Id = 1,
                Value = @Resources.Resource.Purchase
            });
            transactionTypes.Add(new TransactionType()
            {
                Id = 2,
                Value = @Resources.Resource.Sale
            });
            transactionTypes.Add(new TransactionType()
            {
                Id = 3,
                Value = @Resources.Resource.ReturnOfSales
            });
            transactionTypes.Add(new TransactionType()
            {
                Id = 4,
                Value = @Resources.Resource.BackFromThePurchase
            });

            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "Title");
            ViewBag.SizeId = new SelectList(db.Product_Sizes.ToList(), "SizeId", "SizeIR");
            ViewBag.TransactionType = new SelectList(transactionTypes, "Id", "Value",db.Invoices.Where(i => i.InvoiceId == id).FirstOrDefault().TransactionType);

            return View();
        }

        // POST: Admin/Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionType")] Invoices invoices,int id)
        {
            if (ModelState.IsValid)
            {
                if (Session["InvoiceItems"] != null)
                {
                    List<InvoiceItemsViewModel> invoiceItems = Session["InvoiceItems"] as List<InvoiceItemsViewModel>;

                    var invoiceToUpdate = db.Invoices.Where(i => i.InvoiceId == id).FirstOrDefault();

                    invoiceToUpdate.Discount = 0;
                    invoiceToUpdate.InvoiceAmount = 0;
                    invoiceToUpdate.InvoiceNetAmount = 0;
                    invoiceToUpdate.Quantity = 0;

                    foreach (var item in invoiceItems)
                    {
                        invoiceToUpdate.Quantity += item.Quantity;
                        invoiceToUpdate.Discount += item.Fee - item.DiscountFee;
                        invoiceToUpdate.InvoiceNetAmount += item.DiscountFee * item.Quantity;
                        invoiceToUpdate.InvoiceAmount += item.Fee * item.Quantity;
                    }

                    var transactionType = invoiceToUpdate.TransactionType;

                    var dateTime = DateTime.Now;
                    invoiceToUpdate.InvoiceDate = dateTime;
                    invoiceToUpdate.TransactionType = invoices.TransactionType;
                    invoiceToUpdate.UserId = db.Users.Where(u => u.Username.ToLower().Trim() == User.Identity.Name.ToLower().Trim()).FirstOrDefault().UserId;

                    //remove previous invoice items
                    var invoiceItemsToRemove = db.InvoiceItems.Where(i => i.InvoiceId == id);

                    //Change Stock                   
                    if (transactionType == 1 || transactionType == 3)
                    {
                        foreach (var item in invoiceItemsToRemove)
                        {
                            db.Stock.Where(s => s.ProductId == item.ProductId && s.SizeId == item.SizeId).FirstOrDefault().Stock1 -= item.Quantity;
                        }
                    }
                    else
                    {
                        foreach (var item in invoiceItemsToRemove)
                        {
                            db.Stock.Where(s => s.ProductId == item.ProductId && s.SizeId == item.SizeId).FirstOrDefault().Stock1 += item.Quantity;
                        }
                    }

                    db.InvoiceItems.RemoveRange(invoiceItemsToRemove);

                    //remove previous invoice
                    var invoiceToRemove = db.Invoices.Where(i => i.InvoiceId == id).FirstOrDefault();

                    //db.Invoices.Remove(invoiceToRemove);

                    //db.Invoices.Add(invoice);
                    db.SaveChanges();

                    var invoiceId = db.Invoices.Where(i => i.InvoiceId == id).FirstOrDefault().InvoiceId;

                    foreach (var item in invoiceItems)
                    {
                        db.InvoiceItems.Add(new InvoiceItems()
                        {
                            Quantity = item.Quantity,
                            Fee = item.Fee,
                            DiscountFee = item.DiscountFee,
                            Price = item.Quantity * item.DiscountFee,
                            SizeId = item.SizeId,
                            ProductId = item.ProductId,
                            InvoiceId = invoiceId
                        });

                        // Add To Stock
                        var stockToEdit = db.Stock.Where(s => s.ProductId == item.ProductId)
                                                  .Where(s => s.SizeId == item.SizeId).FirstOrDefault();

                        stockToEdit.Stock1 = (invoices.TransactionType == 1 || invoices.TransactionType == 3) ? stockToEdit.Stock1 + item.Quantity : stockToEdit.Stock1 - item.Quantity;                                               
                    }
                }
                db.SaveChanges();
                Session["InvoiceItems"] = null;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoices invoice = db.Invoices.Find(id);

            if (invoice == null)
            {
                return HttpNotFound();
            }
            return PartialView(invoice);
        }

        // POST: Admin/Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoiceItems = db.InvoiceItems.Where(i => i.InvoiceId == id);
            db.InvoiceItems.RemoveRange(invoiceItems);

            var invoiceToEdit = db.Invoices.Where(i => i.InvoiceId == id).FirstOrDefault();

            foreach (var item in invoiceItems)
            {
                var stockToEdit = db.Stock.Where(s => s.ProductId == item.ProductId)
                                           .Where(s => s.SizeId == item.SizeId).FirstOrDefault();

                stockToEdit.Stock1 = (invoiceToEdit.TransactionType == 1 || invoiceToEdit.TransactionType == 3) ? stockToEdit.Stock1 - item.Quantity : stockToEdit.Stock1 + item.Quantity;    
            }

            Invoices invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
