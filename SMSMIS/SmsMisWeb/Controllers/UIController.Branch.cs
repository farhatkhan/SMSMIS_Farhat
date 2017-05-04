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
    public partial class UIController
    {
        //public JsonResult getAllBranch()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult /getAllActiveBranches()
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectActiveBranches(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult getAllBranchofCompany(int CompanyCode)
        {

            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranch().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveBranch(Branch branch, string imgFile)
        {
            try
            {
                branch.AddByUserId = Convert.ToString(Session["User"]);
                string path = Server.MapPath("~/upload/Branches/");
                new hdlBranch().save(branch, imgFile, path);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllBranches());
        }

        [HttpPost]
        public JsonResult deleteBranch(Branch branch)
        {
            try
            {
                new hdlBranch().delete(branch);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return (new UIController().getAllBranches());
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