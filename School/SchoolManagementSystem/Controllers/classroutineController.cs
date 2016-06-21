using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class classroutineController : Controller
    {
        [HttpGet]
        public ActionResult create()
        {
            ClassRoutineModel classRoutineModel = new ClassRoutineModel();
            return View(classRoutineModel);
        }

        [HttpPost]
        public ActionResult create(ClassRoutineModel classRoutineModel)
        {
            if (string.IsNullOrEmpty(classRoutineModel.SelectedClass))
            {
                TempData["ErrorMsg"] = "Please select Class";
                ModelState.AddModelError("classId","Please Select class");
            }
            if (string.IsNullOrEmpty(classRoutineModel.SeletedSection))
            {
                TempData["ErrorMsg"] = "Please select Section";
                ModelState.AddModelError("classId", "Please Select Section");
            }
            if (string.IsNullOrEmpty(classRoutineModel.SelectedSubject))
            {
                TempData["ErrorMsg"] = "Please select Subject";
                ModelState.AddModelError("SelectedSubject","Please select subject");
            }
            if (string.IsNullOrEmpty(classRoutineModel.day))
            {
                TempData["ErrorMsg"] = "Please select day";
                ModelState.AddModelError("day","Please select day");
            }
            if (string.IsNullOrEmpty(classRoutineModel.strtTime))
            {
                TempData["ErrorMsg"] = "Please enter start time";
                ModelState.AddModelError("day", "Please enter start time");
            }
            if (string.IsNullOrEmpty(classRoutineModel.endTime))
            {
                TempData["ErrorMsg"] = "Please enter End time";
                ModelState.AddModelError("day", "Please enter Emdtime");
            }
            //if (string.Compare(classRoutineModel.strtTime,classRoutineModel.endTime))
            //{
            //    TempData["ErrorMsg"] = "Start time and end time can not be same ";
            //    ModelState.AddModelError("day", "Please enter Emdtime");
            //}
            if (ModelState.IsValid)
            {
                bool st = false;
                st = classRoutineModel.createClassRoutine(classRoutineModel);
                if (st)
                {
                    TempData["successMsg"] = "Class Routine successfully created";
                    return RedirectToAction("create");
                }
                else
                {
                    TempData["ErrorMsg"] = "Class Routine can not be created";
                    return View(classRoutineModel);
                }
            }
            return View(classRoutineModel);
        }

        public ActionResult list()
        {
            return View();
        }
    }
}
