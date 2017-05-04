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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllFeeParticularRate()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeParticularRate().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult saveFeeParticularRate(IList<FeeParticularRate> FeeParticularRate, int CompanyCode, int BranchCode, int SessionCode, int ClassCode)
        {
            try
            {

                new hdlFeeParticularRate().save(FeeParticularRate, Convert.ToString(Session["User"]), CompanyCode, BranchCode, SessionCode, ClassCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeParticularRate();
        }

        [HttpPost]
        public JsonResult deleteFeeParticularRate(IList<FeeParticularRate> FeeParticularRate)
        {
            try
            {
                new hdlFeeParticularRate().delete(FeeParticularRate);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeParticularRate();
        }
    }
}