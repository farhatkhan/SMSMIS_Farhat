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
        // GET: ClientControllerStudentFixedDiscountType
        public JsonResult getAllStudentFixedDiscountType(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedDiscountType().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllStudentFixedDiscountTypeActive(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedDiscountType().SelectAllActive(CompanyCode), JsonRequestBehavior.AllowGet);
        }


        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlStudentFixedDiscountType().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveStudentFixedDiscountType(StudentFixedDiscountType StudentFixedDiscountType,bool isNew)
        {
            try
            {
                new hdlStudentFixedDiscountType().save(StudentFixedDiscountType, Convert.ToString(Session["User"]), isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFixedDiscountType(StudentFixedDiscountType.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteStudentFixedDiscountType(StudentFixedDiscountType StudentFixedDiscountType)
        {
            try
            {
                new hdlStudentFixedDiscountType().delete(StudentFixedDiscountType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudentFixedDiscountType(StudentFixedDiscountType.CompanyCode);
        }
    }
}