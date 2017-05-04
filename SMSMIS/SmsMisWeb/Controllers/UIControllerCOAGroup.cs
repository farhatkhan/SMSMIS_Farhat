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
        public JsonResult getAllCOAGroup(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCOAGroup().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveCOAGroup(IList<COAGroup> COAGroup)
        {
            try
            {

                new hdlCOAGroup().save(COAGroup, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return null;// getAllCOAGroup(COAGroup[0].CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteCOAGroup(IList<COAGroup> COAGroup)
        {
            try
            {
                new hdlCOAGroup().delete(COAGroup);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return null;//return getAllCOAGroup(COAGroup[0].CompanyCode);
        }
    }
}