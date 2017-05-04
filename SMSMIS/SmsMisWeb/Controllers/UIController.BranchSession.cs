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
        public JsonResult getAllBranchSession()
        {
            return Json(new hdlBranchSession().SelectAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveBranchSession(List<BranchSession> branchSession, int iCompanyCode, int iBranchCode)
        {
            try
            {
                //branch.BranchContactPersonList.ToList<BranchContactPerson>().ForEach(i => i.BranchCode = branch.BranchCode);
                if (branchSession != null)
                    branchSession.ToList<BranchSession>().ForEach(i => i.AddByUserId = Convert.ToString(Session["User"]));
                new hdlBranchSession().save(branchSession, iCompanyCode, iBranchCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllBranchSession();
        }

        [HttpPost]
        public JsonResult deleteBranchSession(List<BranchSession> branchSession, int iCompanyCode, int iBranchCode)
        {
            try
            {
                new hdlBranchSession().delete(branchSession, iCompanyCode, iBranchCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllBranchSession();
        }
    }
}