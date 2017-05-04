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
        // GET: Project

        [HttpPost]
        public JsonResult getAllProject(int CompanyCode)
        {            
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlProject().SelectAllCompanyProjects(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getallProjectBranch(int CompanyCode, int BranchCode)
        {
            return Json(new hdlProject().SelectAllBranchProjects(CompanyCode, BranchCode), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveProject(Project Project, int CompanyCode)
        {
            try
            {
                new hdlProject().save(Project, Convert.ToString(Session["User"]), CompanyCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllProject(CompanyCode);
        }

        [HttpPost]
        public JsonResult Project(IList<Project> Project)
        {
            int companyCode = 0;
            try
            {
                companyCode = Project[0].CompanyCode;
                new hdlProject().delete(Project);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllProject(companyCode);
        }
    }
}