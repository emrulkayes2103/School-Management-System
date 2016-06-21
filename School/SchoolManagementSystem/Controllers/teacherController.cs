using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSupport.Tech;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class teacherController : Controller
    {
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(TeacherModel teachers, HttpPostedFileBase teacherImageFile)
        {
            try
            {

                if (string.IsNullOrEmpty(teachers.teacherName))
                {
                    ModelState.AddModelError("teacherName", "Enter Teacher Name");
                    TempData["ErrorMsg"] = "Plase enter teacher name";
                }
                if (string.IsNullOrEmpty(teachers.teacherEmail))
                {
                    ModelState.AddModelError("teacherEmail", "Enter Teacher Email");
                    TempData["ErrorMsg"] = "Plase enter teacher email";
                }
                if (string.IsNullOrEmpty(teachers.teacherDob))
                {
                    ModelState.AddModelError("teacherDob", "Enter Teacher Email");
                    TempData["ErrorMsg"] = "Plase enter teacher date of birth";
                }
                if (string.IsNullOrEmpty(teachers.teacherGender))
                {
                    ModelState.AddModelError("teacherGender", "Enter Teacher Gender");
                    TempData["ErrorMsg"] = "Plase enter teacher gender";
                }
                if (string.IsNullOrEmpty(teachers.teacherPermanentAdd))
                {
                    ModelState.AddModelError("teacherPermanentAdd", "Enter Teacher permanent address");
                    TempData["ErrorMsg"] = "Plase enter teacher permanent address";
                }
                if (string.IsNullOrEmpty(teachers.teacherPresentAdd))
                {
                    ModelState.AddModelError("teacherPermanentAdd", "Enter Teacher present address");
                    TempData["ErrorMsg"] = "Plase enter teacher Present address";
                }
                if (string.IsNullOrEmpty(teachers.teacherContuctNumber))
                {
                    ModelState.AddModelError("teacherContuctNumber", "Enter Teacher Contuct Number");
                    TempData["ErrorMsg"] = "Plase enter teacher contact Number";
                }
                if (string.IsNullOrEmpty(teachers.teacherpassword))
                {
                    ModelState.AddModelError("teacherpassword", "Enter Teacher password");
                    TempData["ErrorMsg"] = "Plase enter teacher password";
                }
                if (string.IsNullOrEmpty(teachers.reTypeTeacherPass))
                {
                    ModelState.AddModelError("reTypeTeacherPass", "Enter Teacher Re-Type Password");
                    TempData["ErrorMsg"] = "Plase enter teacher Re-Type Password";
                }
                if (teachers.teacherpassword != teachers.reTypeTeacherPass)
                {
                    ModelState.AddModelError("reTypeTeacherPass", "Enter Teacher Password and Re-Type Password");
                    TempData["ErrorMsg"] = "Plase enter teacher password and Re-Type Password";
                }

                if (checkDuplicateEmail(teachers.teacherEmail))
                {
                    ModelState.AddModelError("teacherEmail", "Email id is already exists");
                    TempData["ErrorMsg"] = "Email id is already exists";
                }
                teachers.proPicname = propicName(teacherImageFile,teachers.teacherEmail);
                if (string.IsNullOrEmpty(teachers.proPicname))
                {
                    ModelState.AddModelError("proPicname", "Enter Teacher Profile Image");
                    TempData["ErrorMsg"] = "Plase enter teacher profile iamge";
                }

                if (ModelState.IsValid)
                {
                    bool st = false;
                    st = teachers.CreateTeacher(teachers);
                    if (st)
                    {
                        TempData["successMsg"] = "Teacher created successfully";
                        return RedirectToAction("create");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Teacher can not be created";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
                return View();
            }
           
        }

        protected string propicName(HttpPostedFileBase ImageFile,string teacherEmail)
        {
            string ProfileImagename = "";
            try
            {
                if (ImageFile == null)
                {
                    return "";
                }
                else
                {
                    string exten = Path.GetExtension(ImageFile.FileName);
                    if (exten.ToLower() == ".jpg" || exten.ToLower() == ".png"
                        || exten.ToLower() == ".jpeg" || exten.ToLower() == ".gif"
                        )
                    {
                        ProfileImagename = teacherEmail + exten;
                        string path = Server.MapPath("~/ProfileImage/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                            ImageFile.SaveAs(path + ProfileImagename);
                        }
                        else
                        {
                            ImageFile.SaveAs(path + ProfileImagename);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProfileImagename;
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

        public ActionResult list()
        {
            TeacherModel teachers = new TeacherModel();
            List<TeacherModel> teacherList = teachers.Teachers.ToList();
            return View(teacherList);
        }

        [HttpGet]
        public ActionResult edit(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
            {
                return RedirectToAction("list");
            }
            else
            {
                teacherId = AppSupportLibraryManager.DecryptString(teacherId);
                TeacherModel teachers = new TeacherModel();
                teachers = teachers.Teachers.Single(teacher => teacher.teacherId == teacherId);
                if (teachers == null)
                {
                    TempData["ErrorMsg"] = "No Data Found";
                    return RedirectToAction("list");
                }
                return View(teachers);
            }
        }

        [HttpPost]
        public ActionResult edit(TeacherModel teachers, HttpPostedFileBase teacherImageFile)
        {
            try
            {
                
                TeacherModel teacherPreviousData =
                   teachers.Teachers.Single(
                       teacher => teacher.teacherId == AppSupportLibraryManager.DecryptString(teachers.teacherId));

                teachers.teacherEmail = teacherPreviousData.teacherEmail;
                if (string.IsNullOrEmpty(teachers.teacherName))
                {
                    ModelState.AddModelError("teacherName", "Enter Teacher Name");
                    TempData["ErrorMsg"] = "Plase enter teacher name";
                }
                if (string.IsNullOrEmpty(teachers.teacherEmail))
                {
                    ModelState.AddModelError("teacherEmail", "Enter Teacher Email");
                    TempData["ErrorMsg"] = "Plase enter teacher email";
                }
                if (string.IsNullOrEmpty(teachers.teacherDob))
                {
                    ModelState.AddModelError("teacherDob", "Enter Teacher Email");
                    TempData["ErrorMsg"] = "Plase enter teacher date of birth";
                }
                if (string.IsNullOrEmpty(teachers.teacherGender))
                {
                    ModelState.AddModelError("teacherGender", "Enter Teacher Gender");
                    TempData["ErrorMsg"] = "Plase enter teacher gender";
                }
                if (string.IsNullOrEmpty(teachers.teacherPermanentAdd))
                {
                    ModelState.AddModelError("teacherPermanentAdd", "Enter Teacher permanent address");
                    TempData["ErrorMsg"] = "Plase enter teacher permanent address";
                }
                if (string.IsNullOrEmpty(teachers.teacherPresentAdd))
                {
                    ModelState.AddModelError("teacherPermanentAdd", "Enter Teacher present address");
                    TempData["ErrorMsg"] = "Plase enter teacher Present address";
                }
                if (string.IsNullOrEmpty(teachers.teacherContuctNumber))
                {
                    ModelState.AddModelError("teacherContuctNumber", "Enter Teacher Contuct Number");
                    TempData["ErrorMsg"] = "Plase enter teacher contact Number";
                }

                if (teacherImageFile == null)
                {
                    teachers.proPicname = "";
                }
                else
                {
                    teachers.proPicname = propicName(teacherImageFile, teachers.teacherEmail);
                }

                if (ModelState.IsValid)
                {
                    bool st = false;
                    st = teachers.UpdateTeacher(teachers);
                    if (st)
                    {
                        TempData["successMsg"] = "Teacher Updated successfully";
                        return RedirectToAction("list");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Teacher can not be Updated";
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult actions(string teacherId,string action)
        {

            bool st = false;
            TeacherModel teacher = new TeacherModel();
            if (string.IsNullOrEmpty(action))
            {
                TempData["ErrorMsg"] = "Please Press correctlly";
            }
            else
            {
                switch (action)
                {
                    case "Activate":
                    {
                        st = teacher.actiateTeacherById(teacherId);
                        if (st)
                        {
                            TempData["successMsg"] = "Teacher activated successfully";
                            return RedirectToAction("list");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Teacher can not be activated";
                            return RedirectToAction("list");
                        }
                    }
                        break;
                    case "Deactivate":
                        {
                            st = teacher.DeactiateTeacherById(teacherId);
                            if (st)
                            {
                                TempData["successMsg"] = "Teacher Deactivated successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "Teacher can not be Deactivated";
                                return RedirectToAction("list");
                            }
                        }
                        break;
                    case "Delete":
                        {
                            st = teacher.DeleteTeacherById(teacherId);
                            if (st)
                            {
                                TempData["successMsg"] = "Teacher Deleted successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "Teacher can not be Deleted";
                                return RedirectToAction("list");
                            }
                        }
                        break;
                    default:
                    {
                        TempData["ErrorMsg"] = "Please Use software like a gentale man";
                        return RedirectToAction("list");
                    }
                        break;
                }
            }
            return RedirectToAction("list","teacher");
        }

        [HttpGet]
        public ActionResult Details(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
            {
                return RedirectToAction("list");
            }
            else
            {
                teacherId = AppSupportLibraryManager.DecryptString(teacherId);
                TeacherModel teachers = new TeacherModel();
                teachers = teachers.Teachers.Single(teacher => teacher.teacherId == teacherId);
                if (teachers == null)
                {
                    TempData["ErrorMsg"] = "No Data Found";
                    return RedirectToAction("list");
                }
                return View(teachers);
            }
        }
    }
}
