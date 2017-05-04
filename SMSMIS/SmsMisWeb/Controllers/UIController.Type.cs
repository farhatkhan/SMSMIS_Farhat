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
        public JsonResult getAllType(int CompanyCode)
        {

            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlType().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveType(SmsMis.Models.Console.Admin.Type Type, bool isNew)
        {
            try
            {
                Type.AddByUserId = Convert.ToString(Session["User"]);
                new hdlType().save(Type, isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllType(Type.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteType(SmsMis.Models.Console.Admin.Type Type)
        {
            try
            {
                new hdlType().delete(Type);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllType(Type.CompanyCode);
        }
    }
}