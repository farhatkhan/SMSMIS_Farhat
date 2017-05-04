using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Fee;


namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        // GET: SubjectPracticleFee
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllSubjectPracticleFee()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlSubjectPracticleFee().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllCompanySubjectPracticleFee(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlSubjectPracticleFee().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveSubjectPracticleFee(IList<SubjectPracticleFee> SubjectPracticleFee, int CompanyCode, int BranchCode, int SessionCode, int ClassCode,int CourseCode)
        {
            try
            {
                new hdlSubjectPracticleFee().save(SubjectPracticleFee, Convert.ToString(Session["User"]), CompanyCode, BranchCode, SessionCode, ClassCode, CourseCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllSubjectPracticleFee();
        }

        [HttpPost]
        public JsonResult SubjectPracticleFee(IList<SubjectPracticleFee> SubjectPracticleFee)
        {
            try
            {
                new hdlSubjectPracticleFee().delete(SubjectPracticleFee);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllSubjectPracticleFee();
        }
    }
}