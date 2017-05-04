using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Fee;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Handlers;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        //public ContentResult getCompanyCode(int CompanyCode)
        //{
        //    DataTable dt = Functions.getDataTable("");
        //    return Content(JsonConvert.SerializeObject(dt, Formatting.None), "text/json");
        //}
        public JsonResult getAllBankofCompany(int CompanyCode)
        {
            return Json(new hdlCommonMethods().SelectByCompany(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        #region ShahZeb Common

        //public ContentResult getAllActiveCompanies()
        //{
        //    return Content(JsonConvert.SerializeObject(new hdlCommonMethods().SelectAllActiveCompany(), Formatting.None), "text/json");
        //}
        //public ContentResult getAllCompanies()
        //{
        //    return Content(JsonConvert.SerializeObject(new hdlCommonMethods().SelectAllCompanies(), Formatting.None), "text/json");
        //}

        //public JsonResult getAllSessions()
        //{
        //    return Json(new hdlCommonMethods().SelectAllSessions(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveSessions()
        //{
        //    return Json(new hdlCommonMethods().SelectActiveSessions(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult getAllActiveBranches()
        //{
        //    return Json(new hdlCommonMethods().SelectAllActiveBranches(), JsonRequestBehavior.AllowGet);
        //}
        
        //public JsonResult getAllBranches()
        //{
        //    return Json(new hdlCommonMethods().SelectAllBranches(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult getAllActiveClassesByCompany(int CompanyCode)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlClass().SelectAllActive(CompanyCode), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllClasses()
        //{
        //    return Json(new hdlCommonMethods().SelectAllClasses(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveClasses()
        //{
        //    return Json(new hdlCommonMethods().SelectAllActiveClasses(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllClassesWithCompanyAndBranch()
        //{
        //    return Json(new hdlCommonMethods().SelectAllClassWithCompanyAndBranch(), JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult getAllCourses()
        //{
        //    return Json(new hdlCommonMethods().SelectAllCourses(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveCourses()
        //{
        //    return Json(new hdlCommonMethods().SelectAllActiveCourses(), JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult getAllSections()
        //{
        //    return Json(new hdlCommonMethods().SelectAllSecions(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveSections()
        //{
        //    return Json(new hdlCommonMethods().SelectAllActiveSections(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult getAllSubjects()
        //{
        //    return Json(new hdlCommonMethods().SelectAllSubjects(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveSubjects()
        //{
        //    return Json(new hdlCommonMethods().SelectAllActiveSubjects(), JsonRequestBehavior.AllowGet);
        //}
        public ContentResult getAllActiveCompanies()
        {
            return Content(JsonConvert.SerializeObject(new hdlCommonMethods().SelectAllActiveCompany(), Formatting.None), "text/json");
        }
        public ContentResult getAllCompanies()
        {
            return Content(JsonConvert.SerializeObject(new hdlCommonMethods().SelectAllCompanies(), Formatting.None), "text/json");
        }

        public JsonResult getAllSessions()
        {
            return Json(new hdlCommonMethods().SelectAllSessions(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveSessions()
        {
            return Json(new hdlCommonMethods().SelectActiveSessions(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllActiveBranches()
        {
            return Json(new hdlCommonMethods().SelectAllActiveBranches(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllBranches()
        {
            return Json(new hdlCommonMethods().SelectAllBranches(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllClasses()
        {
            return Json(new hdlCommonMethods().SelectAllClasses(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveClasses()
        {
            return Json(new hdlCommonMethods().SelectAllActiveClasses(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getActiveClassesCompanyWise(int CompanyCode)
        {
            return Json(new hdlCommonMethods().SelectAllActiveClasses(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllClassesWithCompanyAndBranch()
        {
            return Json(new hdlCommonMethods().SelectAllClassWithCompanyAndBranch(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getAllCourses()
        {
            return Json(new hdlCommonMethods().SelectAllCourses(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveCourses()
        {
            return Json(new hdlCommonMethods().SelectAllActiveCourses(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult getAllSections()
        {
            return Json(new hdlCommonMethods().SelectAllSecions(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveSections()
        {
            return Json(new hdlCommonMethods().SelectAllActiveSections(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllSubjects()
        {
            return Json(new hdlCommonMethods().SelectAllSubjects(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveSubjects()
        {
            return Json(new hdlCommonMethods().SelectAllActiveSubjects(), JsonRequestBehavior.AllowGet);
        }
        public ContentResult getAllActiveStudentSubjects()
        {
            return Content(JsonConvert.SerializeObject(new hdlCommonMethods().SelectAllStudentActiveSubjects(), Formatting.None), "text/json");
            //return Json(new hdlCommonMethods().SelectAllActiveSubjects(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllActiveBranchesCompanyWise(int companyCode)
        {
            return Json(new hdlCommonMethods().SelectAllActiveBranches(companyCode), JsonRequestBehavior.AllowGet);
        }

        #endregion

        
        public JsonResult getAllActiveGrades()
        {
            return Json(new hdlCommonMethods().SelectAllActiveGrades(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveEmployee()
        {
            return Json(new hdlCommonMethods().SelectAllActiveEmployee(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveSessionsCompany(int CompanyCode)
        {
            return Json(new hdlCommonMethods().SelectActiveSessions(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        
    }
}