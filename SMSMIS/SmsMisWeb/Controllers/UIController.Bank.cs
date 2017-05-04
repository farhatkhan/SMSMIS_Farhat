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
        // GET: ClientControllerBank
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpGet]
        public JsonResult getAllBank()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlBank().SelectAll(), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult SelectBankByAccountCode(string AccountCode, int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlBank().SelectBankByAccountCode(AccountCode, CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult saveBank(Bank Bank)
        {
            try
            {
                new hdlBank().save(Bank, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllBankofCompany(Bank.CompanyCode);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult deleteBank(Bank Bank)
        {
            try
            {
                new hdlBank().delete(Bank);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllBankofCompany(Bank.CompanyCode);
        }
    }
}