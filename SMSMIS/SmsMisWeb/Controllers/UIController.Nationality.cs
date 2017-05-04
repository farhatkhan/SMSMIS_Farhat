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
        public JsonResult getAllNationality()
        {
            DataTable dt = new CompanyHandler().SelectAll();
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlNationality().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveNationality(Nationality Nationality)
        {
            try
            {
                Nationality.AddByUserId = Convert.ToString(Session["User"]);
                new hdlNationality().save(Nationality);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllNationality();
        }

        [HttpPost]
        public JsonResult deleteNationality(Nationality Nationality)
        {
            try
            {
                new hdlNationality().delete(Nationality);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllNationality();
        }
   }
}