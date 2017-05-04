using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Handlers.Admin;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Admin;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        public JsonResult getAllMarketingReference()
        {
            DataTable dt = new CompanyHandler().SelectAll();
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlMarketingReference().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveMarketingReference(MarketingReference MarketingReference)
        {
            try
            {
                MarketingReference.AddByUserId = Convert.ToString(Session["User"]);
                new hdlMarketingReference().save(MarketingReference);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllMarketingReference();
        }

        [HttpPost]
        public JsonResult deleteMarketingReference(MarketingReference MarketingReference)
        {
            try
            {
                new hdlMarketingReference().delete(MarketingReference);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllMarketingReference();
        }
   }
}