using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SchoolManagementSystem.Models;
using System.IO;
using AppSupport.Tech;

namespace SchoolManagementSystem.Controllers
{
    public class userController : Controller
    {

        [HttpGet]
        public ActionResult create()
        {
            //ViewBag.userRole = new SelectList(getUserGruoup(""), "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult create(users user, HttpPostedFileBase file)
        {
            try
            {
               
                if (string.IsNullOrEmpty(user.Name))
                {
                    ModelState.AddModelError("Name", "Please enter your name");
                    TempData["ErrorMsg"] = "Please enter your name";
                }
                else if (string.IsNullOrEmpty(user.Email))
                {
                    ModelState.AddModelError("Email", "Please enter your email");
                    TempData["ErrorMsg"] = "Please enter your email";
                }
                else if (string.IsNullOrEmpty(user.DOB))
                {
                    ModelState.AddModelError("DOB", "Please enter your date of birth");
                    TempData["ErrorMsg"] = "Please enter your date of birth";
                }
                else if (string.IsNullOrEmpty(user.Gender))
                {
                    ModelState.AddModelError("Gender", "Please select gender");
                    TempData["ErrorMsg"] = "Please select gender";
                }
                else if (string.IsNullOrEmpty(user.ContactNumber))
                {
                    ModelState.AddModelError("ContactNumber", "Please enter your contact number");
                    TempData["ErrorMsg"] = "Please enter your contact number";
                }
                else if (string.IsNullOrEmpty(user.nationality))
                {
                    ModelState.AddModelError("nationality", "Please enter your nationality");
                    TempData["ErrorMsg"] = "Please enter your nationality";
                }
                else if (string.IsNullOrEmpty(user.bloodGroup))
                {
                    ModelState.AddModelError("bloodGroup", "Please select your blood group");
                    TempData["ErrorMsg"] = "Please select your blood group";
                }
                else if (string.IsNullOrEmpty(user.perManentAdd))
                {
                    ModelState.AddModelError("perManentAdd", "Please enter your permanent address");
                    TempData["ErrorMsg"] = "Please enter your permanent address";
                }
                else if (string.IsNullOrEmpty(user.presentAdd))
                {
                    ModelState.AddModelError("presentAdd", "Please enter your present address");
                    TempData["ErrorMsg"] = "Please enter your present address";
                }
                else if (string.IsNullOrEmpty(user.FathersName))
                {
                    ModelState.AddModelError("FathersName", "Please enter your father name ");
                    TempData["ErrorMsg"] = "Please enter your father name";
                }
                else if (string.IsNullOrEmpty(user.motherName))
                {
                    ModelState.AddModelError("motherName", "Please enter your mother name");
                    TempData["ErrorMsg"] = "Please enter your mother name";
                }
                else if (string.IsNullOrEmpty(user.ContactNumber))
                {
                    ModelState.AddModelError("ContactNumber", "Please enter your contact number");
                    TempData["ErrorMsg"] = "Please enter your contact number";
                }
                else if (file == null)
                {
                    ModelState.AddModelError("errorMsg", "Please enter your profile picture");
                    TempData["ErrorMsg"] = "Please enter your profile picture";
                }
                
                else if (string.IsNullOrEmpty(user.password))
                {
                    ModelState.AddModelError("errorMsg", "Please enter Password");
                    TempData["ErrorMsg"] = "Please enter Password";
                }
                else if (string.IsNullOrEmpty(user.reTypePass))
                {
                    ModelState.AddModelError("errorMsg", "Please enter re-type password");
                    TempData["ErrorMsg"] = "Please enter re-type password";
                }
                else if (user.password != user.reTypePass)
                {
                    ModelState.AddModelError("errorMsg", "Please enter role as an user");
                    TempData["ErrorMsg"] = "Password and re-type password dopes not match";
                }
                if (checkDuplicateEmail(user.Email))
                {
                    ModelState.AddModelError("Email", "Please enter role as an user");
                    TempData["ErrorMsg"] = "Email is already exists";
                }
                if (ModelState.IsValid)
                {
                    bool st = false;
                    string fileName = "";
                    if (file != null)
                    {
                        string extensiom = Path.GetExtension(file.FileName);
                        if (extensiom.ToLower() == ".jpg" || extensiom.ToLower() == ".png"
                            || extensiom.ToLower() == ".jpeg" || extensiom.ToLower() == ".gif"
                            )
                        {
                            fileName = user.Email + extensiom;
                            string path = Server.MapPath("~/ProfileImage/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                                file.SaveAs(path + fileName);
                            }
                            else
                            {
                                file.SaveAs(path + fileName);
                            }
                            if (string.IsNullOrEmpty(user.NationalID))
                            {
                                user.NationalID = "";
                            }
                            user.profileImage = fileName;
                            st = CreateUser(user);
                            if (st)
                            {
                                TempData["successMsg"] = "User Created successfully";
                                return RedirectToAction("create");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "User can not be created";
                                return View();
                            }

                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Please add an image for profile picture";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Please add an image for profile picture";
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

        //protected List<SelectListItem> getUserGruoup(string userrole)
        //{
        //    UserGroupModel userGroup = new UserGroupModel();
        //    List<SelectListItem> groupList = new List<SelectListItem>();

        //    foreach (var group in userGroup.activeusergroup)
        //    {
        //        groupList.Add(new SelectListItem()
        //        {
        //            Text = group.groupName,
        //            Value = group.groupId,
        //            Selected = (group.groupName == userrole)
        //        });
        //    }
        //    return groupList;
        //}

        protected bool CreateUser(users user)
        {
            bool st = false;
            try
            {
                st = user.createUsers(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
            }
            return st;
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

            users user = new users();
            List<users> userList = user.userslist.ToList();
            return View(userList);

        }

        [HttpGet]
        public ActionResult Edit(string userId)
        {
            
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMsg"] = "No data found";
                return RedirectToAction("list");
            }
            else
            {
                userId = AppSupportLibraryManager.DecryptString(userId);
                users user = new users();
                DataTable dt = new DataTable();
                dt = user.getUserDetailByID(userId);
                if (dt.Rows.Count > 0)
                {
                    user.id = userId;
                    user.Name = dt.Rows[0]["Name"].ToString();
                    user.Email = dt.Rows[0]["Email"].ToString();

                    user.DOB = dt.Rows[0]["DobUpdateFormat"].ToString();
                    user.Gender = dt.Rows[0]["Gender"].ToString();
                    user.ContactNumber = dt.Rows[0]["ContactNumber"].ToString();
                    user.nationality = dt.Rows[0]["Nationality"].ToString();
                    user.bloodGroup = dt.Rows[0]["BloodGroup"].ToString();
                    user.perManentAdd = dt.Rows[0]["permanentAdd"].ToString();
                    user.presentAdd = dt.Rows[0]["presentAdd"].ToString();
                    user.NationalID = dt.Rows[0]["nationalID"].ToString();
                    user.FathersName = dt.Rows[0]["FathersName"].ToString();
                    user.motherName = dt.Rows[0]["mothersName"].ToString();
                    user.userRole = dt.Rows[0]["userRole"].ToString();
                   
                    
                   // ViewBag.userRole = new SelectList(getUserGruoup(groupName), "Value", "Text");
                    return View(user);
                }
                else
                {
                    TempData["ErrorMsg"] = "No data found";
                    return RedirectToAction("list");
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(users user, HttpPostedFileBase file)
        {
            //ViewBag.userRole = new SelectList(getUserGruoup(""), "Value", "Text");
            users usersData = user.userslist.Single(x => AppSupportLibraryManager.DecryptString(x.id) == user.id);

           
            user.Email = usersData.Email;
            try
            {
                
                if (string.IsNullOrEmpty(user.Name))
                {
                    ModelState.AddModelError("Name", "Please enter your name");
                    TempData["ErrorMsg"] = "Please enter your name";
                }
                else if (string.IsNullOrEmpty(user.Email))
                {
                    ModelState.AddModelError("Email", "Please enter your email");
                    TempData["ErrorMsg"] = "Please enter your email";
                }
                else if (string.IsNullOrEmpty(user.DOB))
                {
                    ModelState.AddModelError("DOB", "Please enter your date of birth");
                    TempData["ErrorMsg"] = "Please enter your date of birth";
                }
                else if (string.IsNullOrEmpty(user.Gender))
                {
                    ModelState.AddModelError("Gender", "Please select gender");
                    TempData["ErrorMsg"] = "Please select gender";
                }
                else if (string.IsNullOrEmpty(user.ContactNumber))
                {
                    ModelState.AddModelError("ContactNumber", "Please enter your contact number");
                    TempData["ErrorMsg"] = "Please enter your contact number";
                }
                else if (string.IsNullOrEmpty(user.nationality))
                {
                    ModelState.AddModelError("nationality", "Please enter your nationality");
                    TempData["ErrorMsg"] = "Please enter your nationality";
                }
                else if (string.IsNullOrEmpty(user.bloodGroup))
                {
                    ModelState.AddModelError("bloodGroup", "Please select your blood group");
                    TempData["ErrorMsg"] = "Please select your blood group";
                }
                else if (string.IsNullOrEmpty(user.perManentAdd))
                {
                    ModelState.AddModelError("perManentAdd", "Please enter your permanent address");
                    TempData["ErrorMsg"] = "Please enter your permanent address";
                }
                else if (string.IsNullOrEmpty(user.presentAdd))
                {
                    ModelState.AddModelError("presentAdd", "Please enter your present address");
                    TempData["ErrorMsg"] = "Please enter your present address";
                }
                else if (string.IsNullOrEmpty(user.FathersName))
                {
                    ModelState.AddModelError("FathersName", "Please enter your father name ");
                    TempData["ErrorMsg"] = "Please enter your father name";
                }
                else if (string.IsNullOrEmpty(user.motherName))
                {
                    ModelState.AddModelError("motherName", "Please enter your mother name");
                    TempData["ErrorMsg"] = "Please enter your mother name";
                }
                else if (string.IsNullOrEmpty(user.ContactNumber))
                {
                    ModelState.AddModelError("ContactNumber", "Please enter your contact number");
                    TempData["ErrorMsg"] = "Please enter your contact number";
                }
                
                if (ModelState.IsValid)
                {
                    bool st = false;
                    string fileName = "";
                    if (file != null)
                    {
                        string extensiom = Path.GetExtension(file.FileName);
                        if (extensiom.ToLower() == ".jpg" || extensiom.ToLower() == ".png"
                            || extensiom.ToLower() == ".jpeg" || extensiom.ToLower() == ".gif"
                            )
                        {
                            fileName = user.Email + extensiom;
                            string path = Server.MapPath("~/ProfileImage/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                                file.SaveAs(path + fileName);
                            }
                            else
                            {
                                file.SaveAs(path + fileName);
                            }
                            if (string.IsNullOrEmpty(user.NationalID))
                            {
                                user.NationalID = "";
                            }
                            user.profileImage = fileName;
                            st = user.updateUser(user, user.id);
                            if (st)
                            {
                                TempData["successMsg"] = "User Created successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "User can not be created";
                                return View(user);
                            }

                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Please add an image for profile picture";
                            return View(user);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.NationalID))
                        {
                            user.NationalID = "";
                        }
                        user.profileImage = fileName;
                        st = user.updateUser(user, user.id);
                        if (st)
                        {
                            TempData["successMsg"] = "User updated successfully";
                            return RedirectToAction("list");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "User can not be updated";
                            return View(user);
                        }
                    }
                }

                return View(user);

            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
                return View(user);
            }
        }

        public ActionResult Actions(string userId, string action)
        {
            try
            {
                bool st = false;
                users user = new users();
                userId = AppSupportLibraryManager.DecryptString(userId);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("list");
                }
                else
                {
                    switch (action)
                    {
                        case "Active":
                        {
                            st = user.ActiveUser(userId);
                            if (st)
                            {
                                TempData["successMsg"] = "User Activated successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "User can not be Activated";
                                return RedirectToAction("list");
                            }
                        }
                            break;
                        case "Deactivate":
                        {
                            st = user.DeactivateUser(userId);
                            if (st)
                            {
                                TempData["successMsg"] = "User Deactivated successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "User can not be Deactivated";
                                return RedirectToAction("list");
                            }
                        }
                            break;
                        case "Delete":
                        {
                            st = user.deleteUser(userId);
                            if (st)
                            {
                                TempData["successMsg"] = "User Deleted successfully";
                                return RedirectToAction("list");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "User can not be Deleted";
                                return RedirectToAction("list");
                            }
                        }
                            break;
                        default:
                        {
                            return RedirectToAction("list");
                        }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.ToString();
                return RedirectToAction("list");
            }
            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult detailsList()
        {
            users user = new users();
            List<users> userList = user.userslist.ToList();
            return View(userList);
        }

        public ActionResult Details(string userId)
        {
            userId = AppSupportLibraryManager.DecryptString(userId);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMsg"] = "No data found";
                return RedirectToAction("list");
            }
            else
            {
                users user = new users();
                DataTable dt = new DataTable();
                dt = user.getUserDetailByID(userId);
                if (dt.Rows.Count > 0)
                {
                    user.id = userId;
                    user.Name = dt.Rows[0]["Name"].ToString();
                    user.Email = dt.Rows[0]["Email"].ToString();

                    user.DOB = dt.Rows[0]["DobUpdateFormat"].ToString();
                    user.Gender = dt.Rows[0]["Gender"].ToString();
                    user.ContactNumber = dt.Rows[0]["ContactNumber"].ToString();
                    user.nationality = dt.Rows[0]["Nationality"].ToString();
                    user.bloodGroup = dt.Rows[0]["BloodGroup"].ToString();
                    user.perManentAdd = dt.Rows[0]["permanentAdd"].ToString();
                    user.presentAdd = dt.Rows[0]["presentAdd"].ToString();
                    user.NationalID = dt.Rows[0]["nationalID"].ToString();
                    user.FathersName = dt.Rows[0]["FathersName"].ToString();
                    user.motherName = dt.Rows[0]["mothersName"].ToString();
                    user.userRole = dt.Rows[0]["userGroup"].ToString();
                    user.profileImage = dt.Rows[0]["profilePicName"].ToString();

                    return View(user);
                }
                else
                {
                    TempData["ErrorMsg"] = "No data found";
                    return RedirectToAction("list");
                }
            }
        }
    }
}
