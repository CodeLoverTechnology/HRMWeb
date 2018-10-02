using HRMWeb.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMWeb.Controllers
{
    public class HomeController : Controller
    {
        private HRM_DBEntities db = new HRM_DBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login_Post(FormCollection frm)
        {
            string EmployeeID = frm["LoginID"].ToString();
            string Pwd = frm["Password"].ToString();
            if(Resources.HRMResources.AdminUser== EmployeeID && Resources.HRMResources.Pwd==Pwd)
            {
                Session["LoginUserID"] = EmployeeID;
                return RedirectToAction("Dashboard", "M_EmployeeMasters",null);
            }
            else
            {
                var EmployeeDetails = db.M_EmployeeMasters.Where(x => x.EmployeeID == EmployeeID && x.Pwd == Pwd);
                if (EmployeeDetails.FirstOrDefault().EmployeeID == EmployeeID && EmployeeDetails.FirstOrDefault().Pwd==Pwd)
                {
                    Session["LoginUserID"] = EmployeeID;
                    return RedirectToAction("Dashboard", "M_EmployeeMasters", null);
                }
                else
                {
                    return View();
                }
            }
        }

        public ActionResult HRMLogOut()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Session["LoginUserID"] = null;
            //Session["PrName"] = null;
            return RedirectToAction("Login");
        }
    }
}