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
        // GET: ClientControllerItemGrade

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllItemGradeCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemGrade().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedItemGrade(int companyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemGrade().SelectAllApproved(companyCode), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemGrade().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveItemGrade(ItemGrade ItemGrade, int companyId)
        {
            try
            {
                new hdlItemGrade().save(ItemGrade, Convert.ToString(Session["User"]), companyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemGradeCompany(ItemGrade.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteItemGrade(ItemGrade ItemGrade)
        {
            try
            {
                new hdlItemGrade().delete(ItemGrade);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemGradeCompany(ItemGrade.CompanyCode);
        }
    }
}