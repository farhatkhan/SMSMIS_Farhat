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
        public JsonResult getAllCategory(int CompanyCode)
        {

            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlCategory().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveCategory(Category Category, bool isNew)
        {
            try
            {
                Category.AddByUserId = Convert.ToString(Session["User"]);
                new hdlCategory().save(Category, isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllCategory(Category.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteCategory(Category Category)
        {
            try
            {
                new hdlCategory().delete(Category);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllCategory(Category.CompanyCode);
        }
    }
}