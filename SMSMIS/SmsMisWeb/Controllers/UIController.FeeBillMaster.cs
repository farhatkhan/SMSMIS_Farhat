using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Admin;
using SmsMis.Models.Console.Handlers.Fee;


namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        [HttpPost]
        public JsonResult saveFeeBillMaster(int CompanyCode, int BranchCode, int SessionCode, int BankCode, DateTime IssueDate, DateTime DueDate, List<Class> Class, List<monthList> MonthList)
        {
            try
            {
                new hdlFeeBillMaster().save(Convert.ToString(Session["User"]), CompanyCode, BranchCode, SessionCode, BankCode, IssueDate, DueDate, Class, MonthList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        public JsonResult getAllFeeBillMaster(int CompanyCode,int BranchCode, int SessionCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeBillMaster().SelectAll(CompanyCode, BranchCode, SessionCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getFeeBillMasterWithStudent(int CompanyCode, int BranchCode, int SessionCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeBillMaster().getFeeBillMasterWithStudent(CompanyCode, BranchCode, SessionCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getFeeDetailForFeeReciept(int CompanyCode, int BranchCode, int SessionCode,int ChallanNo)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeBillMaster().getFeeBillDetailForFeeReciept(CompanyCode, BranchCode, SessionCode,ChallanNo), JsonRequestBehavior.AllowGet);
        }
    }
}
        