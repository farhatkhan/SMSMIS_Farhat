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
        public JsonResult getAllDesignation(int CompanyCode)
        {

            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlDesignation().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveDesignation(Designation Designation, bool isNew)
        {
            try
            {
                Designation.AddByUserId = Convert.ToString(Session["User"]);
                new hdlDesignation().save(Designation, isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllDesignation(Designation.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteDesignation(Designation Designation)
        {
            try
            {
                new hdlDesignation().delete(Designation);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllDesignation(Designation.CompanyCode);
        }
    }
}