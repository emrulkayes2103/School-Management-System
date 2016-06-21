using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SchoolManagementSystem.Models;
using Newtonsoft.Json;

namespace SchoolManagementSystem.Controllers
{
    public class sectionController : Controller
    {
        [HttpGet]
        public ActionResult create()
        {
            SectionModel sectionModel = new SectionModel();
            return View(sectionModel);
        }

        [HttpPost]
        public ActionResult create(SectionModel sectionModel)
        {
            if (string.IsNullOrEmpty(sectionModel.SectionName))
            {
                ModelState.AddModelError("SectionName", "Enter Section Name ");
                TempData["ErrorMsg"] = "Enter Section Name";
            }
            if (string.IsNullOrEmpty(sectionModel.SectionNickName))
            {
                ModelState.AddModelError("SectionNickName", "Enter Section Nick Name ");
                TempData["ErrorMsg"] = "Enter Section Nick Name";
            }
            if (string.IsNullOrEmpty(sectionModel.SelectedClass) || sectionModel.SelectedClass == "Select Class")
            {
                ModelState.AddModelError("SelectedClass", "Select Class ");
                TempData["ErrorMsg"] = "Select Class";
            }
            if (string.IsNullOrEmpty(sectionModel.SelectedTeaher) || sectionModel.SelectedTeaher == "Select Teacher")
            {
                ModelState.AddModelError("SelectedTeaher", "Select Teacher");
                TempData["ErrorMsg"] = "Select Teacher";
            }

            if (ModelState.IsValid)
            {
                bool st = false;
                st = sectionModel.CreateSection(sectionModel);
                if (st)
                {
                    TempData["successMsg"] = "Section Succesfully Created";
                    return RedirectToAction("create");
                }
                else
                {
                    return View(sectionModel);
                }
            }
            return View(sectionModel);
        }

        [HttpGet]
        public ActionResult list()
        {
            SectionModel sectionModel = new SectionModel();

            return View(sectionModel);
        }

        public string SectionByClassId(string classId)
        {
            string sections = "";
            if (!string.IsNullOrEmpty(classId) || classId != "Select Class")
            {
                SectionModel sectionModel = new SectionModel();
                DataTable dt = new DataTable();
                dt = sectionModel.getSectionsByClassId(classId);
                if (dt.Rows.Count>0)
                {
                    sections = JsonConvert.SerializeObject(dt);
                }
            }
            return sections;
        }

        [HttpPost]
        public ActionResult actions(string sectionId, string action)
        {
            if (string.IsNullOrEmpty(sectionId) || string.IsNullOrEmpty(action))
            {
                return RedirectToAction("list");
            }
            else
            {
                switch (action)
                {
                    case "Edit":
                    {
                        return RedirectToAction("edit", "section", new { secId = sectionId });
                    }
                        break;
                    case "Delete":
                    {
                        bool st = false;

                        SectionModel sectionModel = new SectionModel();
                        st = sectionModel.DeleteSectionById(sectionId);
                        if (st)
                        {
                            TempData["successMsg"] = "Section Succesfully Deleted";
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Section can not be deleted";
                        }
                        return RedirectToAction("list");
                    }
                    default:
                    {
                        return RedirectToAction("list");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult edit(string secId)
        {
            if (string.IsNullOrEmpty(secId))
            {
                return RedirectToAction("list");
            }
            else
            {
                SectionModel sectionModel = new SectionModel();
                sectionModel = sectionModel.Sections.Single(section => section.sectionId == secId);

                return View(sectionModel);

            }
           
        }

        [HttpPost]
        public ActionResult edit(SectionModel sectionModel)
        {
            if (string.IsNullOrEmpty(sectionModel.SectionName))
            {
                ModelState.AddModelError("SectionName", "Enter Section Name ");
                TempData["ErrorMsg"] = "Enter Section Name";
            }
            if (string.IsNullOrEmpty(sectionModel.SectionNickName))
            {
                ModelState.AddModelError("SectionNickName", "Enter Section Nick Name ");
                TempData["ErrorMsg"] = "Enter Section Nick Name";
            }
            if (string.IsNullOrEmpty(sectionModel.SelectedClass) || sectionModel.SelectedClass == "Select Class")
            {
                ModelState.AddModelError("SelectedClass", "Select Class ");
                TempData["ErrorMsg"] = "Select Class";
            }
            if (string.IsNullOrEmpty(sectionModel.SelectedTeaher) || sectionModel.SelectedTeaher == "Select Teacher")
            {
                ModelState.AddModelError("SelectedTeaher", "Select Teacher");
                TempData["ErrorMsg"] = "Select Teacher";
            }

            if (ModelState.IsValid)
            {
                bool st = false;
                st = sectionModel.UpdateSectionById(sectionModel);
                if (st)
                {
                    TempData["successMsg"] = "Section Succesfully Updated";
                    return RedirectToAction("list");
                }
                else
                {
                    TempData["ErrorMsg"] = "Section Can not be Updated";
                    return View(sectionModel);
                }
            }
            return RedirectToAction("list");
        }
    }
}
