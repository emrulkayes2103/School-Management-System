using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSupport.Tech;


namespace SchoolManagementSystem.Models
{
    public class SectionModel
    {
        public string sectionId { get; set; }
        public string SectionName { get; set; }
        public string SectionNickName { get; set; }
        //public string ClassID { get; set; }
        public string className { get; set; }
        public string SelectedClass { get; set; }
        public string TeacherName { get; set; }
        //public string TeacherId { get; set; }
        public string SelectedTeaher { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string createdForm { get; set; }

        public IEnumerable<SelectListItem> teacherList
        {
            get
            {
                List<SelectListItem> teacherDropDownList = new List<SelectListItem>();
                TeacherModel teacherModel = new TeacherModel();

                foreach (var teachers in teacherModel.Teachers)
                {
                   teacherDropDownList.Add(new SelectListItem()
                   {
                       Text = teachers.teacherName,
                       Value = teachers.teacherId,
                       Selected = (teachers.teacherId == this.SelectedTeaher)
                   });
                }
                return teacherDropDownList;
            }
        }

        public IEnumerable<SelectListItem> ClassList
        {
            get
            {
                List<SelectListItem> classDropDwnList = new List<SelectListItem>();
                ClassModel classModel = new ClassModel();
                foreach (var classLists in classModel.Classes)
                {
                    classDropDwnList.Add(
                        new SelectListItem()
                        {
                            Text = classLists.className,
                            Value = classLists.classId,
                            Selected = (classLists.classId == this.SelectedClass)
                        });
                }
                return classDropDwnList;
            }
        }

        public IEnumerable<SectionModel> Sections
        {
            get
            {
                List<SectionModel> sectionList = new List<SectionModel>();
                DBplayer db = new DBplayer();
                DataTable dt = new DataTable();

                db.Start();
                dt = db.ExecuteDataTable("GET_SECTIONS", true);
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       SectionModel section = new SectionModel();
                       section.sectionId = dt.Rows[i]["SectionId"].ToString();
                       section.SectionName = dt.Rows[i]["SectionName"].ToString();
                       section.SectionNickName = dt.Rows[i]["SectionNickName"].ToString();
                       section.SelectedTeaher = dt.Rows[i]["TeacherId"].ToString();
                       section.TeacherName = dt.Rows[i]["TeacherName"].ToString();
                       section.SelectedClass = dt.Rows[i]["ClassId"].ToString();
                       section.className = dt.Rows[i]["ClassName"].ToString();
                       section.createdBy = dt.Rows[i]["CreatedBy"].ToString();
                       section.createdDate = dt.Rows[i]["CreatedDate"].ToString();
                       section.createdForm = dt.Rows[i]["CreatedBy"].ToString();

                       sectionList.Add(section);
                    }
                }
                db.Stop();

                return sectionList;
            }
        }

        public bool CreateSection(SectionModel sectionModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@sectionName", sectionModel.SectionName);
                db.AddParameters("@sectionNickName", sectionModel.SectionNickName);
                db.AddParameters("@classId", sectionModel.SelectedClass);
                db.AddParameters("@TeacherId", sectionModel.SelectedTeaher);
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId"));
                db.AddParameters("@createdForm", AppSupportLibraryManager.Terminal());
                db.AddParameters("@createdDate", DateTime.Today);

                db.ExecuteNonQuery("CREATE_SECTION_FOR_CLASS", true);
                db.Stop();
                st = true;
            }
            catch (Exception)
            {
                throw;
            }
            return st;
        }

        public bool UpdateSectionById(SectionModel sectionModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@sectionId",sectionModel.sectionId);
                db.AddParameters("@sectionName", sectionModel.SectionName);
                db.AddParameters("@sectionNickName", sectionModel.SectionNickName);
                db.AddParameters("@classId", sectionModel.SelectedClass);
                db.AddParameters("@TeacherId", sectionModel.SelectedTeaher);

                db.ExecuteNonQuery("UPDATE_SECTION_BY_CLASS_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        public bool DeleteSectionById(string sectionId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@sectionId", sectionId);
                

                db.ExecuteNonQuery("DELETE_SECTION_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {

                throw;
            }
            return st;
        }

        internal DataTable getSectionsByClassId(string classId)
        {
            DataTable dt = new DataTable();
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@classId",classId.Trim());
                dt = db.ExecuteDataTable("GET_SECTION_BY_CLASS_ID", true);
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return dt;
        }
    }
}