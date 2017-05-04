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
        // GET: ClientControllerInstrumentType
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllInstrumentType()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentType().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllActiveInstrumentType()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentType().SelectAllActive(), JsonRequestBehavior.AllowGet);
        }
        //Farhat Ullah start
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedInstrumentType(int CompanyCode, int BranchCode)
        {
            return Json(new hdlInstrumentType().SelectAllApprove(CompanyCode, BranchCode), JsonRequestBehavior.AllowGet);
        }
        //Farhat Ullah end
        public JsonResult getAllActiveInstrumentTypeManageSerial()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentType().SelectAllActiveManageSerial(), JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentType().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveInstrumentType(InstrumentType InstrumentType)
        {
            try
            {
                new hdlInstrumentType().save(InstrumentType, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllInstrumentType();
        }

        [HttpPost]
        public JsonResult deleteInstrumentType(InstrumentType InstrumentType)
        {
            try
            {
                new hdlInstrumentType().delete(InstrumentType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllInstrumentType();
        }
    }
}

