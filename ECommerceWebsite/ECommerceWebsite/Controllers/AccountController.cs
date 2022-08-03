using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECommerceWebsite.Models;
using ViewModels;
using Utilities;
using ViewModels.Account;

namespace ECommerceWebsite.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaMvc.Attributes.CaptchaVerify(resourceName : "Captcha",resourceType: typeof(Resources.Resource))]
        [Route("Register")]
        public ActionResult Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.Email == vm.Email.ToLower().Trim()))
                {
                    Users user = new Users()
                    {
                        Email = vm.Email.ToLower().Trim(),
                        ActivationCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(vm.Password, "MD5"),
                        RoleId = 3,
                        RegisterationDate = DateTime.Now
                    };

                    db.Users.Add(user);
                    db.SaveChanges();
                    ViewBag.IsSuccess = true;

                    string emailBody = PartialToStringClass.RenderPartialView("EmailSender", "_PartialActivationEmailSend", user);

                    SendEmailGmail.Send(user.Email, Resources.Resource.ActivationEmailSubject, emailBody);
                }
                else
                {
                    ViewBag.IsExistEmail = true;
                }
            }

            return View(vm);
        }

        public ActionResult ActivateUser(string id)
        {
            var user = db.Users.FirstOrDefault(u => u.ActivationCode == id);

            if(user != null)
            {
                user.ActivationCode = Guid.NewGuid().ToString();
                user.IsActive = true;
                db.SaveChanges();
                ViewBag.IsSuccess = true;
            }
            return View();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel vm, string ReturnUrl = "/")
        {
            var userPass = FormsAuthentication.HashPasswordForStoringInConfigFile(vm.Password, "MD5");

            var user = db.Users.FirstOrDefault(u => u.Email == vm.Email.ToLower().Trim() && u.Password == userPass);
            if(user != null)
            {
                if(user.IsActive == true)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, vm.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("Email", Resources.Resource.InActiveAccount);
                }
            }
            else
            {
                ModelState.AddModelError("Email", Resources.Resource.AccountNotFound);
            }
            return View(vm);
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Route("ResetPassword")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [Route("ResetPassword")]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Email.Trim().ToLower() == vm.Email);
                if(user != null)
                {
                    string emailBody = PartialToStringClass.RenderPartialView("EmailSender", "_PartialResetPasswordEmailSend", user);

                    SendEmailGmail.Send(user.Email, Resources.Resource.ResetPasswordLabel, emailBody);
   
                    ViewBag.IsSuccess = true;
                }
                else
                {
                    ModelState.AddModelError("Email", Resources.Resource.UserNotFountMessage);
                }
            }

            return View(vm);
        }

        public ActionResult ChangePassword(string id)
        {
            return View(new ForgotPasswordViewModel() 
            { ActivationCode = id });
        }

        [HttpPost]
        public ActionResult ChangePassword(ForgotPasswordViewModel vm)
        {
            var user = db.Users.FirstOrDefault(u => u.ActivationCode == vm.ActivationCode);

            if(user != null)
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(vm.Password, "MD5");

                user.ActivationCode = Guid.NewGuid().ToString();

                db.SaveChanges();

                return Redirect("/Login");
            }
            else
            {
                ModelState.AddModelError("Password", Resources.Resource.ParametersIsNotValid);
            }

            return View();
        }
    }
}