using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Handlers.Admin;
using Newtonsoft.Json;
using System.Data;

namespace SmsMisWeb.Controllers
{
    public partial class AdminController
    {
        //public ContentResult getCompany()
        //{
        //    DataTable dt = new CompanyHandler().SelectAll();
        //    return Content(JsonConvert.SerializeObject(dt, Formatting.None), "text/json");
        //}

        //public ContentResult getActiveCompanies()
        //{
        //    DataTable dt = new CompanyHandler().SelectAllActiveCompany();
        //    return Content(JsonConvert.SerializeObject(dt, Formatting.None), "text/json");
        //}

        [HttpPost]
        public ContentResult saveCompany(Company company, string imgFile)
        {
            try 
            {
                company.AddByUserId = Convert.ToString(Session["User"]);
                string path = Server.MapPath("~/upload/companies/");
                new CompanyHandler().Save(company, imgFile, path);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new ClientController().getAllCompanies());
        }
        [HttpPost]
        public ContentResult deleteCompany(int iCompanyId)
        {
            try
            {
                new CompanyHandler().Delete(iCompanyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new ClientController().getAllCompanies());
        }
    }
}