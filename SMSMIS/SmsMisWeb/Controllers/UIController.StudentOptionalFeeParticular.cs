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
        // GET: StudentOptionalFeeParticular

        public JsonResult getAllStudentOptionalFeeParticular(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentOptionalFeeParticular().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveStudentOptionalFeeParticular(IList<StudentOptionalFeeParticular> StudentOptionalFeeParticular, int CompanyCode, int BranchCode, int SessionCode, int StudentNo)
        {
            try
            {
                new hdlStudentOptionalFeeParticular().save(StudentOptionalFeeParticular, Convert.ToString(Session["User"]), CompanyCode, BranchCode, SessionCode, StudentNo);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentOptionalFeeParticular(CompanyCode);
        }

        [HttpPost]
        public JsonResult StudentOptionalFeeParticular(IList<StudentOptionalFeeParticular> StudentOptionalFeeParticular)
        {
            int companyCode =0;
            try
            {
                companyCode = StudentOptionalFeeParticular[0].CompanyCode;
                new hdlStudentOptionalFeeParticular().delete(StudentOptionalFeeParticular);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentOptionalFeeParticular(companyCode);
        }
    }
}