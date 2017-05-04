using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;

namespace SmsMisWeb.Controllers
{
    public partial class UIController : Controller
    {
        // GET: Console
        [HttpGet]
        //public ActionResult Index()
        //{
        //    ViewBag.PageTitle = "Admin Login";
        //    ViewBag.Title = "Administration Console";

        //    return View();
        //}

        [HttpPost]
        //public ActionResult Index(User admin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string AdminID = new SmsMisDB().performLogin(admin, "");
        //        if (!string.IsNullOrEmpty(AdminID))
        //        {
        //            // Added by Shah: 2014-09-08
        //            Session["isAdmin"] = true;
        //            Session["User"] = AdminID;
        //            hdlCommon HD = new hdlCommon();
        //            Session["Links"] = HD.getLinks(AdminID.ToString());

        //            return RedirectToAction("Home");
        //        }
        //        else
        //        {
        //            ViewBag.errorMsg = "Invalid username or password";
        //        }
        //    }
        //    return Index();
        //}
        public ActionResult Logout(admUser admin)
        {
            if (Convert.ToBoolean(Session["isAdmin"]))
            {
                new SmsMisDB().performLogOut(Convert.ToInt32(Session["User"]), "");
            }

            return RedirectToAction("Index");
        }
        public JsonResult buildParentMenu()
        {
            return Json(Session["Links"], JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult Home()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult User()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Company()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Branch()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CompanySession()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BranchSession()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Subject()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Class()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Course()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Section()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ClassCourseSubject()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Religion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SeatType()
        {
            return View();
        }

        [HttpGet]
        public ActionResult House()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MarketingReference()
        {
            return View();
        } 
        [HttpGet]
        public ActionResult FeeParticular()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Designation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Department()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BranchBuilding()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DocType()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Grade()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Category()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Type()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Nationality()
        {
            return View();
        }
    }
}


