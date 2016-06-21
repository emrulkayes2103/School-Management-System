using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Models;
using AppSupport.Tech;

namespace SchoolManagementSystem.Controllers
{
    public class classController : Controller
    {
        [HttpGet]
        public ActionResult create()
        {
            ClassModel classModel = new ClassModel();
            return View(classModel);
        }
        [HttpPost]
        public ActionResult create(ClassModel classModel)
        {
            ClassModel classModell = new ClassModel();
            int num;
            if (string.IsNullOrEmpty(classModel.className))
            {
                ModelState.AddModelError("className", "Please Enter class name");
                TempData["ErrorMsg"] = "Please Enter class name";
            }
            if (string.IsNullOrEmpty(Convert.ToString(classModel.classNumericNumber)))
            {
                ModelState.AddModelError("classNumericNumber", "Please Enter class numaric number");
                TempData["ErrorMsg"] = "Please Enter class numaric number";
            }
            if (string.IsNullOrEmpty(classModel.classTeacherId) || classModel.classTeacherId == "Select Teacher")
            {
                ModelState.AddModelError("classTeacherId", "Please select class teacher");
                TempData["ErrorMsg"] = "Please Select class teacher";
            }
            
            if (ModelState.IsValid)
            {
                bool st = false;
                st = classModel.CreateClass(classModel);
                if (st)
                {
                    TempData["successMsg"] = "Class created successfully";
                    return RedirectToAction("create");
                }
                else
                {
                    TempData["ErrorMsg"] = "Class can not be created";
                    
                    return View(classModell);
                }
            }
            
            return View(classModell);
        }

        public ActionResult list()
        {
            ClassModel classModel = new ClassModel();
            List<ClassModel> classModels = classModel.Classes.ToList();
            if (classModels.Count <=0)
            {
                TempData["ErrorMsg"] = "No class data found";
                return View(classModels);
            }
            return View(classModels);
        }

        [HttpGet]
        public ActionResult edit(string classId)
        {
            if (string.IsNullOrEmpty(classId))
            {
                TempData[""] = "No Data Found";
                return RedirectToAction("list");
            }
            else
            {
                ClassModel classModel = new ClassModel();
                classModel = classModel.Classes.Single(classs => classs.classId == AppSupportLibraryManager.DecryptString(classId));
                //ViewBag.teacherList = new SelectList(classModel.teacherDropDwnList(), "teacherId", "teacherName",
                //    classModel.teacherSelected);
                return View(classModel);
            }
        }

        [HttpPost]
        public ActionResult delete(string classId)
        {
            if (string.IsNullOrEmpty(classId))
            {
                TempData["ErrorMsg"] = "Please Select";
            }
            else
            {
                bool st = false;
                ClassModel classModel = new ClassModel();
                st = classModel.DeleteClassById(classId);
                if (st)
                {
                    TempData["successMsg"] = "Class successfully deleted";
                }
                else
                {
                    TempData["ErrorMsg"] = "Class can not be deleted";
                }
            }
            return RedirectToAction("list");
        }
        [HttpPost]
        public ActionResult edit(ClassModel classModel)
        {
            int num;
            classModel.classTeacherId = classModel.teacherSelected;

            if (string.IsNullOrEmpty(classModel.className))
            {
                ModelState.AddModelError("className", "Please Enter class name");
                TempData["ErrorMsg"] = "Please Enter class name";
            }
            if (string.IsNullOrEmpty(Convert.ToString(classModel.classNumericNumber)))
            {
                ModelState.AddModelError("classNumericNumber", "Please Enter class numaric number");
                TempData["ErrorMsg"] = "Please Enter class numaric number";
            }
            if (string.IsNullOrEmpty(classModel.classTeacherId) || classModel.classTeacherId == "Select Teacher")
            {
                ModelState.AddModelError("classTeacherId", "Please select class teacher");
                TempData["ErrorMsg"] = "Please Select class teacher";
            }

            if (ModelState.IsValid)
            {
                bool st = false;
                st = classModel.UpdateClass(classModel);
                if (st)
                {
                    TempData["successMsg"] = "Class updated successfully";
                    return RedirectToAction("list");
                }
                else
                {
                    TempData["ErrorMsg"] = "Class can not be updated";

                    return View(classModel);
                }
            }

            return View(classModel);
        }
    }
}
