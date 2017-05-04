using System;
using System.Web.Mvc;
using SmsMis.Models.Console;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Handlers.Client;
using SmsMis.Models.Console.Client;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()

            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue

            };
        }
        //public JsonResult getAllEmployee()
        //{
        //    var jsonResult = Json(new SmsMis.Models.Console.Handlers.Client.hdlEmployee().SelectAll(), JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;
        //}
       
        [HttpPost]
        public JsonResult getAllEmployee(int CompanyCode)
        {
            var jsonResult = Json(new SmsMis.Models.Console.Handlers.Client.hdlEmployee().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        [HttpPost]
        public JsonResult loadEmployee(int CompanyCode, int EmployeeCode)
        {
            var jsonResult = Json(new SmsMis.Models.Console.Handlers.Client.hdlEmployee().SelectAll(CompanyCode, EmployeeCode), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult saveEmployee(Employee Employee,bool isNew ,string imgEmployeePhoneFile, string imgSignature)
        {
            try
            {
                Employee.AddByUserId = Convert.ToString(Session["User"]);
                string path = Server.MapPath("~/upload/Employees/");
                string signaturePath = Server.MapPath("~/upload/Employees/Signatures/");
                new hdlEmployee().save(Employee, imgEmployeePhoneFile, imgSignature, path, signaturePath);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllEmployee(Employee.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteEmployee(Employee Employee)
        {
            try
            {
                new hdlEmployee().delete(Employee);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllEmployee(Employee.CompanyCode);
        }
    }
}