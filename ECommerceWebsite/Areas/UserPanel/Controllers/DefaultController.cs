using ECommerceWebsite.Controllers;
using ECommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utilities;
using ViewModels.Account;

namespace ECommerceWebsite.Areas.UserPanel.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: UserPanel/Default
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.First(u => u.Email == User.Identity.Name);

                //var oldPass = FormsAuthentication.HashPasswordForStoringInConfigFile(vm.OldPassword,"MD5");
                var oldPass = PasswordHelper.EncodePasswordMd5(vm.Password);

                if (user.Password == oldPass)
                {
                    //user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(vm.Password, "MD5");
                    user.Password = PasswordHelper.EncodePasswordMd5(vm.Password);
                    db.SaveChanges();
                    ViewBag.IsSuccess = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", Resources.Resource.PasswordIsNotCorrect);
                }
            }

            return View(vm);
        }

        //[Route("Settings")]
        //public ActionResult Settings()
        //{
        //    var states = db.States.ToList();

        //    var model = new ChangeProfileViewModel();

        //    var selectList = new List<SelectListItem>();

        //    foreach (var item in states)
        //    {
        //        selectList.Add(new SelectListItem()
        //        {
        //            Value = item.StateId.ToString(),
        //            Text = item.StateName
        //        });
        //    }

        //    model.State.States = selectList;

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Settings(ChangeProfileViewModel vm)
        //{
        //    var states = db.States.ToList();

        //    var selectList = new List<SelectListItem>();

        //    foreach (var item in states)
        //    {
        //        selectList.Add(new SelectListItem()
        //        {
        //            Value = item.StateId.ToString(),
        //            Text = item.StateName
        //        });
        //    }

        //    vm.State.States = selectList;

        //    vm.Email = User.Identity.Name;

        //    var profile = db.UsersProfile.FirstOrDefault(u => u.Email == vm.Email);

        //    if(ModelState.IsValid)
        //    {
        //        if (profile != null)
        //        {
        //            profile.Address = vm.Address.ToLower().Trim();
        //            profile.Avatar = vm.Avatar;
        //            profile.Birthday = vm.Birthday;
        //            profile.City = vm.City;
        //            profile.FirstName = vm.FirstName.ToLower().Trim();
        //            if (vm.Gender == true)
        //            {
        //                profile.Gender = true;
        //            }
        //            else
        //            {
        //                profile.Gender = false;
        //            }
        //            profile.LastName = vm.LastName.ToLower().Trim();
        //            profile.Mobile = vm.Mobile;
        //            profile.NationalCode = vm.NationalCode;
        //            profile.NewsletterMembership = vm.NewsletterMembership;
        //            profile.State = vm.State.StateName;
        //            profile.Tel = vm.Tel;
        //            db.SaveChanges();
        //        }
        //        else
        //        {
        //            db.UsersProfile.Add(new Models.UsersProfile()
        //            {
        //                Address = vm.Address.ToLower().Trim(),
        //                Avatar = vm.Avatar,
        //                Birthday = vm.Birthday,
        //                City = vm.City,
        //                Email = vm.Email,
        //                FirstName = vm.FirstName.ToLower().Trim(),
        //                Gender = vm.Gender,
        //                LastName = vm.LastName.ToLower().Trim(),
        //                Mobile = vm.Mobile,
        //                NationalCode = vm.NationalCode,
        //                NewsletterMembership = vm.NewsletterMembership,
        //                State = vm.State.StateName,
        //                Tel = vm.Tel,
        //                UserId = vm.UserId
        //            });
        //            db.SaveChanges();
        //        }
        //    }

        //    return View(vm);
        //}

        public ActionResult State()
        {
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");

            return View();
        }

        //public ActionResult State(StatesViewModel model)
        //{
        //    if(model.States == null)
        //    {
        //        model.States = new SelectList(db.States.Select(m => m.StateName),"StateId","StateName");
        //    }

        //    return View(model);
        //}

        public ActionResult City(int? id)
        {
            var cities = db.Cities.Where(c => c.StateId == id);
            if (cities != null)
            {
                ViewBag.CityId = new SelectList(cities, "CityId", "CityName");
            }

            return View();
        }
    }
}