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
        // GET: ClientControllerCSMaster

        [HttpGet]
        public JsonResult getCSDataAs()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCSMaster().SelectAllCSDataAs(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getCSFontStyle()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCSMaster().SelectAllCSFontStyle(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getCSObjectBorder()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCSMaster().SelectAllCSObjectBorder(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getCSRowAction()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCSMaster().SelectAllCSRowAction(), JsonRequestBehavior.AllowGet);
        }
        


        [HttpPost]
        public JsonResult getCSMasterByCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCSMaster().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getCSMasterByID(int companycode, int ReportCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCSMaster().SelectAll(companycode, ReportCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveCSMaster(CSMaster CSMaster)
        {
            try
            {
                new hdlCSMaster().save(CSMaster, Convert.ToString(Session["User"]));
                //return return getAllCSMaster();
                //return Content(JsonConvert.SerializeObject(new { mainObject = getCSMasterByCompany(CSMaster.CompanyCode), subObject = CSMaster }, Formatting.None), "text/json");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getCSMasterByCompany(CSMaster.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteCSMaster(CSMaster CSMaster)
        {
            try
            {
                new hdlCSMaster().delete(CSMaster);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getCSMasterByCompany(CSMaster.CompanyCode);
        }
    }
}