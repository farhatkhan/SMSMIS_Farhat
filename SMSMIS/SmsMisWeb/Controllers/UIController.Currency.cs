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
        // GET: ClientControllerCurrency
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllCurrency()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCurrency().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllActiveCurrency()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCurrency().SelectAllActive(), JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCurrency().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveCurrency(Currency Currency)
        {
            try
            {
                new hdlCurrency().save(Currency, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllCurrency();
        }

        [HttpPost]
        public JsonResult deleteCurrency(Currency Currency)
        {
            try
            {
                new hdlCurrency().delete(Currency);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllCurrency();
        }
    }
}
