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
using System.IO;
using ViewModels.Product;
using PagedList;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ECommerceWebsiteEntities db = new ECommerceWebsiteEntities();
        //List<string> removedPics = new List<string>();

        // GET: Admin/Products
        public ActionResult Index(string search, string currentFilter, int? page)
        {
            var products = db.Products.Include(p => p.Product_Categories).Include(p => p.Product_Categories1).OrderBy(p => p.CreatedDate);

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
                products = products.Where(p => p.Title.Contains(search) || p.Text.Contains(search) || p.Article.Contains(search)).OrderBy(p => p.CreatedDate);
            }

            ViewBag.PageSize = 10;
            int pageSize = ViewBag.PageSize;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        public ActionResult FillGenders(int? brand)
        {
            var genders = db.Product_Categories
                .Where(g => g.ParentId == brand)
                .Select(g => new
                {
                    g.CategoryId,
                    g.CategoryTitle,
                    g.ParentId
                });

            return Json(genders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillDivisions(int genderId)
        {
            var divisions = db.Product_Categories
                .Where(g => g.ParentId == genderId)
                .Select(g => new
                {
                    g.CategoryId,
                    g.CategoryTitle,
                    g.ParentId
                });

            List<SelectListItem> subCategories = new List<SelectListItem>();

            foreach (var item in divisions)
            {
                SelectListItem division = new SelectListItem();
                division.Text = item.CategoryTitle;
                division.Value = item.CategoryId.ToString();
                division.Group = new SelectListGroup { Name = "Division" };
                subCategories.Add(division);
            }

            ViewBag.SubCategoryId = new SelectList(subCategories, "Value", "Text", "Group.Name", 1);
            TempData["genderId"] = genderId;

            //return Json(divisions, JsonRequestBehavior.AllowGet);
            return PartialView("_PartialProductSubCategory", subCategories);
        }

        public ActionResult FillSportsCode(int divisionId)
        {
            var sportsCode = db.Product_Categories
                .Where(g => g.ParentId == divisionId)
                .Select(g => new
                {
                    g.CategoryId,
                    g.CategoryTitle,
                    g.ParentId
                });

            List<SelectListItem> subCategories = new List<SelectListItem>();

            foreach (var item in sportsCode)
            {
                SelectListItem sports = new SelectListItem();
                sports.Text = item.CategoryTitle;
                sports.Value = item.CategoryId.ToString();
                sports.Group = new SelectListGroup { Name = "SportsCode" };
                subCategories.Add(sports);
            }

            ViewBag.SubCategoryId = new SelectList(subCategories, "Value", "Text", "Group.Name", 1);

            TempData["divisionId"] = divisionId;

            //return Json(divisions, JsonRequestBehavior.AllowGet);
            return PartialView("_PartialProductSubCategories", subCategories);
        }

        public ActionResult GetSportsCode(int sportsCodeId)
        {
            TempData["sportsCode"] = sportsCodeId;
            //TempData["thumbPic"] = thumbPicName;
            return Json(sportsCodeId, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult FillSecondDropDown(int? divisionId)
        //{
        //    var sportsCodes = db.Product_Categories
        //        .Where(g => g.ParentId == divisionId)
        //        .Select(g => new
        //        {
        //            g.CategoryId,
        //            g.CategoryTitle,
        //            g.ParentId
        //        });

        //    return Json(sportsCodes, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetCityByStaeId(int genderid)
        //{
        //    List<City> objcity = new List<City>();
        //    objcity = GetAllCity().Where(m => m.StateId == stateid).ToList();
        //    SelectList obgcity = new SelectList(objcity, "Id", "CityName", 0);
        //    return Json(obgcity);
        //}

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            var brands = db.Product_Categories.Where(p => p.ParentId == null).Distinct().ToList();

            var brandIds = (from b in db.Product_Categories
                            where b.ParentId == null
                            select b.CategoryId).ToList();

            List<SelectListItem> categories = new List<SelectListItem>();

            foreach (var item in brands)
            {
                SelectListItem brand = new SelectListItem();
                brand.Text = item.CategoryTitle;
                brand.Value = item.CategoryId.ToString();
                brand.Group = new SelectListGroup { Name = @Resources.Resource.Gender };
                categories.Add(brand);
            }

            ViewBag.CategoryId = new SelectList(categories, "Value", "Text", "Group.Name", 1);
            ViewBag.FeaturesId = new SelectList(db.Features, "FeatureId", "FeatureTitle");
            //ViewBag.FeatureValues = new SelectList(db.Feature_Values, "FeatureValueId", "FeatureValue");

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,SubCategoryId,FurtherSubCategoryId,FurtherSubCategoryId,Title,Article,ShortDescription,Text,ImageName,Fee,DiscountPercent,CreatedDate,Status,Comment")]
            Products products, HttpPostedFileBase[] imageUpload, string metaTitle, string metaDescription, string metaKeywords)
        {
            if (ModelState.IsValid)
            {
                string mainPic = TempData["mainPic"].ToString();
                var genderId = TempData["genderId"];
                var divisionId = TempData["divisionId"];
                var sportsCodeId = TempData["sportsCode"];

                //var thumbPic = TempData["thumbPic"];

                products.DiscountPercent = products.DiscountPercent;
                products.ImageName = "no-image.png";

                products.SubCategoryId = Convert.ToInt32(genderId);
                products.FurtherSubCategoryId = Convert.ToInt32(divisionId);
                products.FurthestSubCategoryId = Convert.ToInt32(sportsCodeId);

                products.CreatedDate = DateTime.Now;

                db.Products.Add(products);

                if (!string.IsNullOrEmpty(metaKeywords))
                {
                    string[] tagsArray = metaKeywords.Split('-');

                    foreach (var tag in tagsArray)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductId = products.ProductId,
                            MetaTitle = metaTitle,
                            MetaDescription = metaDescription,
                            MetaKeyword = tag.Trim()
                        });
                    }
                }

                if(Session["features"] != null)
                {
                    List<ViewModels.Product.Feature_ValuesViewModel> features = Session["features"] as List<ViewModels.Product.Feature_ValuesViewModel>;

                    foreach (var item in features)
                    {
                        if (!db.Feature_Values.Where(f => f.FeatureId == item.FeatureId && f.FeatureValue == item.FeatureValue).Any())
                        {
                            db.Feature_Values.Add(new Feature_Values()
                            {
                                FeatureId = item.FeatureId,
                                FeatureValue = item.FeatureValue
                            });
                        }

                        db.SaveChanges();

                        db.Products_Features.Add(new Products_Features()
                        {
                            ProductId = products.ProductId,
                            FeatureValueId = db.Feature_Values.Where(f => f.FeatureId == item.FeatureId && f.FeatureValue == item.FeatureValue).FirstOrDefault().FeatureValueId
                        });
                    }

                    Session["features"] = null;
                }

                db.SaveChanges();

                if (imageUpload != null && imageUpload.Any())
                {
                    foreach (var image in imageUpload)
                    {
                        var extension = Path.GetExtension(image.FileName);
                        var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        string imageName = fileName + "_" + products.ProductId + extension;
                        image.SaveAs(Server.MapPath("/Images/Products/Original/" + imageName));

                        ImageResizer imageResizer = new ImageResizer();
                        imageResizer.Resize(Server.MapPath("/Images/Products/Original/" + imageName),
                            Server.MapPath("/Images/Products/Thumb/" + imageName));

                        db.Product_Gallery.Add(new Product_Gallery()
                        {
                            ProductId = products.ProductId,
                            ImageName = imageName
                        });
                    }

                    int index = mainPic.LastIndexOf('.');
                    int numOfTotalCharacters = mainPic.Length;

                    string mainPicWithoutExtension = mainPic.Substring(0, index);
                    int numOfCharacters = mainPicWithoutExtension.Length;
                    int remaining = numOfTotalCharacters - numOfCharacters;
                    string mainPicExtension = mainPic.Substring(numOfCharacters, remaining);

                    products.ImageName = mainPicWithoutExtension + "_" + products.ProductId + mainPicExtension;

                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            var brands = db.Product_Categories.Where(p => p.ParentId == null);

            var brandIds = (from b in db.Product_Categories
                            where b.ParentId == null
                            select b.CategoryId).ToList();


            var gender = db.Product_Categories.Where(g => brandIds.Contains(g.ParentId.Value)).ToList();


            ViewBag.CategoryId = new SelectList(db.Product_Categories, "CategoryId", "CategoryTitle", products.CategoryId);

            ViewBag.FeaturesId = new SelectList(db.Features, "FeatureId", "FeatureTitle");

            //ViewBag.FeatureValues = new SelectList(db.Feature_Values, "FeatureValueId", "FeatureValue");
            
            //ViewBag.SubCategoryId = new SelectList(db.Product_Categories, "CategoryId", "CategoryTitle", products.SubCategoryId);
            return View(products);
        }

        public ActionResult Images(string mainPicName/*, string thumbPicName*/)
        {
            TempData["mainPic"] = mainPicName;
            //TempData["thumbPic"] = thumbPicName;
            return Json(mainPicName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveImages(string[] removedPics)
        {
            if (removedPics != null)
            {
                List<string> myList = new List<string>();

                myList.AddRange(removedPics);

                TempData["removedPics"] = myList;

                TempData.Keep();
            }

            return Json(removedPics, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetCategory(int categoryId)
        {
            var model = db.Product_Categories.Where(c => c.ParentId == null);

            return PartialView("_PartialCatgories", model);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            var brands = db.Product_Categories.Where(p => p.ParentId == null).Distinct().ToList();

            var selectedBrand = db.Products.Where(p => p.ProductId == id).First().CategoryId;

            var genders = db.Product_Categories.Where(p => p.ParentId == selectedBrand).Distinct().ToList();

            var selectedGender = db.Products.Where(p => p.ProductId == id).First().SubCategoryId;
            var selectedDivision = db.Products.Where(p => p.ProductId == id).First().FurtherSubCategoryId;
            var selectedSportsCode = db.Products.Where(p => p.ProductId == id).First().FurthestSubCategoryId;

            var divisions = db.Product_Categories.Where(p => p.ParentId == selectedGender).Distinct().ToList();

            var sportsCode = db.Product_Categories.Where(p => p.ParentId == selectedDivision).Distinct().ToList();

            var brandIds = (from b in db.Product_Categories
                            where b.ParentId == null
                            select b.CategoryId).ToList();

            ViewBag.brandCount = db.Product_Categories.Where(p => p.ParentId == null).Count();
            ViewBag.divisionCount = db.Product_Categories.Where(p => p.ParentId == selectedGender).Distinct().ToList().Count;
            ViewBag.sportsCount = db.Product_Categories.Where(p => p.ParentId == selectedDivision).Distinct().ToList().Count;

            List<SelectListItem> categories = new List<SelectListItem>();

            List<int> selectedItems = new List<int>();
            selectedItems.Add(selectedBrand);
            selectedItems.Add(selectedGender);

            //List<SelectListItem> subCategories = new List<SelectListItem>();

            foreach (var item in brands)
            {
                SelectListItem brand = new SelectListItem();
                brand.Text = item.CategoryTitle;
                brand.Value = item.CategoryId.ToString();
                brand.Group = new SelectListGroup { Name = @Resources.Resource.Gender };
                categories.Add(brand);
            }

            foreach (var item in genders)
            {
                SelectListItem gender = new SelectListItem();
                gender.Text = item.CategoryTitle;
                gender.Value = item.CategoryId.ToString();
                gender.Group = new SelectListGroup { Name = @Resources.Resource.Category };
                categories.Add(gender);
            }

            //ViewBag.CategoryId = new SelectList(categories, "Value", "Text", "Group.Name", selectedBrand);

            ViewBag.CategoryId = new MultiSelectList(categories, "Value", "Text", "Group.Name", selectedItems);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> FurtherSubCategories = new List<SelectListItem>();

            foreach (var item in divisions)
            {
                SelectListItem division = new SelectListItem();
                division.Text = item.CategoryTitle;
                division.Value = item.CategoryId.ToString();
                division.Group = new SelectListGroup { Name = @Resources.Resource.Division };
                FurtherSubCategories.Add(division);
            }

            List<SelectListItem> FurthestSubCategories = new List<SelectListItem>();

            foreach (var item in sportsCode)
            {
                SelectListItem sportCode = new SelectListItem();
                sportCode.Text = item.CategoryTitle;
                sportCode.Value = item.CategoryId.ToString();
                sportCode.Group = new SelectListGroup { Name = @Resources.Resource.SportsCode };
                FurthestSubCategories.Add(sportCode);
            }

            List<int> selectedDivisionId = new List<int>();
            selectedDivisionId.Add(selectedDivision);

            List<int> selectedSportsCodeId = new List<int>();
            selectedSportsCodeId.Add(selectedSportsCode);

            var featureValueIds = products.Products_Features.Select(f => f.FeatureValueId);

            List<ViewModels.Product.Feature_ValuesViewModel> features = new List<ViewModels.Product.Feature_ValuesViewModel>();

            foreach (var item in db.Feature_Values)
            {
                if (featureValueIds.Contains(item.FeatureValueId))
                {
                    features.Add(new ViewModels.Product.Feature_ValuesViewModel()
                    {
                        FeatureId = item.FeatureId,
                        FeatureValue = item.FeatureValue,
                        FeatureValueId = item.FeatureValueId,
                        FeatureTitle = item.Features.FeatureTitle
                    });
                }
            }

            Session["features"] = features.ToList();

            //ViewBag.CategoryId = new SelectList(db.Product_Categories.Where(p => p.ParentId == null), "CategoryId", "CategoryTitle", products.CategoryId);

            //ViewBag.ParentCategory = db.Product_Categories.Where(c => c.ParentId == null);

            //ViewBag.ParentCategoryId = products.CategoryId;
            //ViewBag.ParentCategoryTitle = db.Product_Categories.Where(c => c.CategoryId == products.CategoryId).First().CategoryTitle;

            //ViewBag.SubCategoryId = db.Product_Categories.Where(c => c.ParentId == products.Product_Categories.CategoryId).First().CategoryId;
            //ViewBag.SubCategoryTitle = db.Product_Categories.Where(c => c.ParentId == products.Product_Categories.CategoryId).First().CategoryTitle;

            //ViewBag.SubCategory = db.Product_Categories.Where(c => c.ParentId == products.CategoryId);

            // ----------------------------------------
            //var parentCategories = db.Product_Categories.Where(p => p.ParentId == null);

            //var subCategories = db.Product_Categories.Where(c => c.ParentId == products.CategoryId);

            //var FurtherSubCategories = db.Product_Categories.Where(c => c.Product_Categories2.CategoryId == products.SubCategoryId);

            //var FurthestSubCategories = db.Product_Categories.Where(c => c.Product_Categories2.CategoryId == products.FurtherSubCategoryId);

            ViewBag.SubCategoryId = new MultiSelectList(FurtherSubCategories, "Value", "Text", "Group.Name", selectedDivisionId);

            ViewBag.SubCategoriesId = new MultiSelectList(FurthestSubCategories, "Value", "Text", "Group.Name", selectedSportsCodeId);

            ViewBag.FeaturesId = new SelectList(db.Features, "FeatureId", "FeatureTitle");

            //ViewBag.CategoryId = new SelectList(parentCategories, "CategoryId", "CategoryTitle", products.CategoryId);
            //ViewBag.SubCategoryId = new SelectList(subCategories, "CategoryId", "CategoryTitle", products.SubCategoryId);
            //ViewBag.FurtherSubCategoryId = new SelectList(FurtherSubCategories, "CategoryId", "CategoryTitle", products.FurtherSubCategoryId);
            //ViewBag.FurthestSubCategoryId = new SelectList(FurthestSubCategories, "CategoryId", "CategoryTitle", products.FurthestSubCategoryId);


            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,SubCategoryId,FurtherSubCategoryId,FurthestSubCategoryId,Title,Article,ShortDescription,Text,ImageName,Fee,DiscountPercent,CreatedDate,Status,Comment")] 
            Products products, HttpPostedFileBase[] imageUpload, string metaTitle, string metaDescription, string metaKeywords)
        {
            if (products != null && ModelState.IsValid)
            {
                var genderId = products.SubCategoryId;
                var divisionId = products.FurtherSubCategoryId;
                var sportsCodeId = products.FurthestSubCategoryId;

                //old images of edited product
                var oldOriginalPics = db.Product_Gallery.Where(p => p.ProductId == products.ProductId)
                    .Select(p => p.ImageName).ToList();

                if (TempData["genderId"] != null)
                {
                    genderId = (int)TempData["genderId"];
                }

                if (TempData["divisionId"] != null)
                {
                    divisionId = (int)TempData["divisionId"];
                }

                if (TempData["sportsCode"] != null)
                {
                    sportsCodeId = (int)TempData["sportsCode"];
                }

                //var thumbPic = TempData["thumbPic"];

                //products.ImageName = "no-image.png";
                products.SubCategoryId = Convert.ToInt32(genderId);
                products.FurtherSubCategoryId = Convert.ToInt32(divisionId);
                products.FurthestSubCategoryId = Convert.ToInt32(sportsCodeId);

                products.CreatedDate = DateTime.Now;

                //if (!string.IsNullOrEmpty(metaKeywords))
                //{
                //    string[] tagsArray = metaKeywords.Split('-');

                //    foreach (var tag in tagsArray)
                //    {
                //        db.Product_Tags.Add(new Product_Tags()
                //        {
                //            ProductId = products.ProductId,
                //            MetaTitle = metaTitle,
                //            MetaDescription = metaDescription,
                //            MetaKeyword = tag.Trim()
                //        });
                //    }
                //}

                string mainPic = products.ImageName;

                if (imageUpload != null && imageUpload.Any() && imageUpload[0] != null)
                {
                    foreach (var image in imageUpload)
                    {
                        var extension = Path.GetExtension(image.FileName);
                        var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        string imageName = fileName + "_" + products.ProductId + extension;
                        image.SaveAs(Server.MapPath("/Images/Products/Original/" + imageName));

                        ImageResizer imageResizer = new ImageResizer();
                        imageResizer.Resize(Server.MapPath("/Images/Products/Original/" + imageName),
                            Server.MapPath("/Images/Products/Thumb/" + imageName));

                        //var oldGallery = products.Product_Gallery.Select(p => p.ProductId == products.ProductId);

                        if (TempData["removedPics"] != null)
                        {
                            List<string> removedPics = (List<string>)TempData["removedPics"];

                            foreach (var item in removedPics)
                            {
                                var galleryToRemove = products.Product_Gallery.Where(g => g.ImageName == item)
                                    .Where(g => g.ProductId == products.ProductId).FirstOrDefault();

                                products.Product_Gallery.Remove(galleryToRemove);
                            }
                        }

                        db.Product_Gallery.Add(new Product_Gallery()
                        {
                            ProductId = products.ProductId,
                            ImageName = imageName
                        });
                    }
                }

                if (TempData["removedPics"] != null)
                {
                    List<string> removedPics = (List<string>)TempData["removedPics"];

                    db.Entry(products).State = EntityState.Modified;

                    foreach (var item in removedPics)
                    {
                        //var galleryToRemove = products.Product_Gallery.Where(g => g.ImageName == item)
                        //    .Where(g => g.ProductId == products.ProductId).FirstOrDefault();

                        //products.Product_Gallery.Remove(galleryToRemove);
                        var galleryToRemove = db.Product_Gallery.Where(g => g.ImageName == item)
                            .Where(g => g.ProductId == products.ProductId).FirstOrDefault();
                        db.Product_Gallery.Remove(galleryToRemove);
                    }
                }

                if (TempData["mainPic"] != null)
                {
                    mainPic = TempData["mainPic"].ToString();

                    //int index = mainPic.LastIndexOf('.');
                    //int numOfTotalCharacters = mainPic.Length;

                    //string mainPicWithoutExtension = mainPic.Substring(0, index);
                    //int numOfCharacters = mainPicWithoutExtension.Length;
                    //int remaining = numOfTotalCharacters - numOfCharacters;
                    //string mainPicExtension = mainPic.Substring(numOfCharacters, remaining);

                    //products.ImageName = mainPicWithoutExtension + "_" + products.ProductId + mainPicExtension;
                }

                products.ImageName = mainPic;

                //var brands = db.Product_Categories.Where(p => p.ParentId == null).Distinct().ToList();

                //var brandIds = (from b in db.Product_Categories
                //                where b.ParentId == null
                //                select b.CategoryId).ToList();

                //List<SelectListItem> categories = new List<SelectListItem>();

                //foreach (var item in brands)
                //{
                //    SelectListItem brand = new SelectListItem();
                //    brand.Text = item.CategoryTitle;
                //    brand.Value = item.CategoryId.ToString();
                //    brand.Group = new SelectListGroup { Name = @Resources.Resource.Brand };
                //    categories.Add(brand);
                //}

                //ViewBag.CategoryId = new SelectList(categories, "Value", "Text", "Group.Name", 1);

                //Remove all tags
                db.Product_Tags.Where(t => t.ProductId == products.ProductId).ToList().ForEach(t => db.Product_Tags.Remove(t));

                //Insert new tags
                if (!string.IsNullOrEmpty(metaKeywords))
                {
                    string[] keywords = metaKeywords.Split('-');

                    foreach (string keyword in keywords)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductId = products.ProductId,
                            MetaTitle = metaTitle,
                            MetaDescription = metaDescription,
                            MetaKeyword = keyword.Trim()
                        });
                    }
                }

                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();

                TempData.Remove("mainPic");
                TempData.Remove("genderId");
                TempData.Remove("divisionId");
                TempData.Remove("sportsCode");
                TempData.Remove("removedPics");

                //New Images where saved for edited product
                var productImages = db.Product_Gallery.Where(p => p.ProductId == products.ProductId)
                    .Select(p => p.ImageName);

                //Remove all features
                db.Products_Features.Where(f => f.ProductId == products.ProductId).ToList().ForEach(f => db.Products_Features.Remove(f));

                db.SaveChanges();

                var valueIds = db.Products_Features.Where(f => f.ProductId == products.ProductId).Select(f => f.FeatureValueId).ToList();

                foreach (var item in valueIds)
	            {
                    db.Feature_Values.Where(f => f.FeatureValueId == item).ToList().ForEach(f => db.Feature_Values.Remove(f));
	            }

                db.SaveChanges();
              
                //Add new features 
                if (Session["features"] != null)
                {
                    List<ViewModels.Product.Feature_ValuesViewModel> features = Session["features"] as List<ViewModels.Product.Feature_ValuesViewModel>;

                    foreach (var item in features)
                    {
                        if (!db.Feature_Values.Where(f => f.FeatureId == item.FeatureId && f.FeatureValue == item.FeatureValue).Any())
                        {
                            db.Feature_Values.Add(new Feature_Values()
                            {
                                FeatureId = item.FeatureId,
                                FeatureValue = item.FeatureValue,
                            });
                        }

                        db.SaveChanges();

                        db.Products_Features.Add(new Products_Features()
                        {
                            ProductId = products.ProductId,
                            FeatureValueId = db.Feature_Values.Where(f => f.FeatureId == item.FeatureId && f.FeatureValue == item.FeatureValue).FirstOrDefault().FeatureValueId
                        });

                        db.SaveChanges();
                    }

                    Session["features"] = null;
                }

                //Delete images where not for edited product and are in photos directory
                foreach (var img in oldOriginalPics)
	            {
                    if (!productImages.Contains(img))
                    {
                        if (System.IO.File.Exists(Server.MapPath("/Images/Products/Original/" + img)))
                        {
                            System.IO.File.Delete(Server.MapPath("/Images/Products/Original/" + img));
                        }

                        if (System.IO.File.Exists(Server.MapPath("/Images/Products/Thumb/" + img)))
                        {
                            System.IO.File.Delete(Server.MapPath("/Images/Products/Thumb/" + img));
                        }
                    }
	            }

                return RedirectToAction("Index");
            }

            var FurtherSubCategories = db.Product_Categories.Where(c => c.Product_Categories2.CategoryId == products.SubCategoryId);

            var FurthestSubCategories = db.Product_Categories.Where(c => c.Product_Categories2.CategoryId == products.FurtherSubCategoryId);

            ViewBag.SubCategoryId = new SelectList(FurtherSubCategories, "CategoryId", "CategoryTitle", products.FurtherSubCategoryId);
            ViewBag.SubCategoriesId = new SelectList(FurthestSubCategories, "CategoryId", "CategoryTitle", products.FurthestSubCategoryId);
            return View(products);
        }

        public ActionResult AddFeature(int id, string featureValue)
        {
            List<ViewModels.Product.Feature_ValuesViewModel> features = new List<ViewModels.Product.Feature_ValuesViewModel>();

            if (Session["features"] != null)
            {
                features = Session["features"] as List<ViewModels.Product.Feature_ValuesViewModel>;
            }

            features.Add(new ViewModels.Product.Feature_ValuesViewModel()
            {
                FeatureId = id,
                FeatureValue = featureValue,
                FeatureTitle = db.Features.Where(f => f.FeatureId == id).Select(f => f.FeatureTitle).FirstOrDefault()
            });

            Session["features"] = features;

            return PartialView("_PartialShowFeatures", features);
        }

        public ActionResult DeleteFeature(int id,string featureValue)
        {
            List<ViewModels.Product.Feature_ValuesViewModel> features = new List<ViewModels.Product.Feature_ValuesViewModel>();

            if (Session["features"] != null)
            {
                features = Session["features"] as List<ViewModels.Product.Feature_ValuesViewModel>;

                int index = features.FindIndex(f => f.FeatureId == id && f.FeatureValue == featureValue);
                features.RemoveAt(index);

                Session["features"] = features;
            }

            return PartialView("_PartialShowFeatures", features);
        }

        public ActionResult ShowFeatures()
        {
            List<ViewModels.Product.Feature_ValuesViewModel> features = new List<ViewModels.Product.Feature_ValuesViewModel>();

            if (Session["features"] != null)
            {
                features = Session["features"] as List<ViewModels.Product.Feature_ValuesViewModel>;
            }

            return PartialView("_PartialShowFeatures", features);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return PartialView(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pics = db.Product_Gallery.Where(p => p.ProductId == id)
                .Select(p => p.ImageName).ToList();

            Products products = db.Products.Find(id);
            db.Products.Remove(products);

            db.Product_Tags.Where(t => t.ProductId == id).ToList().ForEach(t => db.Product_Tags.Remove(t));

            db.Product_Gallery.Where(g => g.ProductId == id).ToList().ForEach(g => db.Product_Gallery.Remove(g));

            db.Products_Features.Where(f => f.ProductId == id).ToList().ForEach(f => db.Products_Features.Remove(f));

            db.SaveChanges();

            var valueIds = db.Products_Features.Where(f => f.ProductId == id).Select(f => f.FeatureValueId).ToList();

            foreach (var item in valueIds)
            {
                db.Feature_Values.Where(f => f.FeatureValueId == item).ToList().ForEach(f => db.Feature_Values.Remove(f));
            }

            db.SaveChanges();

            foreach (var img in pics)
            {
                if (System.IO.File.Exists(Server.MapPath("/Images/Products/Original/" + img)))
                {
                    System.IO.File.Delete(Server.MapPath("/Images/Products/Original/" + img));
                }

                if (System.IO.File.Exists(Server.MapPath("/Images/Products/Thumb/" + img)))
                {
                    System.IO.File.Delete(Server.MapPath("/Images/Products/Thumb/" + img));
                }
            }

            return RedirectToAction("Index");
        }

        public JsonResult ShowFeatureValues(int id)
        {
            var featureValues = db.Feature_Values.Where(f => f.FeatureId == id).Select(f => new { f.FeatureValueId, f.FeatureValue });
            return Json(featureValues, JsonRequestBehavior.AllowGet);
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
