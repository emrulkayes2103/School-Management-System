using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AppSupport.Tech;
using SchoolManagementSystem.Models;
using System.Data;

namespace SchoolManagementSystem.Controllers
{
    public class loginController : Controller
    {
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(login logData)
        {
            try
            {
                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(logData.Email))
                {
                    ModelState.AddModelError("Email","Enter your email address");
                    TempData["ErrorMsg"] = "Please enter email address";
                    return View();
                }
                if (string.IsNullOrEmpty(logData.Password))
                {
                    ModelState.AddModelError("Email", "Enter your password");
                    TempData["ErrorMsg"] = "Please enter password";
                    return View();
                }
                if (ModelState.IsValid)
                {
                    dt = logData.userLogIn(logData);
                    if (dt.Rows.Count>0)
                    {
                        AppSupportSessionManager.Add("UserName", dt.Rows[0]["Name"].ToString());
                        AppSupportSessionManager.Add("UserEmail", dt.Rows[0]["Email"].ToString());
                        AppSupportSessionManager.Add("UserId", dt.Rows[0]["Serial"].ToString());
                        AppSupportSessionManager.Add("UserRole", dt.Rows[0]["userRole"].ToString());
                        
                        AppSupportSessionManager.Add("ProfileImageName", dt.Rows[0]["profilePicName"].ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Email or password does not match";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
                return View();
            }

        }

        public ActionResult logout()
        {
            AppSupportSessionManager.RemoveAll();
            return RedirectToAction("login", "login");
        }
    }
}
