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
        // GET: ClientControllerMeasuringUnit

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllMeasuringUnitCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlMeasuringUnit().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedMeasuringUnit(int companyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlMeasuringUnit().SelectAllApproved(companyCode), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlMeasuringUnit().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveMeasuringUnit(MeasuringUnit MeasuringUnit, int companyId)
        {
            try
            {
                new hdlMeasuringUnit().save(MeasuringUnit, Convert.ToString(Session["User"]), companyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllMeasuringUnitCompany(MeasuringUnit.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteMeasuringUnit(MeasuringUnit MeasuringUnit)
        {
            try
            {
                new hdlMeasuringUnit().delete(MeasuringUnit);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllMeasuringUnitCompany(MeasuringUnit.CompanyCode);
        }
    }
}