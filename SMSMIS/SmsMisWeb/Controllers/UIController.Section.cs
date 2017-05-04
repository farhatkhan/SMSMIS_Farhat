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
        //public JsonResult getAllSection()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlSection().SelectAll(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveSection()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlSection().SelectAllActive(), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult saveSection(Section Section)
        {
            try
            {
                Section.AddByUserId = Convert.ToString(Session["User"]);
                new hdlSection().save(Section);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllSections());
        }

        [HttpPost]
        public JsonResult deleteSection(Section Section)
        {
            try
            {
                new hdlSection().delete(Section);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllSections());
        }
   }
}