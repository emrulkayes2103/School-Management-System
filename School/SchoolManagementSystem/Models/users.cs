using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AppSupport.Tech;

namespace SchoolManagementSystem.Models
{
    public class users
    {
        public string id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string DOB { get; set; }

        public string ContactNumber { get; set; }

        public string Gender { get; set; }

        public string bloodGroup { get; set; }

        public string nationality { get; set; }

        public string NationalID { get; set; }

        public string perManentAdd { get; set; }

        public string presentAdd { get; set; }

        public string FathersName { get; set; }

        public string motherName { get; set; }

        public string password { get; set; }

        public string userRole { get; set; }

        public string profileImage { get; set; }

        public string isActive { get; set; }

        public string createdBy { get; set; }

        public string createdForm { get; set; }

        public DateTime createdDate { get; set; }

        public string isDeleted { get; set; }

        public string errorMsg { get; set; }

        public string reTypePass { get; set; }

        public IEnumerable<users> userslist
        {
            get
            {
                List<users> users = new List<users>();
                try
                {
                    DBplayer db = new DBplayer();
                    DataTable dt = new DataTable();
                    db.Start();
                    dt = db.ExecuteDataTable("GET_ALL_USERS",false);
                    if (dt.Rows.Count>0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            users alllUser = new users();
                            alllUser.id = AppSupportLibraryManager.EncryptString(dt.Rows[i]["Serial"].ToString());
                            alllUser.Name = dt.Rows[i]["Name"].ToString();
                            alllUser.Email = dt.Rows[i]["Email"].ToString();
                            alllUser.DOB = dt.Rows[i]["DOB"].ToString();
                            //alllUser.password = dt.Rows[i]["Password"].ToString();
                            alllUser.Gender = dt.Rows[i]["Gender"].ToString();
                            alllUser.ContactNumber = dt.Rows[i]["ContactNumber"].ToString();
                            alllUser.nationality = dt.Rows[i]["Nationality"].ToString();
                            alllUser.bloodGroup = dt.Rows[i]["BloodGroup"].ToString();
                            alllUser.perManentAdd = dt.Rows[i]["permanentAdd"].ToString();
                            alllUser.presentAdd = dt.Rows[i]["presentAdd"].ToString();
                            alllUser.NationalID = dt.Rows[i]["nationalID"].ToString();
                            alllUser.profileImage = dt.Rows[i]["profilePicName"].ToString();
                            alllUser.FathersName = dt.Rows[i]["FathersName"].ToString();
                            alllUser.motherName = dt.Rows[i]["mothersName"].ToString();
                            alllUser.userRole = dt.Rows[i]["userRole"].ToString();
                            alllUser.isActive = dt.Rows[i]["isActive"].ToString();
                            alllUser.createdBy = dt.Rows[i]["createdBy"].ToString();
                            alllUser.createdForm = dt.Rows[i]["createdFrom"].ToString();
                            alllUser.createdDate = AppSupportLibraryManager.ParseAppDateTime(dt.Rows[i]["createdDate"].ToString());
                            alllUser.isDeleted = dt.Rows[i]["isDeleted"].ToString();
                            users.Add(alllUser);
                        }
                    }
                    db.Stop();
            
                }
                catch (Exception)
                {
                    
                    throw;
                }
                return users;
            }
        }

        internal bool createUsers(users user)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@Name", user.Name.Trim());
                db.AddParameters("@Email", user.Email.Trim());
                db.AddParameters("@DOB", AppSupportLibraryManager.ParseAppDateTime(user.DOB));
                db.AddParameters("@Password", AppSupportLibraryManager.EncryptSHA1hash(user.password.Trim()));
                db.AddParameters("@Gender", user.Gender.Trim());
                db.AddParameters("@ContactNumber", user.ContactNumber.Trim());
                db.AddParameters("@Nationality", user.nationality.Trim());
                db.AddParameters("@BloodGroup", user.bloodGroup.Trim());
                db.AddParameters("@permanentAdd", user.perManentAdd.Trim());
                db.AddParameters("@presentAdd", user.presentAdd.Trim());
                db.AddParameters("@nationalID", user.NationalID.Trim());
                db.AddParameters("@profilePicName", user.profileImage.Trim());
                db.AddParameters("@FathersName", user.FathersName.Trim());
                db.AddParameters("@mothersName", user.motherName.Trim());
                db.AddParameters("@userRole", "Super Admin");
                db.AddParameters("@isActive", "Yes");
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createdDate", DateTime.Today);
                db.AddParameters("@createdFrom", AppSupportLibraryManager.Terminal());
                db.AddParameters("@isDeleted", "No");

                //st = Convert.ToBoolean(db.ExecuteNonQuery("CREATE_USER", true));
                db.ExecuteDataTable("CREATE_USER", true);
                db.Stop();
                st = true;
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }

        internal DataTable CheckDuplicateEmail(string Email)
        {

            DataTable dt = new DataTable();
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@Email", Email.Trim());
                dt = db.ExecuteDataTable("DUPLICATE_EMAIL", true);
                db.Stop();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        internal bool updateUser(users user, string serial)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@Serial", serial.Trim());
                db.AddParameters("@Name", user.Name.Trim());
                //db.AddParameters("@Email", user.Email.Trim());
                db.AddParameters("@DOB", AppSupportLibraryManager.ParseAppDateTime(user.DOB));

                db.AddParameters("@Gender", user.Gender.Trim());
                db.AddParameters("@ContactNumber", user.ContactNumber.Trim());
                db.AddParameters("@Nationality", user.nationality.Trim());
                db.AddParameters("@BloodGroup", user.bloodGroup.Trim());
                db.AddParameters("@permanentAdd", user.perManentAdd.Trim());
                db.AddParameters("@presentAdd", user.presentAdd.Trim());
                db.AddParameters("@nationalID", user.NationalID.Trim());
                db.AddParameters("@profilePicName", user.profileImage.Trim());
                db.AddParameters("@FathersName", user.FathersName.Trim());
                db.AddParameters("@mothersName", user.motherName.Trim());
                db.AddParameters("@userRole", user.userRole.Trim());


                db.ExecuteDataTable("UPDATE_USER_BY_ID", true);
                db.Stop();
                st = true;
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }
        internal DataTable getUserDetailByID(string Serial)
        {
            DataTable dt = new DataTable();
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@Serial", Serial.Trim());

                dt = db.ExecuteDataTable("GET_USER_BY_ID", true);
                db.Stop();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }
        internal bool ActiveUser(string serial)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@Serial", serial.Trim());

                db.ExecuteNonQuery("ACTIVATE_USER_BY_SERAL", true);
                db.Stop();
                st = true;
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }
        internal bool DeactivateUser( string serial)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@Serial", serial.Trim());
                db.ExecuteNonQuery("DEACTIVATE_USER_BY_SERAL", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }
        internal bool deleteUser(string serial)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                string Command_Query = "update users set isDeleted='Yes',isActive='No' where Serial=@serial";
                db.AddParameters("@serial", serial.Trim());
                db.ExecuteNonQuery(Command_Query);
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