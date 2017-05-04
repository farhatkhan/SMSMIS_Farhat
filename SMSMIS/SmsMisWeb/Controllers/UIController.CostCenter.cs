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
        // GET: CostCenter

        [HttpPost]
        public JsonResult getAllCostCenter(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCostCenter().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getallCostCenterBranch(int CompanyCode, int BranchCode)
        {
            return Json(new hdlCostCenter().SelectAllCostCenterBranch(CompanyCode,BranchCode), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveCostCenter(CostCenter CostCenter, int CompanyCode)
        {
            try
            {
                new hdlCostCenter().save(CostCenter, Convert.ToString(Session["User"]), CompanyCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllCostCenter(CompanyCode);
        }

        [HttpPost]
        public JsonResult CostCenter(IList<CostCenter> CostCenter)
        {
            int companyCode = 0;
            try
            {
                companyCode = CostCenter[0].CompanyCode;
                new hdlCostCenter().delete(CostCenter);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllCostCenter(companyCode);
        }
    }
}