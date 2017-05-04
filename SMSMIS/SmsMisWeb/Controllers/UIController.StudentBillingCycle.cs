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
        // GET: ClientControllerStudentBillingCycle
        public ContentResult getAllStudentBillingCycle(int CompanyCode)
        {
            //return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentBillingCycle().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new SmsMis.Models.Console.Handlers.Fee.hdlStudentBillingCycle().SelectAll(CompanyCode), Formatting.None), "text/json");
            
        }
        [HttpPost]
        public ContentResult saveStudentBillingCycle(StudentBillingCycle StudentBillingCycle, bool isNew)
        {
            try
            {
                new hdlStudentBillingCycle().save(StudentBillingCycle, Convert.ToString(Session["User"]), isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentBillingCycle(StudentBillingCycle.CompanyCode);
        }

        [HttpPost]
        public ContentResult deleteStudentBillingCycle(StudentBillingCycle StudentBillingCycle)
        {
            try
            {
                new hdlStudentBillingCycle().delete(StudentBillingCycle);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentBillingCycle(StudentBillingCycle.CompanyCode);
        }
    }
}