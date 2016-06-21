using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppSupport.Tech;

namespace SchoolManagementSystem.Models
{
    public class ClassRoutineModel
    {
        public string RoutineId { get; set; }
        public string classId { get; set; }
        public string SectionId { get; set; }
        public string SubjectId { get; set; }
        public string day { get; set; }
        public string strtTime { get; set; }
        public string endTime { get; set; }
        public string createdBy { get; set; }
        public string createdForm { get; set; }
        public string createdDate { get; set; }
        public string SelectedClass { get; set; }
        public string SeletedSection { get; set; }
        public string SelectedSubject { get; set; }

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

        internal bool createClassRoutine(ClassRoutineModel classRoutineModel)
        {
            bool st = false;
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@classId", classRoutineModel.SelectedClass);
                db.AddParameters("@sectionId", classRoutineModel.SeletedSection);
                db.AddParameters("@subjectId", classRoutineModel.SelectedSubject);
                db.AddParameters("@day", classRoutineModel.day);
                db.AddParameters("@classStartTime", classRoutineModel.strtTime);
                db.AddParameters("@classEndTime", classRoutineModel.endTime);
                db.AddParameters("@createdBy", AppSupportSessionManager.Get("UserId").ToString());
                db.AddParameters("@createdForm", AppSupportLibraryManager.Terminal());
                db.AddParameters("@createdDate", DateTime.Today);

                db.ExecuteNonQuery("ADD_CLASS_ROUTINE", true);

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