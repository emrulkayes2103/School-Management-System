using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using AppSupport.Tech;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Models
{
    public class ClassModel
    {
        public string classId { get; set; }
        public string className { get; set; }
        public int classNumericNumber { get; set; }
        public string classTeacherId { get; set; }
        public string createdBy { get; set; }
        public string createdForm { get; set; }
        public string createdDate { get; set; }
        public string teacherSelected{ get; set; }


        public IEnumerable<ClassModel> Classes
        {
            get
            {
                List<ClassModel> classList = new List<ClassModel>();
                DataTable dt = new DataTable();
                DBplayer db = new DBplayer();
                db.Start();
                dt = db.ExecuteDataTable("GET_ALL_CLASS", false);
                db.Stop();
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ClassModel ListOfClass = new ClassModel();
                        ListOfClass.classId = dt.Rows[i]["ClassId"].ToString();
                        ListOfClass.className = dt.Rows[i]["ClassName"].ToString();
                        ListOfClass.classNumericNumber = Convert.ToInt32(dt.Rows[i]["ClassNumericNumber"].ToString());
                        ListOfClass.classTeacherId = dt.Rows[i]["classTeacherName"].ToString();
                        ListOfClass.teacherSelected = dt.Rows[i]["ClassTeacher"].ToString();
                        ListOfClass.createdBy = dt.Rows[i]["CreatedBy"].ToString();
                        ListOfClass.createdForm = dt.Rows[i]["CreatedForm"].ToString();
                        ListOfClass.createdDate = dt.Rows[i]["createdDate"].ToString();

                        classList.Add(ListOfClass);
                    }
                }
                return classList;
            }
        }

        public bool CreateClass(ClassModel classModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@className", classModel.className.Trim());
                db.AddParameters("@classNumaricNumber", classModel.classNumericNumber);
                db.AddParameters("@classTeacherId", classModel.classTeacherId);
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createdForm", AppSupportLibraryManager.Terminal());
                db.AddParameters("@createdDate", DateTime.Today);

                db.ExecuteNonQuery("CREATE_CLASS", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }



        public IEnumerable<SelectListItem> teacherDropDwnList()
        {

            TeacherModel teacher = new TeacherModel();
            //IEnumerable<SelectListItem> teacherList = teacher.Teachers.Select( t => new SelectListItem
            //{
            //    Text = t.teacherName,
            //    Value = AppSupportLibraryManager.EncryptString(t.teacherId),
            //    Selected = (t.teacherId == this.teacherSelected)
            //}) ;
            //return teacherList;

            //another process 
            List<SelectListItem> teacherList = new List<SelectListItem>();
            foreach (var teachers in teacher.Teachers)
            {
                teacherList.Add(new SelectListItem()
                {
                    Text = teachers.teacherName,
                    Value = teachers.teacherId,
                    Selected = (teachers.teacherId == this.teacherSelected)
                });
            }
            return teacherList;
        }
        //public IEnumerable<SelectListItem> teacherDropDwnList
        //{
        //    get
        //    {
        //        TeacherModel teacher = new TeacherModel();
        //        //IEnumerable<SelectListItem> teacherList = teacher.Teachers.Select( t => new SelectListItem
        //        //{
        //        //    Text = t.teacherName,
        //        //    Value = AppSupportLibraryManager.EncryptString(t.teacherId),
        //        //    Selected = (t.teacherId == this.teacherSelected)
        //        //}) ;
        //        //return teacherList;

        //        //another process 
        //        List<SelectListItem> teacherList = new List<SelectListItem>();
        //        foreach (var teachers in teacher.TeachersList)
        //        {
        //            teacherList.Add(new SelectListItem()
        //            {
        //                Text = teachers.teacherName,
        //                Value = teachers.teacherId,
        //                Selected = (teachers.teacherId == this.teacherSelected)
        //            });
        //        }
        //        return teacherList;
        //    }
        //}


        internal bool UpdateClass(ClassModel classModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@classId", AppSupportLibraryManager.DecryptString(classModel.classId));
                db.AddParameters("@className", classModel.className.Trim());
                db.AddParameters("@classNumaricNumber", classModel.classNumericNumber);
                db.AddParameters("@classTeacherId", classModel.classTeacherId);

                db.ExecuteNonQuery("UPDATE_CLASS_BY_CLASS_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {

                throw;
            }
            return st;
        }
        internal bool DeleteClassById(string classId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@classId", classId);


                db.ExecuteNonQuery("DELETE_CLASS_BY_ID", true);
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