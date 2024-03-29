﻿using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;

using IdentityModel.Client;
using System.ComponentModel;

namespace TMS.Models
{
    public class LoginPage
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Select Manager")]
        public string ManagerId { get; set; }


        public int manager_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string manager_name { get; set; }


        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [DisplayName("Date of joining")]
        [DataType(DataType.Date)]
        public DateTime Date_Of_Joining { get; set; }






        Boolean flag1, flag;
        public int[] Login()
        {
            int[] results = { 0, 1, 1 };
            flag = false;
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.username == UserName & e.password == Password);


            if (emp != null)
            {
                HttpContext.Current.Session["user_id"] = emp.user_id;
                HttpContext.Current.Session["username"] = emp.username;
                if (emp.password == "12345")
                {
                    results[1] = 0;
                    flag = true;
                }
                else
                    results[1] = 1;

            }
            else
            {
                flag = false;
                results[2] = 2;
            }
            return results;
        }


        public Boolean ChangePassword()
        {

            flag1 = false;
            int Session_id = 0;
            if (HttpContext.Current.Session["user_id"] != null)
            {
                Session_id = Convert.ToInt32(HttpContext.Current.Session["user_id"].ToString());

            }
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.user_id == Session_id);

            emp.password = Password;
            entities.SaveChanges();
            if (emp.password != "12345")
                flag1 = true;




            return flag1;
        }


        public Boolean UpdateProfile()
        {
            Boolean flag2 = false;
            int Session_id = 0;
            if (HttpContext.Current.Session["user_id"] != null)
            {
                Session_id = Convert.ToInt32(HttpContext.Current.Session["user_id"].ToString());

            }
            training_management_systemEntities entities = new training_management_systemEntities();
            company_employee emp = entities.company_employee.SingleOrDefault(e => e.user_id == Session_id);
            emp.user_full_name = FullName;
            emp.user_title = Title;
            emp.user_department = Department;
            emp.manager_id = Convert.ToInt32(ManagerId);
            emp.date_of_joining = Date_Of_Joining;


            entities.SaveChanges();
            if (emp != null)
                flag2 = true;

            return flag2;

        }
    }
}