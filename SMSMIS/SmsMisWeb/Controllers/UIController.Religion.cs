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
        public JsonResult getAllReligion()
        {
            DataTable dt = new CompanyHandler().SelectAll();
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlReligion().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveReligion(Religion Religion)
        {
            try
            {
                Religion.AddByUserId = Convert.ToString(Session["User"]);
                new hdlReligion().save(Religion);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllReligion();
        }

        [HttpPost]
        public JsonResult deleteReligion(Religion Religion)
        {
            try
            {
                new hdlReligion().delete(Religion);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllReligion();
        }
   }
}