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
        //public JsonResult getAllSubject()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlSubject().SelectAll(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getAllActiveSubject()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlSubject().SelectAllActive(), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult saveSubject(Subject Subject)
        {
            try
            {
                Subject.AddByUserId = Convert.ToString(Session["User"]);
                new hdlSubject().save(Subject);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllSubjects());
        }

        [HttpPost]
        public JsonResult deleteSubject(Subject Subject)
        {
            try
            {
                new hdlSubject().delete(Subject);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllSubjects());
        }
   }
}