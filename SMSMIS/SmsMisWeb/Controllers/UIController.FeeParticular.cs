using System;
using System.Web.Mvc;
using SmsMis.Models.Console.Handlers.Admin;
using Newtonsoft.Json;
using SmsMis.Models.Console.Admin;
using SmsMis.Models.Console.Client;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        public JsonResult getAllFeeParticular()
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlFeeParticular().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveFeeParticular()
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlFeeParticular().SelectAllActiveOptional(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveOnlyFeeParticular()
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlFeeParticular().SelectAllActive(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveFeeParticularofCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlFeeParticular().SelectAllActive(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllFeeParticularofCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlFeeParticular().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getFeeParticularRecurringType()
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlFeeParticularRecurringType().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveFeeParticular(FeeParticular FeeParticular)
        {
            try
            {
                FeeParticular.AddByUserId = Convert.ToString(Session["User"]);
                new hdlFeeParticular().save(FeeParticular);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeParticularofCompany(FeeParticular.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteFeeParticular(FeeParticular FeeParticular)
        {
            try
            {
                new hdlFeeParticular().delete(FeeParticular);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeParticularofCompany(FeeParticular.CompanyCode);
        }


        //[HttpPost]
        //public ContentResult deleteCompany(int iCompanyId)
        //{
        //    try
        //    {
        //        new CompanyHandler().Delete(iCompanyId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 500;
        //        Content(JsonConvert.SerializeObject(new { error = ex.Message }));
        //    }
        //    return getCompany();
        //}
    }
}