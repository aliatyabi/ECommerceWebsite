using ECommerceWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities.Helpers;
using ViewModels.ShoppingCart;

namespace ECommerceWebsite.Controllers
{
    public class ShoppingCartController : BaseController
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        //Hosted web API REST Service base url  
        string Baseurl = "http://api.aliatyabi.ir/api/Stock/4";
        public async Task<ActionResult> GetStock()
        {
            Chain_Stock chain_Stock = new Chain_Stock();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("4");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var stockResponse = Res.Content.ReadAsStringAsync().Result;
                    //var stockResponse = await Res.Content.ReadAsAsync<Chain_Stock>();
                    
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    chain_Stock = JsonConvert.DeserializeObject<Chain_Stock>(stockResponse);
                    TempData.Add("chainStock", chain_Stock);
                    TempData.Add("stock", chain_Stock.Stock);
                }
                //returning the employee list to view
            }

            return Content(TempData["chainStock"].ToString());
        }

        public ActionResult ShoppingCart()
        {
            List<ShoppingCartViewModel> model = new List<ShoppingCartViewModel>();

            if (Session["ShoppingCart"] != null)
            {
                model = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
            }

            return PartialView("_PartialShoppingCart", model);
        }

        [Route("AddToCart")]
        public ActionResult AddToCart(int id, int sizeId, byte quantity)
        {
            List<ShoppingCartViewModel> list = new List<ShoppingCartViewModel>();

            if (Session["ShoppingCart"] != null)
            {
                list = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
            }

            // Get stock from its own database
            //int? stock = db.GetStock(id, sizeId).FirstOrDefault();

            string size = db.Product_Sizes.Where(p => p.SizeId == sizeId).FirstOrDefault().SizeUK;
            string article = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Article;

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

            decimal d = decimal.Parse(values["Stock"],CultureInfo.InvariantCulture);

            int? stock = (int?)d;

            var fee = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Fee;
            var discount = db.Products.Where(p => p.ProductId == id).FirstOrDefault().DiscountPercent;

            if (list.Any(p => p.ProductId == id && p.SizeId == sizeId))
            {
                int index = list.FindIndex(p => p.ProductId == id && p.SizeId == sizeId);

                if (stock != null && stock > list[index].Quantity)
                {
                    list[index].Quantity += 1;
                    list[index].Price = (int)(list[index].Quantity * fee * (double)(100 - discount) / 100);
                }
            }
            else
            {
                if (stock != null && stock > 0)
                {
                    list.Add(new ShoppingCartViewModel()
                    {
                        ProductId = id,
                        ProductName = db.Products.Where(p => p.ProductId == id).FirstOrDefault().Title,
                        SizeId = sizeId,
                        Size = db.Product_Sizes.Where(p => p.SizeId == sizeId).FirstOrDefault().SizeIR,
                        Quantity = quantity,
                        Fee = fee,
                        DiscountPercent = discount,
                        DiscountFee = (int)(fee * (double)(100 - discount) / 100),
                        OldPrice = quantity * fee,
                        Price = (int)(quantity * fee * (double)(100 - discount) / 100),
                        ImageName = db.Products.Where(p => p.ProductId == id).FirstOrDefault().ImageName
                    });
                }
            }

            Session["ShoppingCart"] = list;

            return PartialView("_PartialCartList", list);
        }

        [Route("ShowCart")]
        public ActionResult ShowCart()
        {
            List<ShoppingCartViewModel> model = new List<ShoppingCartViewModel>();

            if (Session["ShoppingCart"] != null)
            {
                model = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
            }

            return PartialView("_PartialCartList", model);
        }

        [Route("RemoveCartItem")]
        public ActionResult RemoveCartItem(int productId, int sizeId)
        {
            List<ShoppingCartViewModel> model = new List<ShoppingCartViewModel>();

            if (Session["ShoppingCart"] != null)
            {
                model = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
            }

            var itemToRemove = model.Where(p => p.ProductId == productId && p.SizeId == sizeId).FirstOrDefault();

            model.Remove(itemToRemove);

            return PartialView("_PartialShoppingCart", model);
        }

        [Route("ClearCart")]
        public ActionResult ClearCart()
        {
            List<ShoppingCartViewModel> model = new List<ShoppingCartViewModel>();

            if (Session["ShoppingCart"] != null)
            {
                model = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
            }

            model.Clear();

            return RedirectToAction("Index");
        }

        [Route("ChangeQuantity")]
        public ActionResult ChangeQuantity(int productId, int sizeId, byte quantity)
        {
            List<ShoppingCartViewModel> model = new List<ShoppingCartViewModel>();

            if (Session["ShoppingCart"] != null)
            {
                model = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
            }

            var itemToChange = model.Where(p => p.ProductId == productId && p.SizeId == sizeId).FirstOrDefault();

            //Get stock from database
            //int? stock = db.GetStock(productId, sizeId).FirstOrDefault();

            //Get stock from web api
            string size = db.Product_Sizes.Where(s => s.SizeId == sizeId).Select(s => s.SizeUK).FirstOrDefault();
            string article = db.Products.Where(p => p.ProductId == productId).Select(p => p.Article).FirstOrDefault();

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

            decimal d = decimal.Parse(values["Stock"],CultureInfo.InvariantCulture);

            int? stock = (int?)d;

            if (model.Any(p => p.ProductId == productId && p.SizeId == sizeId))
            {
                int index = model.FindIndex(p => p.ProductId == productId && p.SizeId == sizeId);

                if (stock != null && stock >= model[index].Quantity)
                {
                    itemToChange.Quantity = quantity;
                    itemToChange.Price = quantity * itemToChange.DiscountFee;
                }
                else
                {
                    itemToChange.Quantity = (byte)stock;
                    ViewBag.NotAvailable = true;
                    var msg = @Resources.Resource.MoreThanStock;
                    ViewBag.Message = "<script>alert('" + msg + "')</script>";
                }
            }

            return PartialView("_PartialShoppingCart", model);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var user = db.Users.Where(u => u.Username.ToLower().Trim() == User.Identity.Name.ToLower().Trim()).FirstOrDefault();

            var userProfile = db.UsersInformation.Where(u => u.UserId == user.UserId).FirstOrDefault();

            if (userProfile == null)
            {
                return RedirectToAction("UserInformation", "ShoppingCart");
            }
            else
            {
                CheckoutViewModel model = new CheckoutViewModel();

                List<ShoppingCartViewModel> cart = new List<ShoppingCartViewModel>();

                if (Session["ShoppingCart"] != null)
                {
                    cart = Session["ShoppingCart"] as List<ShoppingCartViewModel>;
                    model.ShoppingCart = cart;
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            List<ShoppingCartViewModel> cart = Session["ShoppingCart"] as List<ShoppingCartViewModel>;

            Invoices order = new Invoices();

            order.Quantity = cart.Sum(c => c.Quantity);
            order.Discount = cart.Sum(c => c.Quantity * c.Fee) - cart.Sum(c => c.Quantity * c.DiscountFee);
            order.InvoiceNetAmount = cart.Sum(c => c.Price);
            order.InvoiceAmount = cart.Sum(c => c.OldPrice);
            order.InvoiceDate = DateTime.Now;
            order.TransactionType = 2;
            order.UserId = db.Users.Where(u => u.Username.ToLower().Trim() == User.Identity.Name.ToLower().Trim()).FirstOrDefault().UserId;
            order.Completed = false;

            db.Invoices.Add(order);

            foreach (var item in cart)
            {
                db.InvoiceItems.Add(new InvoiceItems()
                {
                    Quantity = item.Quantity,
                    Fee = item.Fee,
                    DiscountFee = item.DiscountFee,
                    Price = item.Quantity * item.DiscountFee,
                    SizeId = item.SizeId,
                    ProductId = item.ProductId,
                    InvoiceId = order.InvoiceId
                });
            }

            db.SaveChanges();

            if (model.PaymentMethod == 0)
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                Zarrinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarrinpal.PaymentGatewayImplementationServicePortTypeClient();
                string Authority;

                int Status = zp.PaymentRequest("9e957568-5cde-11e8-ac9f-005056a205be", order.InvoiceNetAmount / 10, "test", "you@yoursite.com", "09123456789", System.Configuration.ConfigurationManager.AppSettings["DomainAddress"] + "/ShoppingCart/Verify/" + order.InvoiceId, out Authority);
                //int Status = zp.PaymentRequest("9e957568-5cde-11e8-ac9f-005056a205be", order.InvoiceNetAmount / 10, "test", "you@yoursite.com", "09123456789", "http://localhost:3782/ShoppingCart/Verify/" + order.InvoiceId, out Authority);

                if (Status == 100)
                {
                    //Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
                    return Redirect("https://www.zarinpal.com/pg/StartPay/" + long.Parse(Authority));
                    //return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
                }
                else
                {
                    ViewBag.Error = "error: " + Status;
                }
            }
            else
            {
                return RedirectToAction("VerifyWithInPlacePayment", new { id = order.InvoiceId });
            }

            return View();
        }

        public ActionResult Verify(int id)
        {
            var order = db.Invoices.Find(id);

            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    int Amount = order.InvoiceNetAmount;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    Zarrinpal.PaymentGatewayImplementationServicePortTypeClient zp = new Zarrinpal.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification("9e957568-5cde-11e8-ac9f-005056a205be", Request.QueryString["Authority"].ToString(), Amount / 10, out RefID);

                    if (Status == 100)
                    {
                        order.Completed = true;
                        db.SaveChanges();
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                        order.TrackingCode = RefID;
                        order.Status = Request.QueryString["Status"].ToString();
                        order.Authority = long.Parse(Request.QueryString["Authority"].ToString());
                        db.SaveChanges();

                        Utilities.Helpers.ShariatiWebApi.CreateSaleFactor(Amount);

                        foreach (var item in order.InvoiceItems)
	                    {
                            int sizeId = item.SizeId;
                            string size = item.Product_Sizes.SizeUK;
                            string article = item.Products.Article;
                            int qty = item.Quantity;
                            int mfee = item.Fee;
                            int fee = item.DiscountFee;
                            int price = (int)item.Price;

                            Utilities.Helpers.ShariatiWebApi.CreateSaleFactorDetails(article, size, qty, mfee, fee, Amount);

                            Session["ShoppingCart"] = null;
	                    }

                    }
                    else
                    {
                        ViewBag.Authority = long.Parse(Request.QueryString["Authority"].ToString());
                        ViewBag.Status = Status;
                        order.Authority = ViewBag.Authority;
                        order.Status = Status.ToString();
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.Authority = long.Parse(Request.QueryString["Authority"].ToString());
                    order.Authority = ViewBag.Authority;
                    order.Status = Request.QueryString["Status"].ToString();
                    db.SaveChanges();
                    //Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                }
            }
            else
            {
                ViewBag.InvalidInput = @Resources.Resource.InvalidInput;
                //Response.Write("Invalid Input");
            }

            return View();
        }

        public ActionResult VerifyWithInPlacePayment(int id)
        {
            var order = db.Invoices.Find(id);
            order.Completed = true;
            db.SaveChanges();
            ViewBag.IsSuccess = true;
            var rand = Helpers.RandomLong();
            if (rand < 0) { rand = rand * -1; }
            var RefID = rand;
            ViewBag.RefId = RefID;
            order.TrackingCode = RefID;
            db.SaveChanges();

            Session["ShoppingCart"] = null;

            return View();
        }

        public ActionResult UserInformation()
        {
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInformation([Bind(Include = "FirstName,LastName,NationalCode,Tel,Mobile,Birthday,Gender,StateId,CityId,Address,NewsletterMembership")] UsersInformation profile)
        {
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            if (profile.StateId == 0)
            {
                ModelState.AddModelError("StateId", Resources.Resource.StateRequired);
            }

            else if(profile.CityId == 0)
            {
                ModelState.AddModelError("CityId", Resources.Resource.CityRequired);
            }
            else if (ModelState.IsValid)
            {
                if (Helpers.IsValidNationalCode(profile.NationalCode))
                {
                    var user = db.Users.Where(u => u.Username.ToLower().Trim() == User.Identity.Name).FirstOrDefault();
                    profile.UserId = user.UserId;
                    db.UsersInformation.Add(profile);
                    db.SaveChanges();
                    return RedirectToAction("Checkout");
                }
                else
                {
                    ModelState.AddModelError("NationalCode",Resources.Resource.NationalCodeNotValid);
                }
            }

            return View(profile);
        }

        public JsonResult ShowCities(int id)
        {
            var cities = db.Cities.Where(c => c.StateId == id).Select(c => new { c.CityId, c.CityName, c.CityEnName });
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Menu()
        {
            var categories = db.Product_Categories.ToList();

            ViewBag.Brands = db.Brands().ToList();

            return PartialView("_PartialMenu", categories);
        }
    }
}