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
        public JsonResult getAllGrade(int CompanyCode)
        {

            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlGrade().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveGrade(Grade Grade,bool isNew)
        {
            try
            {
                Grade.AddByUserId = Convert.ToString(Session["User"]);
                new hdlGrade().save(Grade, isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllGrade(Grade.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteGrade(Grade Grade)
        {
            try
            {
                new hdlGrade().delete(Grade);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllGrade(Grade.CompanyCode);
        }
   }
}