using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Fee;


namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        // GET: ClientControllerStudentFixedDiscount
        public ContentResult getAllStudentFixedDiscount(int CompanyCode)
        {
            //return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedDiscount().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedDiscount().SelectAll(CompanyCode), Formatting.None), "text/json");
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedDiscount().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ContentResult saveStudentFixedDiscount(StudentFixedDiscount StudentFixedDiscount, bool isNew)
        {
            try
            {
                new hdlStudentFixedDiscount().save(StudentFixedDiscount, Convert.ToString(Session["User"]), isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFixedDiscount(StudentFixedDiscount.CompanyCode);
        }

        [HttpPost]
        public ContentResult deleteStudentFixedDiscount(StudentFixedDiscount StudentFixedDiscount)
        {
            try
            {
                new hdlStudentFixedDiscount().delete(StudentFixedDiscount);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFixedDiscount(StudentFixedDiscount.CompanyCode);
        }
    }
}