using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppSupport.Tech;
using System.Web.Mvc;
using System.Data;

namespace SchoolManagementSystem.Models
{
    public class UserGroupModel
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string groupDesc { get; set; }
        public string isActive { get; set; }
        public string isDeleted { get; set; }

        public string errorMsgTitle { get; set; }
        public string SuccessMsg { get; set; }

        public IEnumerable<UserGroupModel> usergroup 
        {
            get
            {
                try
                {
                    List<UserGroupModel> usergroupList = new List<UserGroupModel>();

                    DBplayer db = new DBplayer();
                    db.Start();
                    DataTable dt = new DataTable();
                    dt = db.ExecuteDataTable("GET_USER_GROUP_LIST", true);

                    if(dt.Rows.Count>0)
                    {
                        for(int i=0; i< dt.Rows.Count;i++)
                        {
                            UserGroupModel userGroup = new UserGroupModel();
                            userGroup.groupId = AppSupportLibraryManager.EncryptString(dt.Rows[i]["UserGroupId"].ToString());
                            userGroup.groupName = dt.Rows[i]["UserGroupName"].ToString();
                            userGroup.groupDesc = dt.Rows[i]["Description"].ToString();
                            userGroup.isActive = dt.Rows[i]["IsActive"].ToString();
                            userGroup.isDeleted = dt.Rows[i]["IsDeleted"].ToString();

                            usergroupList.Add(userGroup);
                        }
                    }
                    db.Stop();

                    return usergroupList;
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
        public IEnumerable<UserGroupModel> activeusergroup
        {
            get
            {
                try
                {
                    List<UserGroupModel> usergroupList = new List<UserGroupModel>();

                    DBplayer db = new DBplayer();
                    db.Start();
                    DataTable dt = new DataTable();
                    dt = db.ExecuteDataTable("GET_ACTIVE_USER_GROUP_LIST", true);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UserGroupModel userGroup = new UserGroupModel();
                            userGroup.groupId = dt.Rows[i]["UserGroupId"].ToString();
                            userGroup.groupName = dt.Rows[i]["UserGroupName"].ToString();
                            userGroup.groupDesc = dt.Rows[i]["Description"].ToString();
                            userGroup.isActive = dt.Rows[i]["IsActive"].ToString();
                            userGroup.isDeleted = dt.Rows[i]["IsDeleted"].ToString();

                            usergroupList.Add(userGroup);
                        }
                    }
                    db.Stop();

                    return usergroupList;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public bool CreateUserGroup() 
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@userGroupName", this.groupName.Trim());
                db.AddParameters("@description", this.groupDesc.Trim());
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createtedDate", DateTime.Today);
                db.AddParameters("@createForm", AppSupportLibraryManager.Terminal());

                db.ExecuteNonQuery("CREATE_USER_GROUP", true);

                st = true;

                db.Stop();
            }
            catch(Exception)
            {
                throw;
            }
            return st;
        }
        public DataTable userGroupList()
        {
            DataTable dt = null;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                dt = db.ExecuteDataTable("GET_USER_GROUP_LIST", true);

                db.Stop();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable getGroupDetailsByGroupId(string groupId)
        {
            DataTable dt = null;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@UserGroupId", groupId.Trim());

                dt = db.ExecuteDataTable("GET_USER_GROUP_DETAILS_BY_ID", true);
                db.Stop();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }


        internal bool updateUserGroupByGroupId(string groupId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@userGroupId", groupId.Trim());
                db.AddParameters("@userGroupName", this.groupName.Trim());
                db.AddParameters("@description", this.groupDesc.Trim());
                db.ExecuteNonQuery("UPDATE_USER_GROUP_BY_ID", true);

                st = true;
                db.Stop();
            }
            catch(Exception)
            {
                throw;
            }
            return st;
        }

      

        internal bool deleteUserGroupById(string groupId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@usergroupId",groupId.Trim());
                db.ExecuteNonQuery("DELETE_USER_GROUP_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        internal bool activateUserGroupById(string groupId)
        {
            bool st = false;
            try
            {
                
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@userGroupId", groupId.Trim());
                string query_command = "update UserGroup set IsActive='Yes' where UserGroupId=@userGroupId";
                db.ExecuteNonQuery(query_command);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }
        internal bool DeactivateUserGroupById(string userGroupId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@userGroupId", userGroupId.Trim());

                string query_command = "update UserGroup set IsActive='No' where UserGroupId=@userGroupId";
                db.ExecuteNonQuery(query_command);
                db.Stop();
                st = true;
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }
    }
}