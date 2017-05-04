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
        // GET: ClientControllerInstrumentSerialMaster
        public JsonResult getAllInstrumentSerialMaster(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentSerialMaster().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentSerialMaster().SelectAll(), JsonRequestBehavior.AllowGet);
        //}
        
            [HttpPost]
        public JsonResult saveInstrumentCancelled(InstrumentSerialDetail InstrumentSerialDetail)
        {
            try
            {
                new hdlInstrumentSerialMaster().save(InstrumentSerialDetail, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return Json("", JsonRequestBehavior.AllowGet);
            //return getAllInstrumentSerialMaster();
        }
        [HttpPost]
        public JsonResult saveInstrumentSerialMaster(InstrumentSerialMaster InstrumentSerialMaster, bool isNew)
        {
            try
            {
                new hdlInstrumentSerialMaster().save(InstrumentSerialMaster, Convert.ToString(Session["User"]), isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllInstrumentSerialMaster(InstrumentSerialMaster.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteInstrumentSerialMaster(InstrumentSerialMaster InstrumentSerialMaster)
        {
            try
            {
                new hdlInstrumentSerialMaster().delete(InstrumentSerialMaster);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllInstrumentSerialMaster(InstrumentSerialMaster.CompanyCode);
        }
    }
}