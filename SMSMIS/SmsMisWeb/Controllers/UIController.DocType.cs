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
        public JsonResult getAllDocType()
        {
            DataTable dt = new CompanyHandler().SelectAll();
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlDocType().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveDocType(DocType DocType)
        {
            try
            {
                DocType.AddByUserId = Convert.ToString(Session["User"]);
                new hdlDocType().save(DocType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllDocType();
        }

        [HttpPost]
        public JsonResult deleteDocType(DocType DocType)
        {
            try
            {
                new hdlDocType().delete(DocType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllDocType();
        }
   }
}