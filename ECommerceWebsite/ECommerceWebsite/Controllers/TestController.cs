using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWebsite.Controllers
{
    public class TestController : Controller
    {
        [Authorize(Roles = "admin")]
        // GET: Test
        public ActionResult Test1()
        {
            return View();
        }

        public ActionResult Test2()
        {
            return View();
        }
    }
}