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
        Boolean flag, flag1;

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
            int[] results = st.Login();

            if (results[1] == 0)
            {
                return RedirectToAction("changePassword");
            }
            if (results[1] == 1)
            {
                return RedirectToAction("Dashboard");
            }
            if (results[2] == 2)
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
        public ActionResult changePassword(LoginPage st)
        {
            flag1 = st.ChangePassword();
            if (flag1 == true)
                return RedirectToAction("Profiles");
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

            training_management_systemEntities entities = new training_management_systemEntities();

            manager man = new manager();
            ViewBag.data = man.manager_name;


            if (Session["user_id"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Profiles(LoginPage profile)
        {

            training_management_systemEntities entities = new training_management_systemEntities();

            manager man = new manager();
            ViewBag.data = man.manager_name;


            Boolean flag = profile.UpdateProfile();
            if (flag == true)
                return RedirectToAction("Index");
            else
                return RedirectToAction("error");
           


            return View();
        }
        public ActionResult routetodash()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult reg()
        {
            ViewBag.Message = "You have registered for this course successfully...";
            return View("Dashboard");
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

        }

    }
}