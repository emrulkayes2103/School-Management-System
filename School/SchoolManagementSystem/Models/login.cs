using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using AppSupport.Tech;

namespace SchoolManagementSystem.Models
{
    public class login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string errorMsg { get; set; }

        internal DataTable userLogIn(login logData)
        {
            DataTable dt = new DataTable();
            try
            {
                DBplayer db = new DBplayer();
                db.Start();
                db.AddParameters("@email", logData.Email.Trim());
                db.AddParameters("@pass", AppSupportLibraryManager.EncryptSHA1hash(logData.Password));

                dt = db.ExecuteDataTable("USER_LOGIN", true);
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