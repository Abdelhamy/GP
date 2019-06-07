using CollageSystem.Core;
using CollageSystem.Core.CustomModel;
using CollageSystem.Data;
using CollageSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CollageSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult test()
        {
            return View();
        }
        IUsersServices usersServices;
        Data.MyRoleProvide RoleProvide = new Data.MyRoleProvide();

        public AccountController(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.EmailAddress))
                {
                    ModelState.AddModelError("LogOnError", "يوجد خطأ فى البريد الالكترونى او كلمه المرور");

                }
                else
                {
                    var objUser = usersServices.login(model.EmailAddress, model.Password);
                    if (objUser == null)
                    {
                        ModelState.AddModelError("LogOnError", "يوجد خطأ فى البريد الالكترونى او كلمه المرور");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, model.RememberMe);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                           && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            Session["User"] = objUser;
                            Session["UserName"] = objUser.FirstName;
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            Session["User"] = objUser;
                            Session["UserName"] = objUser.FirstName;
                            return RedirectToAction("RedirectToDefault");
                        }

                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult RedirectToDefault()
        {
            User objUser = (User)Session["User"];
            string[] roles = RoleProvide.GetRolesForUser(objUser.EmailAddress);
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Semesters", new { area = "Administration" });
            }
            else if (roles.Contains("Student"))
            {
                //return RedirectToAction("Index", "Semesters", new { area = "Administration" });

                return RedirectToAction("Index", "StudentSemesters", new { area = "Student" });
            }
            else if (roles.Contains("Control"))
            {
                return RedirectToAction("Index", "SemesterMarks", new { area = "Control" });
            }
            else

            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Login");
        }
    }

}