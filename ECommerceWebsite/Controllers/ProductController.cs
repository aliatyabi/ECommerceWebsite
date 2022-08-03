using ECommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Product;
using ViewModels.ShoppingCart;
using PagedList;
using Microsoft.AspNet.Identity;
using Utilities.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace ECommerceWebsite.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index(string search, string[] colors, int? minPrice, int? maxPrice, int? page, string[] brands, int? categoryId, int? subCategoryId, int? furtherCategoryId, int? inStock, string sortOrder = "opt3")
        {
            ViewBag.MinPrice = db.Products.Select(p => p.Fee).Min();
            ViewBag.MaxPrice = db.Products.Select(p => p.Fee).Max();

            if (minPrice == null) { minPrice = ViewBag.MinPrice; }
            if (maxPrice == null) { maxPrice = ViewBag.MaxPrice; }

            //ViewBag.MinPrice = minPrice;
            //ViewBag.MaxPrice = maxPrice;

            IQueryable<Products> products = db.Products;

            List<Products> filter = new List<Products>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                filter.AddRange(db.Product_Tags.Where(t => t.MetaTitle.ToLower() == search.ToLower()).Select(t => t.Products));
                filter.AddRange(db.Product_Tags.Where(t => t.MetaKeyword.ToLower().Contains(search.ToLower())).Select(t => t.Products));
                filter.AddRange(db.Product_Tags.Where(t => t.MetaDescription.ToLower().Contains(search.ToLower())).Select(t => t.Products));
                filter.AddRange(db.Products.Where(p => p.Title.ToLower().Contains(search.ToLower())));
                filter.AddRange(db.Products.Where(p => p.Text.ToLower().Contains(search.ToLower())));
                filter.AddRange(db.Products.Where(p => p.ShortDescription.ToLower().Contains(search.ToLower())));

                products = filter.AsQueryable();
                products = products.Distinct();
            }

            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            if (subCategoryId != null)
            {
                products = products.Where(p => p.SubCategoryId == subCategoryId);
            }

            if (furtherCategoryId != null)
            {
                products = products.Where(p => p.FurtherSubCategoryId == furtherCategoryId);
            }

            if (brands != null)
            {
                if (brands.Length != 0 && brands[0] != "")
                {
                    foreach (var item in brands)
                    {
                        filter.AddRange(products.Where(p => p.Product_Categories3.CategoryTitle.Contains(item)));
                    }
                    products = filter.AsQueryable();
                    products = products.Distinct();
                }
            }

            List<int> productIds = new List<int>();

            if (colors != null)
            {
                if (colors.Length != 0 && colors[0] != "")
                {
                    ViewBag.Colors = colors;

                    foreach (var item in colors)
                    {
                        productIds.AddRange(from v in db.Feature_Values
                                            join f in db.Products_Features
                                            on v.FeatureValueId equals f.FeatureValueId
                                            where v.FeatureValue == item
                                            select f.ProductId);
                    }

                    products = products
                                .Where(p => productIds.Contains(p.ProductId))
                                .Where(p => p.Fee >= minPrice)
                                .Where(p => p.Fee <= maxPrice);
                }
                else
                {
                    products = products
                       .Where(p => p.Fee >= minPrice)
                       .Where(p => p.Fee <= maxPrice);
                }
            }
            else
            {
                products = products
                   .Where(p => p.Fee >= minPrice)
                   .Where(p => p.Fee <= maxPrice);
            }

            if (inStock == 1)
            {
                var stocks = db.Stock.Where(s => s.Stock1 > 0).Select(s => s.ProductId);

                products = products.Where(p => stocks.Contains(p.ProductId));
            }

            switch (sortOrder)
            {
                case "opt1": // sort by popularity
                    products = products.OrderByDescending(p => p.Title);
                    break;

                case "opt2": // sort by rating
                    products = products.OrderBy(p => p.Title);
                    break;

                case "opt3": // sort by newness
                    products = products.OrderByDescending(p => p.CreatedDate);
                    break;

                case "opt4": // sort by price : low to high
                    products = products.OrderBy(p => (p.Fee * p.DiscountPercent) == 0 ? p.Fee : p.Fee * (1 - p.DiscountPercent / 100));
                    break;

                case "opt5": // sort by price : high to low
                    products = products.OrderByDescending(p => (p.Fee * p.DiscountPercent) == 0 ? p.Fee : p.Fee * (1 - p.DiscountPercent / 100));
                    break;

                default: // sort by rating : default
                    products = products.OrderByDescending(p => p.CreatedDate);
                    break;
            }

            ViewBag.CurrentSort = sortOrder;

            ViewBag.PageSize = 20;
            int pageSize = ViewBag.PageSize;
            int pageNumber = (page ?? 1);

            if (inStock == 1) { ViewBag.Checked = true; } else { ViewBag.Checked = false; }
            ViewBag.NumberOfResults = products.Count();
            ViewBag.FeatureProducts = db.Products.Where(p => p.DiscountPercent >= 50).Take(4).ToList();

            if (System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "fa-IR")
            {
                return View("IndexRTL", products.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(products.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult FilterProducts(string search, string[] colors, int? minPrice, int? maxPrice, int? page, string[] brands, int? categoryId, int? subCategoryId, int? furtherCategoryId, int? inStock, string sortOrder = "opt3")
        {
            ViewBag.MinPrice = db.Products.Select(p => p.Fee).Min();
            ViewBag.MaxPrice = db.Products.Select(p => p.Fee).Max();

            if (minPrice == null) { minPrice = ViewBag.MinPrice; }
            if (maxPrice == null) { maxPrice = ViewBag.MaxPrice; }
            //if (minPrice == null) { minPrice = 0; }
            //if (maxPrice == null) { maxPrice = 20000000; }

            IQueryable<Products> products = db.Products;

            List<Products> filter = new List<Products>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                filter.AddRange(db.Product_Tags.Where(t => t.MetaTitle.ToLower() == search.ToLower()).Select(t => t.Products));
                filter.AddRange(db.Product_Tags.Where(t => t.MetaKeyword.ToLower().Contains(search.ToLower())).Select(t => t.Products));
                filter.AddRange(db.Product_Tags.Where(t => t.MetaDescription.ToLower().Contains(search.ToLower())).Select(t => t.Products));
                filter.AddRange(db.Products.Where(p => p.Title.ToLower().Contains(search.ToLower())));
                filter.AddRange(db.Products.Where(p => p.Text.ToLower().Contains(search.ToLower())));
                filter.AddRange(db.Products.Where(p => p.ShortDescription.ToLower().Contains(search.ToLower())));

                products = filter.AsQueryable();
                products = products.Distinct();
            }

            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            if (subCategoryId != null)
            {
                products = products.Where(p => p.SubCategoryId == subCategoryId);
            }

            if (furtherCategoryId != null)
            {
                products = products.Where(p => p.FurtherSubCategoryId == furtherCategoryId);
            }

            if (brands != null)
            {
                if (brands.Length != 0 && brands[0] != "")
                {
                    foreach (var item in brands)
                    {
                        filter.AddRange(products.Where(p => p.Product_Categories3.CategoryTitle.Contains(item)));
                    }
                    products = filter.AsQueryable();
                    products = products.Distinct();
                }
            }

            List<int> productIds = new List<int>();

            if (colors != null)
            {
                if (colors.Length != 0 && colors[0] != "")
                {
                    ViewBag.Colors = colors;

                    foreach (var item in colors)
                    {
                        productIds.AddRange(from v in db.Feature_Values
                                            join f in db.Products_Features
                                            on v.FeatureValueId equals f.FeatureValueId
                                            where v.FeatureValue == item
                                            select f.ProductId);
                    }

                    products = products
                                .Where(p => productIds.Contains(p.ProductId))
                                .Where(p => p.Fee >= minPrice)
                                .Where(p => p.Fee <= maxPrice);
                }
                else
                {
                    products = products
                       .Where(p => p.Fee >= minPrice)
                       .Where(p => p.Fee <= maxPrice);
                }
            }
            else
            {
                products = products
                   .Where(p => p.Fee >= minPrice)
                   .Where(p => p.Fee <= maxPrice);
            }

            if (inStock == 1)
            {
                //var stocks = db.Stock.Where(s => s.Stock1 > 0).Select(s => s.ProductId).ToList();

                //products = products.Where(p => stocks.Contains(p.ProductId));

                // Get Stock from Web api
                string response = Utilities.Helpers.ShariatiWebApi.GetAvailableArticles();

                products = products.Where(p => response.Contains(p.Article));
            }

            switch (sortOrder)
            {
                case "opt1": // sort by popularity
                    products = products.OrderByDescending(p => p.Title);
                    break;

                case "opt2": // sort by rating
                    products = products.OrderBy(p => p.Title);
                    break;

                case "opt3": // sort by newness
                    products = products.OrderByDescending(p => p.CreatedDate);
                    break;

                case "opt4": // sort by price : low to high
                    products = products.OrderBy(p => (p.Fee * p.DiscountPercent) == 0 ? p.Fee : p.Fee * (1 - p.DiscountPercent / 100));
                    break;

                case "opt5": // sort by price : high to low
                    products = products.OrderByDescending(p => (p.Fee * p.DiscountPercent) == 0 ? p.Fee : p.Fee * (1 - p.DiscountPercent / 100));
                    break;

                default: // sort by rating : default
                    products = products.OrderByDescending(p => p.CreatedDate);
                    break;
            }

            ViewBag.CurrentSort = sortOrder;

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            if (inStock == 1) { ViewBag.Checked = true; } else { ViewBag.Checked = false; }
            ViewBag.NumberOfResults = products.Count();
            ViewBag.FeatureProducts = db.Products.Where(p => p.DiscountPercent >= 50).Take(4).ToList();

            return PartialView("_PartialFilterProducts", products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ShowCategories()
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "fa-IR")
            {
                return PartialView("_PartialShowCategoriesRTL", db.Product_Categories.ToList());
            }
            else
            {
                return PartialView("_PartialShowCategories", db.Product_Categories.ToList());
            }
        }

        public ActionResult ShowCategoriesByColor()
        {
            var model = (from f in db.Features
                         join v in db.Feature_Values
                         on f.FeatureId equals v.FeatureId
                         where f.FeatureTitle.ToLower() == "color" || f.FeatureTitle.ToLower() == "رنگ"
                         select v.FeatureValue).Distinct().ToList();

            return PartialView("_PartialShowCategoriesByColor", model);
        }

        public ActionResult ShowCategoriesByBrand()
        {
            // Run stored procedure to get all brands as list<string>
            Dictionary<string, int> brands = new Dictionary<string, int>();

            if (System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "fa-IR")
            {
                foreach (var item in db.Brands())
                {
                    brands.Add(item.CategoryTitle, item.Quantity.Value);
                }
            }
            else
            {
                foreach (var item in db.Brands())
                {
                    brands.Add(item.CategoryTitle, item.Quantity.Value);
                }
            }

            return PartialView("_PartialShowCategoriesByBrand", brands);
        }

        //[Route("Brand/{brand}")]
        //public ActionResult ShowProductsByBrand(string[] brands)
        //{
        //    var model = db.Products.Where(p => p.Product_Categories3.CategoryTitle == brand);
        //    return View("ShowProductsByCategory", model);
        //}

        [Route("Color/{color}")]
        public ActionResult ShowProductsByColor(string color)
        {
            var productIds = from v in db.Feature_Values
                             join f in db.Products_Features
                             on v.FeatureValueId equals f.FeatureValueId
                             where v.FeatureValue == color
                             select f.ProductId;

            var model = db.Products.Where(p => productIds.Contains(p.ProductId)).ToList();

            return View("ShowProductsByCategory", model);
        }

        [Route("Category/{id}/{title}")]
        public ActionResult ShowProductsByCategory(int id, string title)
        {
            return View(db.Products.Where(p => p.CategoryId == id).ToList());
        }

        //[Route("SubCategory/{id}/{title}")]
        //public ActionResult ShowProductsBySubCategory(int id)
        //{
        //    return View(db.Products.Where(p => p.SubCategoryId == id).ToList());
        //}

        //[Route("FilterProducts/{id}/{color}/{brand}")]
        //public ActionResult FilterProducts(string sortOrder)
        //{
        //    //var productIds = from v in db.Feature_Values
        //    //            join f in db.Products_Features
        //    //            on v.FeatureValueId equals f.FeatureValueId
        //    //            where v.FeatureSimpleValue == color
        //    //            select f.ProductId;

        //    //var model1 = db.Products.Where(p => productIds.Contains(p.ProductId)).ToList();
        //    //var model2 = model1.Where(p => p.Product_Categories3.CategoryTitle == brand).ToList();
        //    //var model3 = model2.Where(p => p.CategoryId == id).ToList();

        //    ViewBag.orderByName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
        //    ViewBag.orderByPrice = sortOrder == "price_desc" ? "price_desc" : "price_asc";

        //    var products = db.Products.ToList();

        //    switch (sortOrder)
        //    {
        //        case "name_desc" :
        //            products = products.OrderByDescending(p => p.Title).ToList();
        //            break;

        //        case "name_asc" :
        //            products = products.OrderBy(p => p.Title).ToList();
        //            break;

        //        case "price_desc" :
        //            products = products.OrderByDescending(p => p.Fee).ToList();
        //            break;

        //        case "price_asc" :
        //            products = products.OrderBy(p => p.Fee).ToList();
        //            break;

        //        default:
        //            products = products.OrderByDescending(p => p.ProductId).ToList();
        //            break;
        //    }

        //    return View(products);
        //}

        [Route("Products/{id}")]
        public ActionResult ShowProductDetails(int id)
        {
            var userId = db.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.UserId).FirstOrDefault();

            Products product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Gallery = db.Product_Gallery.Where(p => p.ProductId == id).Select(p => p.ImageName);

            ViewBag.Rating = db.Product_Reviews.Where(p => p.ProductId == id).Average(p => p.Rating);

            List<Products> relatedProducts = new List<Products>();

            relatedProducts.AddRange(db.Products.Where(p => p.Title.Contains(product.Title)).Except(db.Products.Where(p => p.ProductId == id)));
            relatedProducts.AddRange(db.Products.Where(p => p.CategoryId == product.CategoryId && p.SubCategoryId == product.SubCategoryId &&
                p.FurtherSubCategoryId == product.FurtherSubCategoryId && p.FurthestSubCategoryId == product.FurthestSubCategoryId)
                .Except(db.Products.Where(p => p.ProductId == id)));

            relatedProducts = relatedProducts.Distinct().ToList();

            if (relatedProducts.Contains(product))
            {
                relatedProducts.Remove(product);
            }

            ViewBag.RelatedProducts = relatedProducts.Take(10).ToList();

            var brand = db.Products.Join(db.Product_Categories, p => p.FurthestSubCategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Where(pc => pc.p.ProductId == id).Select(pc => pc.c.CategoryTitle).Distinct().FirstOrDefault();

            var category = db.Products.Join(db.Product_Categories, p => p.SubCategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Where(pc => pc.p.ProductId == id).Select(pc => pc.c.CategoryTitle).Distinct().FirstOrDefault();

            int sizeGroup = db.SizeGroups
                .Where(s => s.Category == category).FirstOrDefault().SizeGroup;

            var sizes = db.Product_Sizes.Where(s => s.SizeGroup == sizeGroup).Select(s => new { SizeId = s.SizeId, Size = s.SizeIR });

            //Create disabled sizes

            List<SelectListItem> sizesList = new List<SelectListItem>();

            var isAvailable = false;

            //Get available sizes from database
            //foreach (var item in sizes)
            //{
            //    int? stock = db.GetStock(product.ProductId, item.SizeId).FirstOrDefault();

            //    if (stock == null || (int?)stock <= 0)
            //    {
            //        isAvailable = false;
            //    }
            //    else
            //    {
            //        isAvailable = true;
            //    }

            //    SelectListItem listItem = new SelectListItem();
            //    listItem.Value = item.SizeId.ToString();
            //    listItem.Text = item.Size.ToString();
            //    listItem.Disabled = !isAvailable;
            //    sizesList.Add(listItem);
            //}

            //Get available sizes from web api
            foreach (var item in sizes)
            {
                string article = db.Products.Where(p => p.ProductId == product.ProductId).Select(p => p.Article).FirstOrDefault();
                string size = db.Product_Sizes.Where(p => p.SizeId == item.SizeId).Select(p => p.SizeUK).FirstOrDefault();

                string response = Utilities.Helpers.ShariatiWebApi.GetStock(article, size);
                string[] data = response.Split(',');

                Dictionary<string, string> values = new Dictionary<string, string>();

                if (data != null && data.Length != 0 && data[0] != "[]")
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        int first = data[i].IndexOf('"');
                        int last = data[i].IndexOf(':');
                        string str1 = data[i].Substring(first, last).Replace('"', ' ').Replace('\\', ' ').Replace(':', ' ').Replace('{', ' ').Replace('}', ' ').Trim();
                        string str2 = data[i].Substring(last).Replace('"', ' ').Replace('\\', ' ').Replace(':', ' ').Replace('{', ' ').Replace('}', ' ').Trim();
                        values.Add(str1, str2);
                    }

                    decimal d = decimal.Parse(values["Stock"], CultureInfo.InvariantCulture);

                    int? stock = (int?)d;

                    if (stock == null || (int?)stock <= 0)
                    {
                        isAvailable = false;
                    }
                    else
                    {
                        isAvailable = true;
                    }

                    SelectListItem listItem = new SelectListItem();
                    listItem.Value = item.SizeId.ToString();
                    listItem.Text = item.Size.ToString();
                    listItem.Disabled = !isAvailable;
                    sizesList.Add(listItem);
                }
            }

            ViewBag.SizeId = sizesList;

            /////////////////////////////////////////////////

            ProductDetailsViewModel productDetail = new ProductDetailsViewModel()
            {
                ProductId = product.ProductId,
                SizeId = sizes.Select(s => s.SizeId),
                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId,
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                Text = product.Text,
                ImageName = product.ImageName,
                Fee = product.Fee,
                DiscountPercent = product.DiscountPercent,
                CreatedDate = product.CreatedDate,
                FurtherSubCategoryId = product.FurtherSubCategoryId,
                FurthestSubCategoryId = product.FurthestSubCategoryId,
                Status = product.Status,
                Comment = product.Comment,
                IsFavourite = db.User_Favourites.Where(u => u.UserId == userId).Select(u => u.ProductId).Contains(id)
            };

            string name = product.Product_Tags.Where(p => p.ProductId == id).Select(m => m.MetaTitle).FirstOrDefault();
            ViewBag.MetaTitle = name + " " + brand;
            ViewBag.MetaDescription = product.Product_Tags.Where(p => p.ProductId == id).Select(m => m.MetaDescription).FirstOrDefault();

            return View(productDetail);
        }

        public ActionResult ShowReviews(int id)
        {
            return PartialView("_PartialShowReviews", db.Product_Reviews.Where(p => p.ProductId == id).ToList());
        }

        [Authorize]
        public ActionResult AddReview(int id, int? parentId)
        {
            return PartialView("_PartialAddReview",
                new Product_Reviews() { ProductId = id, ParentId = parentId });
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddReview(Product_Reviews review)
        {
            review.CreatedDate = DateTime.Now;
            db.Product_Reviews.Add(review);
            db.SaveChanges();
            var model = db.Product_Reviews.Where(p => p.ProductId == review.ProductId).ToList();
            return PartialView("_PartialShowReviews", model);
        }

        [Route("GetQuantityOfSize")]
        public ActionResult GetQuantityOfSize(int productId, int sizeId)
        {
            // Get stock from its own database
            //int? stock = db.GetStock(productId, sizeId).FirstOrDefault();

            string article = db.Products.Where(p => p.ProductId == productId).Select(p => p.Article).FirstOrDefault();

            string size = db.Product_Sizes.Where(p => p.SizeId == sizeId).Select(p => p.SizeUK).FirstOrDefault();

            //Get stock from web api
            string response = Utilities.Helpers.ShariatiWebApi.GetStock(article, size);
            string[] data = response.Split(',');

            Dictionary<string, string> values = new Dictionary<string, string>();

            for (int i = 0; i < data.Length; i++)
            {
                int first = data[i].IndexOf('"');
                int last = data[i].IndexOf(':');
                string str1 = data[i].Substring(first, last).Replace('"', ' ').Replace('\\', ' ').Replace(':', ' ').Replace('{', ' ').Replace('}', ' ').Trim();
                string str2 = data[i].Substring(last).Replace('"', ' ').Replace('\\', ' ').Replace(':', ' ').Replace('{', ' ').Replace('}', ' ').Trim();
                values.Add(str1, str2);
            }

            decimal d = decimal.Parse(values["Stock"], CultureInfo.InvariantCulture);

            int? stock = (int?)d;

            var maxQuantity = 0;

            if (stock == null || stock <= 0)
            {
                maxQuantity = 0;
            }
            else
            {
                maxQuantity = (int)stock;
            }

            return Json(maxQuantity, JsonRequestBehavior.AllowGet);
        }

        [Route("ShowSize")]
        public JsonResult ShowSize(int productId)
        {
            //Get stock of sizes from database
            //var stockSizes = db.GetSizesStock(productId);
            //Dictionary<string, string> sizeChart = new Dictionary<string, string>();

            //foreach (var item in stockSizes)
            //{
            //    sizeChart.Add(item.SizeIR.ToString(), item.Stock.ToString());
            //}

            //return Json(sizeChart, JsonRequestBehavior.AllowGet);

            //Get stock of sizes from web api
            string article = db.Products.Where(p => p.ProductId == productId).Select(p => p.Article).FirstOrDefault();
            string response = Utilities.Helpers.ShariatiWebApi.GetSizesStock(article);

            Dictionary<string, string> sizeChart = new Dictionary<string, string>();

            var obj = JsonConvert.DeserializeObject(response).ToString();
            var x = JArray.Parse(obj);

            foreach (JObject item in x.Children<JObject>())
            {
                int charIndex = (item.First).ToString().IndexOf(':');
                string sizeUK = (item.First).ToString().Substring(charIndex).Replace('"', ' ').Replace(':', ' ').Trim();
                string stock = (item.First.Next).ToString().Substring(charIndex).Replace('"', ' ').Replace(':', ' ').Trim();

                string sizeIR = db.Product_Sizes.Where(p => p.SizeUK == sizeUK).Select(p => p.SizeIR).FirstOrDefault();

                if (sizeIR != null)
                {
                    sizeChart.Add(sizeIR, stock);
                }
                //foreach (JProperty item2 in item.Properties())
                //{
                //    string s1 = item2.Name;
                //    string s2 = (string)item2.Value;
                //    string s = s1 + "--- " + s2;
                //}
            }

            return Json(sizeChart, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Menu()
        {
            var categories = db.Product_Categories.ToList();

            ViewBag.Brands = db.Brands().ToList();

            return PartialView("_PartialMenu", categories);
        }

        //[Authorize]
        [Route("ShowWishListIcon")]
        public ActionResult ShowWishListIcon(int id)
        {
            WishListViewModel model = new WishListViewModel();

            var userId = db.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.UserId).FirstOrDefault();

            var isFavourite = db.User_Favourites.Where(p => p.ProductId == id).Where(p => p.UserId == userId).FirstOrDefault();

            model.ProductId = id;
            model.UserId = userId;
            model.ImageName = db.Products.Where(p => p.ProductId == id).FirstOrDefault().ImageName;
            model.Title = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Title;
            model.Price = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Fee;
            model.Stock = db.Stock.Where(p => p.ProductId == id).Select(p => p.Stock1).FirstOrDefault();

            if (isFavourite == null)
            {
                ViewBag.IsFavourite = false;
            }
            else
            {
                ViewBag.IsFavourite = true;
            }

            return PartialView("_PartialAddToWishList", model);
        }

        [AjaxAuthorizeAttribute]
        [Route("AddToWishList")]
        public ActionResult AddToWishList(int id)
        {
            WishListViewModel model = new WishListViewModel();

            var userId = db.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.UserId).FirstOrDefault();

            var isFavourite = db.User_Favourites.Where(p => p.ProductId == id).Where(p => p.UserId == userId).FirstOrDefault();

            model.ProductId = id;
            model.UserId = userId;
            model.ImageName = db.Products.Where(p => p.ProductId == id).FirstOrDefault().ImageName;
            model.Title = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Title;
            var fee = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Fee;
            var discount = db.Products.Where(p => p.ProductId == id).FirstOrDefault().DiscountPercent;
            if (discount != 0)
            {
                model.Price = fee * discount;
            }
            else
            {
                model.Price = fee;
            }
            model.Stock = db.Stock.Where(p => p.ProductId == id).Select(p => p.Stock1).FirstOrDefault();

            if (isFavourite == null)
            {
                db.User_Favourites.Add(new User_Favourites()
                {
                    ProductId = id,
                    UserId = userId
                });
                db.SaveChanges();
                ViewBag.IsFavourite = true;
            }
            else
            {
                db.User_Favourites.Remove(isFavourite);
                db.SaveChanges();
                ViewBag.IsFavourite = false;
            }

            return PartialView("_PartialAddToWishList", model);
        }

        [Authorize]
        [Route("WishList")]
        public ActionResult WishList()
        {
            List<WishListViewModel> model = new List<WishListViewModel>();

            var userId = db.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.UserId).FirstOrDefault();

            var favourites = db.User_Favourites.Where(u => u.UserId == userId);

            if (favourites != null)
            {
                foreach (var item in favourites)
                {
                    var fee = db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault().Fee;
                    var discount = db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault().DiscountPercent;
                    model.Add(new WishListViewModel()
                    {
                        ProductId = item.ProductId,
                        UserId = userId,
                        ImageName = db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault().ImageName,
                        Title = db.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault().Title,
                        Price = discount != 0 ? fee * discount : fee,
                        Stock = db.Stock.Where(p => p.ProductId == item.ProductId).Select(p => p.Stock1).FirstOrDefault()
                    });
                }
            }

            return View(model);
        }

        //[Route("AddToCart")]
        //public ActionResult AddToCart(int id,int sizeId,byte quantity)
        //{
        //    List<ShoppingCartViewModel> list = new List<ShoppingCartViewModel>();

        //    if (Session["ShoppingCart"] != null)
        //    {
        //        list = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
        //    }

        //    int? stock = db.GetStock(id, sizeId).FirstOrDefault();

        //    if (list.Any(p => p.ProductId == id && p.SizeId == sizeId))
        //    {
        //        int index = list.FindIndex(p => p.ProductId == id && p.SizeId == sizeId);

        //        if (stock != null && stock > list[index].Quantity)
        //        {
        //            list[index].Quantity += 1;
        //            list[index].Price = (quantity * db.Products.Where(p => p.ProductId == id).FirstOrDefault().Fee);
        //        }
        //    }
        //    else
        //    {
        //        if (stock != null && stock > 0)
        //        {
        //            list.Add(new ShoppingCartViewModel()
        //            {
        //                ProductId = id,
        //                ProductName = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Title,
        //                SizeId = sizeId,
        //                Quantity = quantity,
        //                Price = (quantity * db.Products.Where(p => p.ProductId == id).FirstOrDefault().Fee),
        //                ImageName = db.Products.Where(p => p.ProductId == id).FirstOrDefault().ImageName
        //            });
        //        }
        //    }

        //    Session["ShoppingCart"] = list;

        //    return PartialView("_PartialCartList", list);
        //}

        //[Route("ShowCart")]
        //public ActionResult ShowCart()
        //{
        //    List<ShoppingCartViewModel> model = new List<ShoppingCartViewModel>();

        //    if (Session["ShoppingCart"] != null)
        //    {
        //        model = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
        //    }

        //    return PartialView("_PartialCartList", model);
        //}

        //[Route("test")]
        //public ActionResult test(string t)
        //{
        //    var text = "salam " + t;
        //    return Json(text,JsonRequestBehavior.AllowGet);
        //}
    }
}