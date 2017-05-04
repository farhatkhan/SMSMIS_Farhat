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
        // GET: AnalysisType

        [HttpPost]
        public JsonResult getAllAnalysisType(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlAnalysisType().SelectAllCompanyAnalysisType(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAllAnalysisTypeBranch(int CompanyCode, int BranchCode)
        {
            var result = Json(new hdlAnalysisType().SelectAllBranchAnalysisType(CompanyCode, BranchCode), JsonRequestBehavior.AllowGet);
            return result;
        }



        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveAnalysisType(AnalysisType AnalysisType, int CompanyCode)
        {
            try
            {
                new hdlAnalysisType().save(AnalysisType, Convert.ToString(Session["User"]), CompanyCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllAnalysisType(CompanyCode);
        }

        [HttpPost]
        public JsonResult AnalysisType(IList<AnalysisType> AnalysisType)
        {
            int companyCode = 0;
            try
            {
                companyCode = AnalysisType[0].CompanyCode;
                new hdlAnalysisType().delete(AnalysisType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllAnalysisType(companyCode);
        }
    }
}