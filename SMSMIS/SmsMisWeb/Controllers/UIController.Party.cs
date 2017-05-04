using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Fee;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        // GET: ClientControllerParty
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult getAllParty(int companyCode, int branchCode)
        {
            return this.Json(new hdlParty().SelectAll(companyCode, branchCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult saveParty(Party Party)
        {
            try
            {
              //  new hdlParty().save(Party, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            //modified by fakhar
            //return getAllParty();//getAllPartyofCompanyBranch(Party.CompanyCode, Party.BranchCode);
            return null;
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult deleteParty(Party Party)
        {
            try
            {
             //   new hdlParty().delete(Party);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            // return getAllParty();// return getAllPartyofCompanyBranch(Party.CompanyCode, Party.BranchCode);
            return null;
            // modified by fakhar
        }
    }
}