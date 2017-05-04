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
        // GET: FeeBillPaymentTerms
                [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpGet]
        public JsonResult getAllFeeBillPaymentTerms()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeBillPaymentTerms().SelectAll(), JsonRequestBehavior.AllowGet);
        }

        
        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}
                [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult saveFeeBillPaymentTerms(IList<FeeBillPaymentTerms> FeeBillPaymentTerms, int CompanyCode, string BillType)
        {
            try
            {

                new hdlFeeBillPaymentTerms().save(FeeBillPaymentTerms, Convert.ToString(Session["User"]), CompanyCode, BillType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeBillPaymentTerms();
        }
                [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult deleteFeeBillPaymentTerms(IList<FeeBillPaymentTerms> FeeBillPaymentTerms)
        {
            try
            {
                new hdlFeeBillPaymentTerms().delete(FeeBillPaymentTerms);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeBillPaymentTerms();
        }
    }
}