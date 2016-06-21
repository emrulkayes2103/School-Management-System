using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AppSupport.Tech;
using System.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class usergroupController : Controller
    {
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(UserGroupModel usergroup) 
        {
            try
            {
                if(string.IsNullOrEmpty(usergroup.groupName))
                {
                    ModelState.AddModelError("groupName", "*** Please Enter Group Name");
                }

                bool st = false;
                if(ModelState.IsValid)
                {
                    st = usergroup.CreateUserGroup();
                    if(st)
                    {

                        TempData["successMsg"] = "User Group Created Successfully";
                        return RedirectToAction("create");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "User Group Can not be created";
                    }
                }
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("errorMsgTitle", ex.ToString());
            }
            return View();
        }
        [HttpGet]
        public ActionResult list()
        {
            UserGroupModel userGroup = new UserGroupModel();
            List<UserGroupModel> usergroupList = userGroup.usergroup.ToList();
            try
            {  
                if(usergroupList !=null)
                {
                    return View(usergroupList);
                }
                else
                {
                    TempData["ErrorMsg"] = "No Data Found";
                    return View(usergroupList);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("errorMsgTitle", ex.ToString());
                return View(usergroupList);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(string groupId)
        {
            try
            {
                groupId = AppSupportLibraryManager.DecryptString(groupId);
                UserGroupModel userGroup = new UserGroupModel();
                
                if(string.IsNullOrEmpty(groupId))
                {
                    return RedirectToAction("list");
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = userGroup.getGroupDetailsByGroupId(groupId);
                    if(dt.Rows.Count>0)
                    {
                        userGroup.groupId = dt.Rows[0]["UserGroupId"].ToString();
                        userGroup.groupName = dt.Rows[0]["UserGroupName"].ToString();
                        userGroup.groupDesc = dt.Rows[0]["Description"].ToString();
                        return View(userGroup);
                    }
                    else
                    {
                        return RedirectToAction("list");
                    }
                }
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("errorMsgTitle", ex.ToString());
                TempData["ErrorMsg"] = ex.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(UserGroupModel usergroup)
        {
            try
            {
                if (string.IsNullOrEmpty(usergroup.groupName))
                {
                    ModelState.AddModelError("groupName", "*** Please Enter Group Name");
                }

                bool st = false;
                if (ModelState.IsValid)
                {
                    usergroup.groupId = AppSupportLibraryManager.DecryptString(usergroup.groupId);
                    st = usergroup.updateUserGroupByGroupId(usergroup.groupId);
                    if (st)
                    {

                        TempData["successMsg"] = "User Group updated Successfully";
                        return RedirectToAction("list");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "User Group Can not be updated";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("errorMsgTitle", ex.ToString());
                return View();
            }
        }

        [HttpPost]
        public ActionResult Actions(string groupId, string action)
        {
            groupId = AppSupportLibraryManager.DecryptString(groupId);
            UserGroupModel userGroupModel = new UserGroupModel();
            bool st = false;
            try
            {
                switch (action)
                {
                    case "Active":
                    {
                        st = userGroupModel.activateUserGroupById(groupId);
                        if (st)
                        {
                            TempData["successMsg"] = "User group Activated Successfully";
                            return RedirectToAction("list");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "User group can not be Activated";
                            return RedirectToAction("list");
                        }
                    }
                        break;
                    case "Deactivate":
                    {
                        st = userGroupModel.DeactivateUserGroupById(groupId);
                        if (st)
                        {
                            TempData["successMsg"] = "User group Deactivated Successfully";
                            return RedirectToAction("list");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "User group can not be Deactivated";
                            return RedirectToAction("list");
                        }
                    }
                        break;
                    case "Delete":
                        {
                           
                            st = userGroupModel.deleteUserGroupById(groupId);
                            if (st)
                            {
                                TempData["successMsg"] = "User group deleted Successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "User group can not be deleted";
                                return RedirectToAction("list");
                            }
                        }
                        break;
                    default:
                    {
                        return RedirectToAction("list");
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorMsg"] = "User Group Can not be Actioned";
                return RedirectToAction("list");
            }
            return RedirectToAction("list");
        }
    }
}
