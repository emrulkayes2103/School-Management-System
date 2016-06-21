using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AppSupport.Tech;

namespace SchoolManagementSystem.Models
{
    public class ParentsModel
    {
        public string parentId { get; set; }
        public string parentName { get; set; }
        public string parentEmail { get; set; }
        public string parentPassword { get; set; }
        public string parentsReTypePass { get; set; }
        public string parentContactNumber { get; set; }
        public string parentAddress { get; set; }
        public string parentProfession { get; set; }
        public string isActive { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string createdForm { get; set; }
        public string userRole { get; set; }

        public IEnumerable<ParentsModel> Parents
        {
            get
            {
                List<ParentsModel> parentsList = new List<ParentsModel>();
                DataTable dt = new DataTable();
                DBplayer db = new DBplayer();
                db.Start();
                dt = db.ExecuteDataTable("GET_ALL_PARENTS", false);
                db.Stop();
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ParentsModel parents = new ParentsModel();
                        parents.parentId = dt.Rows[i]["ParentsId"].ToString();
                        parents.parentName = dt.Rows[i]["ParentsName"].ToString();
                        parents.parentEmail = dt.Rows[i]["ParentsEmail"].ToString();
                        parents.parentContactNumber = dt.Rows[i]["Phone"].ToString();
                        parents.parentAddress = dt.Rows[i]["Address"].ToString();
                        parents.parentProfession = dt.Rows[i]["Profession"].ToString();
                        parents.userRole = dt.Rows[i]["userRole"].ToString();
                        parents.isActive = dt.Rows[i]["isActive"].ToString();
                        parents.isDeleted = dt.Rows[i]["isDeleted"].ToString();
                        parents.createdBy = dt.Rows[i]["createdBy"].ToString();
                        parents.createdDate = dt.Rows[i]["createdDate"].ToString();
                        parents.createdForm = dt.Rows[i]["createdForm"].ToString();

                        parentsList.Add(parents);
                    }
                }
                return parentsList;
            }
        }

        public bool CreateParents(ParentsModel parents)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@parentsName", parents.parentName.Trim());
                db.AddParameters("@parentsEmail", parents.parentEmail.Trim());
                db.AddParameters("@parenstsPassword", AppSupportLibraryManager.EncryptSHA1hash(parents.parentPassword.Trim()));
                db.AddParameters("@phone", parents.parentContactNumber.Trim());
                db.AddParameters("@address", parents.parentAddress.Trim());
                db.AddParameters("@profession", parents.parentProfession.Trim());
                db.AddParameters("@userRole", "Parents");
                db.AddParameters("@isActive", "Yes");
                db.AddParameters("@isDeleted", "No");
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createdForm", AppSupportLibraryManager.Terminal());
                db.AddParameters("@createdDate", DateTime.Today);

                db.ExecuteNonQuery("ADD_PARENTS", true);
                st = true;

                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        public bool UpdateParentsById(ParentsModel parents)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@parentsId", AppSupportLibraryManager.DecryptString(parents.parentId.Trim()));
                db.AddParameters("@parentsName", parents.parentName.Trim());
                db.AddParameters("@phone", parents.parentContactNumber.Trim());
                db.AddParameters("@address", parents.parentAddress.Trim());
                db.AddParameters("@profession", parents.parentProfession.Trim());

                db.ExecuteNonQuery("UPDATE_PARENTS_BY_ID", true);
                st = true;

                db.Stop();
            }
            catch (Exception)
            {

                throw;
            }
            return st;
        }

        public bool DeleteParentsById(string id)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@parentsId", id.Trim());
                db.ExecuteNonQuery("DELTE_PARENTS_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }
    }
}