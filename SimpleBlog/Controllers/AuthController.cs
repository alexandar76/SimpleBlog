using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        public ActionResult Login()
        {
            return View(new AuthLogin
            {
            });
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {


            using (Db db = new Db())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == form.Username);
                if (user == null)
                    SimpleBlog.Models.UserDTO.Fakehash();

                if (user == null || !user.CheckPassword(form.Password))
                    ModelState.AddModelError("Username", "Username or password is incorrect");
            }

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(form.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");
        }
    }
}