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
        // GET: ClientControllerStudentFixedScholarship
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ContentResult getAllStudentFixedScholarship(int CompanyCode)
        {
            //return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedScholarship().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedScholarship().SelectAll(CompanyCode), Formatting.None), "text/json");
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedScholarship().SelectAll(), JsonRequestBehavior.AllowGet);
        //}
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ContentResult saveStudentFixedScholarship(StudentFixedScholarship StudentFixedScholarship, bool isNew)
        {
            try
            {
                new hdlStudentFixedScholarship().save(StudentFixedScholarship, Convert.ToString(Session["User"]), isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFixedScholarship(StudentFixedScholarship.CompanyCode);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public ContentResult deleteStudentFixedScholarship(StudentFixedScholarship StudentFixedScholarship)
        {
            try
            {
                new hdlStudentFixedScholarship().delete(StudentFixedScholarship);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFixedScholarship(StudentFixedScholarship.CompanyCode);
        }
    }
}