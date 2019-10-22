using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Models;
using static System.Collections.Specialized.BitVector32;

namespace TMS.Controllers
{
    public class DashboardController : Controller
    {
        

        // GET: Dashboard
        public ActionResult Index()
        {
            
            
            training_management_systemEntities entities = new training_management_systemEntities();
            course_registration_view_model crvm;
            var model = entities.course_management
                .Join(entities.class_management, u => u.course_id, uir => uir.course_id, (u, uir) => new { u, uir })
                .Select(m => new course_registration_view_model
                {
                    course_id = m.u.course_id,
                    course_name = m.u.course_name,
                    course_type = m.u.course_type,
                    course_category = m.u.course_category,
                    course_duration = m.u.course_duration,
                    class_id = m.uir.class_id,
                    class_name = m.uir.class_name,
                    class_start_date = m.uir.class_start_date,
                    class_end_date = m.uir.class_end_date,
                    available_seats = m.uir.available_seats,
                    is_registration_active = m.uir.is_registration_active
                });

            List<TMS.Models.course_registration_view_model> lst = new List<course_registration_view_model>();

            foreach (var item in model)
            {
                crvm = new course_registration_view_model();
                crvm.course_id = item.course_id;
                crvm.course_name = item.course_name;
                crvm.course_type = item.course_type;
                crvm.course_category = item.course_category;
                crvm.course_duration = item.course_duration;
                crvm.class_id = item.class_id;
                crvm.class_name = item.class_name;
                crvm.class_start_date = item.class_start_date;
                crvm.class_end_date = item.class_end_date;
                crvm.available_seats = item.available_seats;
                crvm.is_registration_active = item.is_registration_active;
                lst.Add(crvm);
            }

            return View("Available_Courses",lst);
        }
        [HttpGet]
        public ActionResult Available_Courses()
        {


            return View();
        }
        public ActionResult Dashboard()
        {
            
                return RedirectToAction("Dashboard","Login");
            
        }
        public ActionResult routetodash()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Register(int id)
        {
            if (Session["user_id"] != null)
            {
                int usrId=0;
                int.TryParse(Session["user_id"].ToString(), out usrId);
                training_management_systemEntities entities = new training_management_systemEntities();
                course_registration course_Registration = new course_registration();

                var model = entities.company_employee
                    .Where(u => u.user_id == usrId)
                    .Select(m => new Registration_Conform
                    {
                        user_id = m.user_id,
                        manager_id = m.manager_id
                    });


                course_Registration.user_id = int.Parse(Session["user_id"].ToString());
                course_Registration.class_id = id;
                course_Registration.manager_id =  model.FirstOrDefault(). manager_id  ;
                course_Registration.status = "In Process";
                course_Registration.is_manager_approved = "No";
                course_Registration.is_admin_approved = "No";

                entities.course_registration.Add(course_Registration);

                entities.SaveChanges();


                return RedirectToAction("reg", "Login");
            }
            else {

                return RedirectToAction("Index" , "Login");
            }
        }










    }
}