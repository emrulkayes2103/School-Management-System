using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Models;
using Newtonsoft.Json;

namespace SchoolManagementSystem.Controllers
{
    public class subjectController : Controller
    {
        [HttpGet]
        public ActionResult create()
        {
            SubjectModel subjectModel = new SubjectModel();
            return View(subjectModel);
        }

        [HttpPost]
        public ActionResult create(SubjectModel subjectModel)
        {
            if (string.IsNullOrEmpty(subjectModel.subjectName))
            {
                ModelState.AddModelError("subjectName", "Please Enter subject name");
                TempData["ErrorMsg"] = "Please enter subject name ";
            }
             if (string.IsNullOrEmpty(subjectModel.SelectedClassId) || subjectModel.SelectedClassId == "--Select Class--")
            {
                ModelState.AddModelError("SelectedClassId", "Please Select subject class");
                TempData["ErrorMsg"] = "Please Select subject class ";
            }
             if (string.IsNullOrEmpty(subjectModel.selectedTeacherId) || subjectModel.selectedTeacherId == "--Select Teacher--")
            {
                ModelState.AddModelError("selectedTeacherId", "Please Select subject teacher");
                TempData["ErrorMsg"] = "Please select subject teacher ";
            }
            if (ModelState.IsValid)
            {
                bool st = false;
                st = subjectModel.createSubject(subjectModel);
                if (st)
                {
                    TempData["successMsg"] = "Subject created successfully";
                    return RedirectToAction("create");
                }
                else
                {
                    TempData["ErrorMsg"] = "Subject can not created";
                    return View(subjectModel);
                }
            }
            return View(subjectModel);
        }

        [HttpGet]
        public ActionResult list()
        {
            SubjectModel subjectModel = new SubjectModel();
            return View(subjectModel);
        }

        public string GetSubjectListByClassId(string ClassId)
        {
            string subjectLList = string.Empty;

            SubjectModel subjectModel = new SubjectModel();

            DataTable dt = new DataTable();

            dt = subjectModel.getSubjectListByClassId(ClassId);
            if (dt.Rows.Count >0)
            {
                subjectLList = JsonConvert.SerializeObject(dt);
            }

            return subjectLList;
        }

        [HttpPost]
        public ActionResult actions(string subjectId,string action)
        {
            if (string.IsNullOrEmpty(subjectId) || string.IsNullOrEmpty(action))
            {
                return RedirectToAction("list");
            }
            else
            {
                switch (action)
                {
                    case "Edit":
                    {
                        return RedirectToAction("edit", "subject", new { subId = subjectId });
                    }
                        break;
                    case "Delete":
                    {
                        SubjectModel subjectModel = new SubjectModel();
                        bool st = false;
                        st = subjectModel.deleteSubjectById(subjectId);
                        if (st)
                        {
                            TempData["successMsg"] = "Subject Succesfully Deleted";
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Subject can not be deleted";
                        }
                        return RedirectToAction("list");
                    }
                        break;
                    default:
                    {
                        return RedirectToAction("list");
                    }
                }
            }
            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult edit(string subId)
        {
            if (string.IsNullOrEmpty(subId))
            {
                return RedirectToAction("list");
            }
            else
            {
                SubjectModel subjectModel = new SubjectModel();
                subjectModel = subjectModel.Subjects.Single(x => x.subjectId == subId);
                if (subjectModel == null)
                {
                    return RedirectToAction("list");
                }
                else
                {
                    return View(subjectModel);
                }
               
            }
        }
        [HttpPost]
        public ActionResult edit(SubjectModel subjectModel)
        {
            if (string.IsNullOrEmpty(subjectModel.subjectId))
            {
                return RedirectToAction("list");
            }
            if (string.IsNullOrEmpty(subjectModel.subjectName))
            {
                ModelState.AddModelError("subjectName", "Please Enter subject name");
                TempData["ErrorMsg"] = "Please enter subject name ";
            }
            if (string.IsNullOrEmpty(subjectModel.SelectedClassId) || subjectModel.SelectedClassId == "--Select Class--")
            {
                ModelState.AddModelError("SelectedClassId", "Please Select subject class");
                TempData["ErrorMsg"] = "Please Select subject class ";
            }
            if (string.IsNullOrEmpty(subjectModel.selectedTeacherId) || subjectModel.selectedTeacherId == "--Select Teacher--")
            {
                ModelState.AddModelError("selectedTeacherId", "Please Select subject teacher");
                TempData["ErrorMsg"] = "Please select subject teacher ";
            }
            if (ModelState.IsValid)
            {
                bool st = false;
                st = subjectModel.UpdateSubjectbyId(subjectModel);
                if (st)
                {
                    TempData["successMsg"] = "Subject Updated  successfully";
                    return RedirectToAction("list");
                }
                else
                {
                    TempData["ErrorMsg"] = "Subject can not be updated";
                    return View(subjectModel);
                }
            }
            return View(subjectModel);
        }
    }
}
