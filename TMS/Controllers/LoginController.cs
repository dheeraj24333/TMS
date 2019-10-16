using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Models;

namespace TMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        LoginPage st;
        Boolean flag,flag1;
       
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {

           
            return View();
        }
        [HttpPost]

        public ActionResult Index(LoginPage st)
        {
            int[] results=new int[2];
            flag = st.Login();
            
            if (flag == true)
            {
                return RedirectToAction("changePassword");
            }
            if ( flag == false)
            {
                return RedirectToAction("Dashboard");
            }
           if(results[1]==2)
            {
                return RedirectToAction("error");
            }

            return View();
        }
        [HttpGet]
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changePassword( LoginPage st)
        {
             flag1 = st.ChangePassword();
            if (flag1 == true)
               return  RedirectToAction("Profiles");
            else
               return RedirectToAction("error");
            
            return View();
        }
        
        public ActionResult error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profiles()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Profiles(LoginPage profile)
        {
            Boolean flag = profile.UpdateProfile();
            if (flag == true)
                return RedirectToAction("Index");
            else
                return RedirectToAction("error");
            /*      string FileName = Path.GetFileNameWithoutExtension(profile.ImageFile.FileName);

                  //To Get File Extension  
                  string FileExtension = Path.GetExtension(profile.ImageFile.FileName);

                  //Add Current Date To Attached File Name  
                  FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                  //Get Upload path from Web.Config file AppSettings.  
                  string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

                  //Its Create complete path to store in server.  
                  profile.ImagePath = UploadPath + FileName;

                  //To copy and save file into server.  
                  profile.ImageFile.SaveAs(profile.ImagePath);

                  //profile.ImageFile=
                  int Session_id = 0;
                      if (Session["user_id"] != null)
                      {
                          Session_id = Convert.ToInt32(Session["user_id"].ToString());

                      }
                      training_management_systemEntities entities = new training_management_systemEntities();
                      company_employee emp = entities.company_employee.FirstOrDefault(e => e.user_id == Session_id);

                      emp.Photograph = profile.ImagePath;
                      entities.SaveChanges();
                  }

              else
              {
                  return RedirectToAction("error");
              }*/


        
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session.Abandon();
                Session.Clear();

                return RedirectToAction("Index");
            }
            return View();
        }

    }
}