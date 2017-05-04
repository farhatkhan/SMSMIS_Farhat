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
        public JsonResult getAllDepartment(int CompanyCode)
        {

            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlDepartment().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveDepartment(Department Department, bool isNew)
        {
            try
            {
                Department.AddByUserId = Convert.ToString(Session["User"]);
                new hdlDepartment().save(Department, isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllDepartment(Department.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteDepartment(Department Department)
        {
            try
            {
                new hdlDepartment().delete(Department);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllDepartment(Department.CompanyCode);
        }
    }
}