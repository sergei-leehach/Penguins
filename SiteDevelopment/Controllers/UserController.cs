﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteDevelopment.Models;
using SiteDevelopment.Repository;
using System.Web.Security;
using SiteDevelopment.Providers;

namespace SiteDevelopment.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _db = new UserRepository();

        // GET: User
        [Authorize]
        public ActionResult Index(int userId)
        {
            var user = _db.GetUser(userId);
            return View(user);
        }

        [Authorize]
        public ActionResult Edit(int userId)
        {
            var user = _db.GetUser(userId);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            _db.EditUser(user);
            return RedirectToAction("Index", user.UserId);
        }

        [AllowAnonymous]
        public ActionResult LogOn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "News");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The email or password provided is incorrect.");
                }
            }
            return View(user);
            //return RedirectToAction("LogOn");
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "User");
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider) Membership.Provider).CreateUser(user.Nickname, user.Email,
                    user.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Edit", user.UserId);
                }
                else
                {
                    ModelState.AddModelError("", "Error during the registration");
                }
            }
            return View(user);
        }

        //<div>
        //        @Html.LabelFor(x => x.Avatar)
        //        <div><input id = "image" type="text" name="Avatar" onkeyup="RenderImage(this)" /></div>
        //        <div><img id = "render-image" src="" /></div>
        //    </div>
    }
}