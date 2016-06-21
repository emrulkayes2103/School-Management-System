using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSupport.Tech;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class parentController : Controller
    {

        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(ParentsModel parents)
        {
            if (string.IsNullOrEmpty(parents.parentName))
            {
                ModelState.AddModelError("parentName", "Please Emter parents Name");
                TempData["ErrorMsg"] = "Please enter parents name ";
            }
            if (string.IsNullOrEmpty(parents.parentEmail))
            {
                ModelState.AddModelError("parentEmail", "Please Emter parents email");
                TempData["ErrorMsg"] = "Please enter parents email ";
            }
            if (string.IsNullOrEmpty(parents.parentContactNumber))
            {
                ModelState.AddModelError("parentContactNumber", "Please Emter parents contact Number");
                TempData["ErrorMsg"] = "Please enter parents contact Number ";
            }
            if (string.IsNullOrEmpty(parents.parentAddress))
            {
                ModelState.AddModelError("parentAddress", "Please Emter parents address");
                TempData["ErrorMsg"] = "Please enter parents address ";
            }
            if (string.IsNullOrEmpty(parents.parentProfession))
            {
                ModelState.AddModelError("parentProfession", "Please Emter parents profession");
                TempData["ErrorMsg"] = "Please enter parents profession ";
            }
            if (string.IsNullOrEmpty(parents.parentPassword))
            {
                ModelState.AddModelError("parentPassword", "Please Emter parents password");
                TempData["ErrorMsg"] = "Please Emter parents password";
            }
            if (string.IsNullOrEmpty(parents.parentsReTypePass))
            {
                ModelState.AddModelError("parentsReTypePass", "Please Emter parents re-type password");
                TempData["ErrorMsg"] = "Please Emter parents re-type password ";
            }
            if (parents.parentPassword != parents.parentsReTypePass)
            {
                ModelState.AddModelError("parentsReTypePass", "Password and re-type password does not match");
                TempData["ErrorMsg"] = "Password and re-type password does not match ";
            }
            if (checkDuplicateEmail(parents.parentEmail))
            {
                ModelState.AddModelError("parentEmail", "Email is already in use");
                TempData["ErrorMsg"] = "Email is already in use ";
            }
            if (ModelState.IsValid)
            {
                bool st = false;
                st = parents.CreateParents(parents);
                if (st)
                {
                    TempData["successMsg"] = "Parents Created successfully";
                    return RedirectToAction("create");
                }
                else
                {
                    TempData["ErrorMsg"] = "Parents can not be created";
                    return View();
                }
            }
            return View();
        }
        protected bool checkDuplicateEmail(string Email)
        {
            bool st = false;
            DataTable dt = new DataTable();
            users checkUserBll = new users();
            try
            {
                dt = checkUserBll.CheckDuplicateEmail(Email);
                if (dt.Rows.Count > 0)
                {
                    st = true;
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
            }
            return st;
        }

        [HttpGet]
        public ActionResult list()
        {
            ParentsModel parent = new ParentsModel();
            List<ParentsModel> parentList = parent.Parents.ToList();
            if (parentList.Count <= 0)
            {
                TempData["ErrorMsg"] = "No Data Found";
                return View(parentList);
            }
            return View(parentList);
        }

        [HttpPost]
        public ActionResult delete(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                return RedirectToAction("list");
            }
            else
            {
                ParentsModel parent = new ParentsModel();
                bool st = false;
                st = parent.DeleteParentsById(parentId);
                if (st)
                {
                    TempData["successMsg"] = "Parent  Deleted Successfully";
                    return RedirectToAction("list");
                }
                else
                {
                    TempData["ErrorMsg"] = "Parent can not be Deleted";
                    return RedirectToAction("list");
                }
            }
            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult edit(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                return RedirectToAction("list");
            }
            else
            {
                ParentsModel parents = new ParentsModel();
                parents =
                    parents.Parents.Single(parent => parent.parentId == AppSupportLibraryManager.DecryptString(parentId));
                return View(parents);
            }
            
        }
        [HttpPost]
        public ActionResult edit(ParentsModel parents)
        {
            ParentsModel parentsPreViuosData = parents.Parents.Single(parent => parent.parentId == AppSupportLibraryManager.DecryptString(parents.parentId));
            parents.parentEmail = parentsPreViuosData.parentEmail;
            if (string.IsNullOrEmpty(parents.parentName))
            {
                ModelState.AddModelError("parentName", "Please Emter parents Name");
                TempData["ErrorMsg"] = "Please enter parents name ";
            }
            if (string.IsNullOrEmpty(parents.parentEmail))
            {
                ModelState.AddModelError("parentEmail", "Please Emter parents email");
                TempData["ErrorMsg"] = "Please enter parents email ";
            }
            if (string.IsNullOrEmpty(parents.parentContactNumber))
            {
                ModelState.AddModelError("parentContactNumber", "Please Emter parents contact Number");
                TempData["ErrorMsg"] = "Please enter parents contact Number ";
            }
            if (string.IsNullOrEmpty(parents.parentAddress))
            {
                ModelState.AddModelError("parentAddress", "Please Emter parents address");
                TempData["ErrorMsg"] = "Please enter parents address ";
            }
            if (string.IsNullOrEmpty(parents.parentProfession))
            {
                ModelState.AddModelError("parentProfession", "Please Emter parents profession");
                TempData["ErrorMsg"] = "Please enter parents profession ";
            }
            if (ModelState.IsValid)
            {
                bool st = false;
                st = parents.UpdateParentsById(parents);
                if (st)
                {
                    TempData["successMsg"] = "Parents Updated successfully";
                    return RedirectToAction("list");
                }
                else
                {
                    TempData["ErrorMsg"] = "Parents can not be updated";
                    return View();
                }
            }
            return View();
        }
    }
}
