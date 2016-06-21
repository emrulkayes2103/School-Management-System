using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSupport.Tech;

namespace SchoolManagementSystem.Controllers
{
    public class _LayoutController : Controller
    {
        public ActionResult _Layout()
        {
            string UserName = AppSupportSessionManager.Get("UserName").ToString();
            string Email = AppSupportSessionManager.Get("UserEmail").ToString();
            string UserRole = AppSupportSessionManager.Get("UserRole").ToString();
           
            string imageName = AppSupportSessionManager.Get("ProfileImageName").ToString();

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(UserRole))
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            } 
        }
    }
}
