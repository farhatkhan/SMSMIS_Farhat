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
        // GET: ClientControllerVoucherType
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllVoucherType()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherType().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllVoucherTypeCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherType().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedVoucherType(int companyCode, int branchCode)
        {
            return Json(new hdlVoucherType().SelectAllApproved(companyCode, branchCode), JsonRequestBehavior.AllowGet);            

        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedVoucherTypeFilterJournal(int companyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherType().SelectAllApproved(companyCode,"J"), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherType().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveVoucherType(VoucherType VoucherType,int companyId)
        {
            try
            {
                new hdlVoucherType().save(VoucherType, Convert.ToString(Session["User"]),companyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllVoucherTypeCompany(VoucherType.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteVoucherType(VoucherType VoucherType)
        {
            try
            {
                new hdlVoucherType().delete(VoucherType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllVoucherTypeCompany(VoucherType.CompanyCode);
        }
    }
}