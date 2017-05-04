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
        //public JsonResult getAllSession()
        //{
        //    return Json(new hdlCompanySession().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult getActiveSession()
        //{
        //    return Json(new hdlCompanySession().SelectActiveSession(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult getAllSessionofCompany(int CompanyCode)
        {
            return Json(new hdlCompanySession().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveCompanySession(Session companySession)
        {
            try
            {
                companySession.AddByUserId = Convert.ToString(Session["User"]);
                new hdlCompanySession().save(companySession);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllSessions());
        }

        [HttpPost]
        public JsonResult deleteCompanySession(Session companySession)
        {
            try
            {
                new hdlCompanySession().delete(companySession);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllSessions());
        }


        //[HttpPost]
        //public ContentResult deleteCompany(int iCompanyId)
        //{
        //    try
        //    {
        //        new CompanyHandler().Delete(iCompanyId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 500;
        //        Content(JsonConvert.SerializeObject(new { error = ex.Message }));
        //    }
        //    return getCompany();
        //}
    }
}