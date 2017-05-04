using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;


namespace SmsMisWeb.Controllers
{
    public partial class ClientController : Controller
    {
        // GET: Console
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Client Login";
            ViewBag.Title = "Client Console";

            return View();
        }

        [HttpPost]
        public ActionResult Index(cltUsers client)
        {
            //if (ModelState.IsValid)
            //{
            //    cltUsers oUser = new ClientContext().performLogin(client);
            //    if (oUser.UserID > 0)
            //    {
            //        // Added by Shah: 2014-09-08
            //        Session["CompanyCode"] = oUser.CompanyCode;
            //        Session["BranchCode"] = oUser.BranchCode;
            //        Session["User"] = oUser.UserID;
            //        hdlCommon HD = new hdlCommon();
            //        Session["Links"] = HD.getClientLinks(oUser.UserID.ToString());

            //        return RedirectToAction("Home");
            //    }
            //    else
            //    {
            //        ViewBag.errorMsg = "Invalid username or password";
            //    }
            //}
            return Index();
        }
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
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Student()
        {
            return View();
        }
        public ActionResult Bank()
        {
            return View();
        }
        public ActionResult FeeBillPaymentTerms()
        {
            return View();
        }

        public ActionResult StudentFixedScholarship()
        {
            return View();
        }
        public ActionResult StudentBillingCycle()
        {
            return View();
        }
        public ActionResult StudentFeeTermScholarship()
        {
            return View();
        }
        public ActionResult FeeTerm()
        {
            return View();
        }
        public ActionResult FeeParticularRate()
        {
            return View();
        }

        public ActionResult VoucherType()
        {
            return View();
        }
        public ActionResult BranchVoucherType()
        {
            return View();
        }
        public ActionResult InstrumentType()
        {
            return View();
        }
        public ActionResult InstrumentSerial()
        {
            return View();
        }
        public ActionResult ChartOfAccounts()
        {
            return View();
        }
        public ActionResult VoucherMaster()
        {
            return View();
        }
        public ActionResult SubjectPracticleFee()
        {
            return View();
        }
        public ActionResult StudentOptionalFeeParticular()
        {
            return View();
        }
        public ActionResult FeeBillMaster()
        {
            return View();
        }
        public ActionResult Currency()
        {
            return View();
        }
        public ActionResult Employee()
        {
            return View();
        }
        public ActionResult StudentAdmission()
        {
            return View();
        }

        public ActionResult ClassListWithSnap()
        {
            return View();
        }
    }
}


