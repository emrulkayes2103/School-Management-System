using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSupport.Tech;

namespace SchoolManagementSystem.Models
{
    public class SubjectModel
    {
        public string subjectId { get; set; }
        public string subjectName { get; set; }
        public string className { get; set; }
        public string teacherName { get; set; }
        public string SelectedClassId { get; set; }
        public string selectedTeacherId { get; set; }
        public string createdBy { get; set; }
        public string createdForm { get; set; }
        public string craetedDate { get; set; }

        public IEnumerable<SubjectModel> Subjects
        {
            get
            {
                List<SubjectModel> subjectList = new List<SubjectModel>();

                try
                {
                    DataTable dt = new DataTable();
                    DBplayer db = new DBplayer();
                    db.Start();
                    dt = db.ExecuteDataTable("GET_ALL_SUBJECTS", false);
                    db.Stop();
                    if (dt.Rows.Count >0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SubjectModel subjectModel = new SubjectModel();
                            subjectModel.subjectId = dt.Rows[i]["subjectId"].ToString();
                            subjectModel.subjectName = dt.Rows[i]["subjectName"].ToString();
                            subjectModel.className = dt.Rows[i]["className"].ToString();
                            subjectModel.SelectedClassId = dt.Rows[i]["classId"].ToString();
                            subjectModel.teacherName = dt.Rows[i]["teacherName"].ToString();
                            subjectModel.selectedTeacherId = dt.Rows[i]["teacherId"].ToString();
                            subjectModel.createdBy = dt.Rows[i]["createdBy"].ToString();
                            subjectModel.createdForm = dt.Rows[i]["createdForm"].ToString();
                            subjectModel.craetedDate = dt.Rows[i]["createdDate"].ToString();

                            subjectList.Add(subjectModel);
                        }
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                return subjectList;
            }
        }

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
                        Selected = (teachers.teacherId == this.selectedTeacherId)
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
                            Selected = (classLists.classId == this.SelectedClassId)
                        });
                }
                return classDropDwnList;
            }
        }

        internal bool createSubject(SubjectModel subjectModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@subjectName", subjectModel.subjectName.Trim());
                db.AddParameters("@classId", subjectModel.SelectedClassId.Trim());
                db.AddParameters("@teacherId", subjectModel.selectedTeacherId.Trim());
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createdForm", AppSupportLibraryManager.Terminal());
                db.AddParameters("@createdDate", DateTime.Today);

                db.ExecuteNonQuery("CREATE_SUBJECT", true);
                st = true;
                db.Stop();

            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }



        internal DataTable getSubjectListByClassId(string ClassId)
        {
            DataTable dt = new DataTable();
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@classId",ClassId);
                dt = db.ExecuteDataTable("GET_SUBJECTS_BY_CLASS_ID", true);
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return dt;
        }

        internal bool deleteSubjectById(string subjectId)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@subjectId",subjectId.Trim());

                db.ExecuteNonQuery("DELETE_SUBJECT_BY_ID", true);
                st = true;
                db.Stop();
            }
            catch (Exception)
            {
                
                throw;
            }
            return st;
        }

        internal bool UpdateSubjectbyId(SubjectModel subjectModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();

                db.AddParameters("@subjectId",subjectModel.subjectId.Trim());
                db.AddParameters("@subjectName", subjectModel.subjectName.Trim());
                db.AddParameters("@classId", subjectModel.SelectedClassId.Trim());
                db.AddParameters("@teacherId", subjectModel.selectedTeacherId.Trim());
                

                db.ExecuteNonQuery("UPDATE_SUBJECT_BY_ID", true);
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