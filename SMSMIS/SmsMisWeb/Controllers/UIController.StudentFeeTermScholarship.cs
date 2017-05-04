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
        // GET: ClientControllerStudentFeeTermScholarship
        public ContentResult getAllStudentFeeTermScholarship(int CompanyCode)
        {
            //return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFeeTermScholarship().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFeeTermScholarship().SelectAll(CompanyCode), Formatting.None), "text/json");
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFeeTermScholarship().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ContentResult saveStudentFeeTermScholarship(StudentFeeTermScholarship StudentFeeTermScholarship, bool isNew)
        {
            try
            {
                new hdlStudentFeeTermScholarship().save(StudentFeeTermScholarship, Convert.ToString(Session["User"]), isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFeeTermScholarship(StudentFeeTermScholarship.CompanyCode);
        }

        [HttpPost]
        public ContentResult deleteStudentFeeTermScholarship(StudentFeeTermScholarship StudentFeeTermScholarship)
        {
            try
            {
                new hdlStudentFeeTermScholarship().delete(StudentFeeTermScholarship);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFeeTermScholarship(StudentFeeTermScholarship.CompanyCode);
        }
    }
}