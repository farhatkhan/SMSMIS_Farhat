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
        [HttpPost]
        public JsonResult getAllCOAbyAccountType(int CompanyCode, /*string AccountType,*/ string levelID)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlChartOfAccounts().SelectCodes(CompanyCode/*,AccountType*/,levelID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllCOABranchesCompanyWise(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlChartOfAccounts().SelectBranchCodes(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        //getAllBankofCompanyBranch
        [HttpPost]
        public JsonResult getAllCOAbyAccountTypeCompanyBranch(int CompanyCode, int BranchCode, string AccountType, string levelID)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlChartOfAccounts().SelectCodes(CompanyCode,BranchCode, AccountType, levelID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllCOAbyCompanyBranch(int CompanyCode, int BranchCode, string AccountType)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlChartOfAccounts().SelectCodes(CompanyCode, BranchCode, AccountType), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllChartOfAccounts(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlChartOfAccounts().SelectAllCOA(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllActiveChartOfAccounts(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlChartOfAccounts().SelectAllActiveCOA(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveChartOfAccounts(ChartOfAccounts ChartOfAccounts, bool isNew)
        {
            
            //ChartOfAccounts.LevelId = string.IsNullOrEmpty(ChartOfAccounts.ParentAccountCode) ? "1" : ChartOfAccounts.LevelId;
            try
            {
                new hdlChartOfAccounts().save(ChartOfAccounts, Convert.ToString(Session["User"]),isNew);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllChartOfAccounts(ChartOfAccounts.CompanyCode);
        }
        public JsonResult deleteChartOfAccounts(ChartOfAccounts ChartOfAccounts)
        {
            try
            {
                new hdlChartOfAccounts().delete(ChartOfAccounts);
            }
            catch (Exception ex)
            {

            }
            return getAllChartOfAccounts(ChartOfAccounts.CompanyCode);
        }

    }
}