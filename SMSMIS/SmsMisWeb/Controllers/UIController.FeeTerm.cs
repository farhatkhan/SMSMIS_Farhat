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
        // GET: ClientControllerFeeTerm
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpGet]
        public JsonResult getAllFeeTerm()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeTerm().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpGet]
        public JsonResult getAllFeeTermCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeTerm().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveFeeTerm(FeeTerm FeeTerm)
        {
            try
            {
                new hdlFeeTerm().save(FeeTerm, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeTerm();
        }

        [HttpPost]
        public JsonResult deleteFeeTerm(FeeTerm FeeTerm)
        {
            try
            {
                new hdlFeeTerm().delete(FeeTerm);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeTerm();
        }
    }
}