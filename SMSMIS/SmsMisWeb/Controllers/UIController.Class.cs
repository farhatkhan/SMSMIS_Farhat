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
        public JsonResult getAllClass()
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlClass().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveClasses(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlClass().SelectAllActive(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveClass(Class Class)
        {
            try
            {
                Class.AddByUserId = Convert.ToString(Session["User"]);
                new hdlClass().save(Class);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllClasses());
        }

        [HttpPost]
        public JsonResult deleteClass(Class Class)
        {
            try
            {
                new hdlClass().delete(Class);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllClasses());
        }
   }
}