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
        // GET: ClientControllerInstrumentSerial
        public JsonResult getAllInstrumentSerial()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentSerial().SelectAll(), JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentSerial().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveInstrumentSerial(InstrumentSerial InstrumentSerial)
        {
            try
            {
                new hdlInstrumentSerial().save(InstrumentSerial, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllInstrumentSerial();
        }

        [HttpPost]
        public JsonResult deleteInstrumentSerial(InstrumentSerial InstrumentSerial)
        {
            try
            {
                new hdlInstrumentSerial().delete(InstrumentSerial);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllInstrumentSerial();
        }
    }
}