using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWebsite.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult LatestNews()
        {
            return PartialView("_PartialLatestNews",db.News.Where(n => n.IsActive)
                .OrderByDescending(n => n.CreatedDate)
                .Take(2)
                .ToList());
        }
    }
}