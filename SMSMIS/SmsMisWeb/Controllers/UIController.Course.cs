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
        //public JsonResult getAllCourse()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlCourse().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult getAllActiveCourse()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlCourse().SelectAllActive(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveCourse(Course Course)
        {
            try
            {
                Course.AddByUserId = Convert.ToString(Session["User"]);
                new hdlCourse().save(Course);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllCourses());
        }

        [HttpPost]
        public JsonResult deleteCourse(Course Course)
        {
            try
            {
                new hdlCourse().delete(Course);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllCourses());
        }
   }
}