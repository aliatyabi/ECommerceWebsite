using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceWebsite.Controllers
{
    public class EmailSenderController : Controller
    {
        // GET: EmailSender
        public ActionResult ActivationEmailSend()
        {
            return PartialView();
        }

        public ActionResult ResetPasswordEmailSend()
        {
            return PartialView("_PartialResetPasswordEmailSend");
        }
    }
}