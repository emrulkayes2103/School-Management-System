using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Script;
using AppSupport.Tech;

namespace SchoolManagementSystem.Models
{
    public class TeacherModel
    {
        public string teacherId { get; set; }
        public string teacherName { get; set; }
        public string teacherEmail { get; set; }
       // public string teacherBloodGroup { get; set; }
        //public string teacherNationality { get; set; }
        public string teacherDob { get; set; }
        //public string teacherNationalId { get; set; }
        public string teacherGender { get; set; }
        public string teacherContuctNumber { get; set; }
        public string teacherPermanentAdd { get; set; }
        public string teacherPresentAdd { get; set; }
        public string teacherpassword { get; set; }
        public string reTypeTeacherPass { get; set; }
        public string userRole { get; set; }
        public string proPicname { get; set; }
        public string isActive { get; set; }
        public string isDeleted { get; set; }
        public string createdBy { get; set; }
        public string createdForm { get; set; }
        public string createdDate { get; set; }


        public IEnumerable<TeacherModel> Teachers
        {
            get
            {
                List<TeacherModel> teacherList = new List<TeacherModel>();
                try
                {
                    DataTable dt = new DataTable();
                    DBplayer db = new DBplayer();
                    db.Start();
                    dt = db.ExecuteDataTable("GET_ALL_TEACHERS",false);
                    if (dt.Rows.Count>0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TeacherModel teacher = new TeacherModel();
                            teacher.teacherId = dt.Rows[i]["TeacherId"].ToString();
                            teacher.teacherName = dt.Rows[i]["TeacherName"].ToString();
                            teacher.teacherEmail = dt.Rows[i]["TeacherEmail"].ToString();
                            teacher.teacherDob = dt.Rows[i]["dob"].ToString();
                            teacher.teacherGender = dt.Rows[i]["Gender"].ToString();
                            teacher.teacherPermanentAdd = dt.Rows[i]["PermanentAdd"].ToString();
                            teacher.teacherPresentAdd = dt.Rows[i]["presentAddr"].ToString();
                            teacher.teacherContuctNumber = dt.Rows[i]["ContactNumber"].ToString();
                            teacher.proPicname = dt.Rows[i]["ProPicName"].ToString();
                            teacher.userRole = dt.Rows[i]["UserRole"].ToString();
                            teacher.isActive = dt.Rows[i]["isActive"].ToString();
                            teacher.isDeleted = dt.Rows[i]["isDeleted"].ToString();
                            teacher.createdBy = dt.Rows[i]["CreatedBy"].ToString();
                            teacher.createdForm = dt.Rows[i]["CreatedForm"].ToString();
                            teacher.createdDate = dt.Rows[i]["CreatedDate"].ToString();

                            teacherList.Add(teacher);
                        }
                    }
                    db.Stop();
                }
                catch (Exception)
                {
                    
                    throw;
                }
                return teacherList;
            }
        }

        //public List<TeacherModel> TeachersList
        //{
        //    get
        //    {
        //        List<TeacherModel> teacherList = new List<TeacherModel>();
        //        try
        //        {
        //            DataTable dt = new DataTable();
        //            DBplayer db = new DBplayer();
        //            db.Start();
        //            dt = db.ExecuteDataTable("GET_ALL_TEACHERS",false);
                    
        //            db.Stop();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    TeacherModel teacherData = new TeacherModel();
        //                    teacherData.teacherId = dt.Rows[i]["TeacherId"].ToString();
        //                    teacherData.teacherName = dt.Rows[i]["TeacherName"].ToString();

        //                    teacherList.Add(teacherData);
        //                }
        //            }
        //        } 
        //        catch (Exception)
        //        {
                    
        //            throw;
        //        }
        //        return teacherList;
        //    }   
        //}

        internal bool CreateTeacher(TeacherModel teachers)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@teacherName", teachers.teacherName.Trim());
                db.AddParameters("@teacherEmail", teachers.teacherEmail.Trim());
                db.AddParameters("@teacherDob", AppSupportLibraryManager.ParseAppDateTime(teachers.teacherDob));
                db.AddParameters("@password", AppSupportLibraryManager.EncryptSHA1hash(teachers.teacherpassword.Trim()));
                db.AddParameters("@gender", teachers.teacherGender.Trim());
                db.AddParameters("@permanentAdd", teachers.teacherPermanentAdd.Trim());
                db.AddParameters("@presentAdd", teachers.teacherPresentAdd.Trim());
                db.AddParameters("@contactNumber", teachers.teacherContuctNumber.Trim());
                db.AddParameters("@proPicName", teachers.proPicname.Trim());
                db.AddParameters("@userRole", "Teacher");
                db.AddParameters("@isActive", "Yes");
                db.AddParameters("@isDeleted", "No");
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createdForm", AppSupportLibraryManager.Terminal());
                db.AddParameters("@createdDate", DateTime.Today);


                db.ExecuteNonQuery("CREATE_TEACHER", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        internal bool UpdateTeacher(TeacherModel teachers)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@teacherId",AppSupportLibraryManager.DecryptString(teachers.teacherId));
                db.AddParameters("@teacherName", teachers.teacherName.Trim());      
                db.AddParameters("@teacherDob", AppSupportLibraryManager.ParseAppDateTime(teachers.teacherDob));
                
                db.AddParameters("@gender", teachers.teacherGender.Trim());
                db.AddParameters("@permanentAdd", teachers.teacherPermanentAdd.Trim());
                db.AddParameters("@presentAdd", teachers.teacherPresentAdd.Trim());
                db.AddParameters("@contactNumber", teachers.teacherContuctNumber.Trim());
                db.AddParameters("@proPicName", teachers.proPicname.Trim());

                db.ExecuteNonQuery("UPDATE_TEACHER_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        internal bool actiateTeacherById(string teacherId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@teacherId", teacherId.Trim());
                db.ExecuteNonQuery("ACTIAVTE_TEACHER_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        internal bool DeactiateTeacherById(string teacherId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@teacherId", teacherId.Trim());
                db.ExecuteNonQuery("DEACTIAVTE_TEACHER_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {

                throw;
            }
            return st;
        }

        internal bool DeleteTeacherById(string teacherId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@teacherId", teacherId.Trim());
                db.ExecuteNonQuery("DELETE_TEACHER_BY_ID", true);
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