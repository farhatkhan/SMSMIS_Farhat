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
using SmsMis.Models.Console.Client;

namespace SmsMisWeb.Controllers
{
    public partial class UIController : Controller
    {
        // GET: Console
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Admin Login";
            ViewBag.Title = "Administration Console";

            return View();
        }

        [HttpPost]
        public ActionResult Index(User admin)
        {
            if (ModelState.IsValid)
            {
                string AdminID = new SmsMisDB().performLogin(admin, "");
                if (!string.IsNullOrEmpty(AdminID))
                {
                    // Added by Shah: 2014-09-08
                    Session["isAdmin"] = true;
                    Session["User"] = AdminID;
                    hdlCommon HD = new hdlCommon();
                    Session["Links"] = HD.getLinks(AdminID.ToString());

                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.errorMsg = "Invalid username or password";
                }
            }
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
        public ActionResult User()
        {
            return View();
        }

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

        #region Copied

        //[HttpPost]
        //public ActionResult Index(cltUsers client)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    cltUsers oUser = new ClientContext().performLogin(client);
        //    //    if (oUser.UserID > 0)
        //    //    {
        //    //        // Added by Shah: 2014-09-08
        //    //        Session["CompanyCode"] = oUser.CompanyCode;
        //    //        Session["BranchCode"] = oUser.BranchCode;
        //    //        Session["User"] = oUser.UserID;
        //    //        hdlCommon HD = new hdlCommon();
        //    //        Session["Links"] = HD.getClientLinks(oUser.UserID.ToString());

        //    //        return RedirectToAction("Home");
        //    //    }
        //    //    else
        //    //    {
        //    //        ViewBag.errorMsg = "Invalid username or password";
        //    //    }
        //    //}
        //    return Index();
        //}
        ////public ActionResult Logout(admUser admin)
        ////{
        ////    if (Convert.ToBoolean(Session["isAdmin"]))
        ////    {
        ////        new SmsMisDB().performLogOut(Convert.ToInt32(Session["User"]), "");
        ////    }

        ////    return RedirectToAction("Index");
        ////}
        //public JsonResult buildParentMenu()
        //{
        //    return Json(Session["Links"], JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public ActionResult Home()
        //{
        //    return View();
        //}
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
        //public ActionResult InstrumentSerial()
        //{
        //    return View();
        //}
        public ActionResult InstrumentSerialMaster()
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
        public ActionResult GenerateFeeBill()
        {
            return View();
        }
        public ActionResult Currency()
        {
            return View();
        }
        public ActionResult StudentFixedDiscount()
        {
            return View();
        }
        public ActionResult InstrumentCancelled()
        {
            return View();
        }

        public ActionResult Project()
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

        public ActionResult AnalysisType() { return View(); }
        public ActionResult CostCenter() { return View(); }
        public ActionResult IssuedChallan() { return View(); }
        public ActionResult FeeReceipt() { return View(); }

        public ActionResult ExamType() { return View(); }
        public ActionResult SubjectTeacher() { return View(); }
        public ActionResult GradingCriteria() { return View(); }

        public ActionResult SubjectPapers() { return View(); }

        public ActionResult Party() { return View(); }

        public ActionResult COAReport() { return View(); }

        public ActionResult PrintVoucher() { return View(); }
        public ActionResult PrintJournal() { return View(); }
        public ActionResult BankPopup() { return View(); }
        public ActionResult InquiryList() { return View(); }

        public ActionResult StudentFixedDiscountType() { return View(); }
        public ActionResult COAGroup() { return View(); }
        public ActionResult CSMaster() { return View(); }

        public ActionResult GeneralLedger() { return View(); }
        public ActionResult TrialBalance() { return View(); }

        public ActionResult ItemBrand() { return View(); }
        public ActionResult ItemModel() { return View(); }
        public ActionResult ItemGrade() { return View(); }
        public ActionResult ItemCategory() { return View(); }
        public ActionResult MeasuringUnit() { return View(); }


        

        #endregion
    }
}


