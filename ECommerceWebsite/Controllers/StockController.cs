using ECommerceWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ECommerceWebsite.Controllers
{

    //  Call web api using async method
    public class StockController : Controller
    {
        // GET: Stock
        string Baseurl = "http://localhost:32283";
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
                HttpResponseMessage Res = await client.GetAsync("api/Stock/4");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var stockResponse = Res.Content.ReadAsStringAsync().Result;
                    //var stockResponse = await Res.Content.ReadAsAsync<Chain_Stock>();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    chain_Stock = JsonConvert.DeserializeObject<Chain_Stock>(stockResponse);
                    //TempData.Add("chainStock", chain_Stock);
                    TempData.Add("stock", chain_Stock.Stock);
                }
                //returning the employee list to view  
            }

            //return Content(TempData["chainStock"].ToString());
            return Json(chain_Stock, JsonRequestBehavior.AllowGet);
        }
    }
}