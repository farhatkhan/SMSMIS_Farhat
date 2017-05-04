using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Fee;


namespace ValencyWeb.Controllers
{
    public partial class ClientController
    {
        // GET: ClientControllerInstrumentType
        public JsonResult getAllInstrumentType()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlInstrumentType().SelectAll(), JsonRequestBehavior.AllowGet);
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
 