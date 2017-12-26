using InventoryManagement.Library.Enums;
using InventoryManagement.Library.Resources;
using InventoryManagement.Library.Services;
using InventoryManagement.Web.Helpers;
using InventoryManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace InventoryManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Members

        IUserService _userService;

        #endregion

        #region Constructor
        public AccountController(IUserService userservice)
        {
            _userService = userservice;
        }

        #endregion

        #region Action Methods

        public ActionResult Login(string returnUrl)
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.ValidateUserLogin(model.Email, model.Password);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(model);
                    }
                    var expirationTime = Convert.ToDouble(ConfigurationManager.AppSettings["SessionExpirationMinutes"]);
                    var UserData = user.UserId + "|" + (user.FirstName ?? string.Empty) + " " + (user.LastName ?? string.Empty) + "|" + user.RoleId + "|" + user.Role.Name;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, (user.FirstName ?? string.Empty) + " " + (user.LastName ?? string.Empty), DateTime.Now, DateTime.Now.AddMinutes(expirationTime), model.RememberMe, UserData, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    return Redirect(model.ReturnUrl, user.Role.Name);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
            }
            return RedirectToAction("Error", "Error");
        }

        private ActionResult Redirect(string returnUrl, string role)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();


                // clear authentication cookie
                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                cookie1.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie1);

                // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
                SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
                HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
                cookie2.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie2);

                Response.Cache.SetNoStore();
                Response.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, Message.GeneralError);
            }
            return RedirectToAction("Error", "Error");

        }

        #endregion
    }
}