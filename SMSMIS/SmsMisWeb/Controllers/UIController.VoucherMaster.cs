using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Ajax.Utilities;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Fee;
using SmsMis.Models.Console.Models;
using SmsMis.Models.Console.ViewModel;
//using SmsMisWeb.Filters;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        // GET: ClientControllerVoucherMaster
        public JsonResult getAllVoucherMaster()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherMaster().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCompanyVoucherMaster(int? CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherMaster().SelectAll(CompanyCode.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
            //var companyObj = new SmsMis.Models.Console.Handlers.Fee.hdlVoucherMaster().SelectAll(CompanyCode);

            //return Json(companyObj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getVoucherByID(int companycode, int branchcode, int voucherCode, int voucherNo, DateTime voucherDate)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherMaster().SelectAll(companycode, branchcode, voucherCode, voucherNo, voucherDate), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        //[Cutom]
        //[DebugModelBinder]
        public ContentResult saveVoucherMaster(VoucherMaster VoucherMaster)
        {
            try
            {
                new hdlVoucherMaster().save(VoucherMaster, Convert.ToString(Session["User"]));
                //var companyObj = new SmsMis.Models.Console.Handlers.Fee.hdlVoucherMaster().SelectAll();
                //return return getAllVoucherMaster();
                return Content(JsonConvert.SerializeObject(new { mainObject = getAllVoucherMaster(), subObject = VoucherMaster }, Formatting.None), "text/json");
                //return Json(res1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            //return getAllVoucherMaster()
        }

        [HttpPost]
        public JsonResult deleteVoucherMaster(VoucherMaster VoucherMaster)
        {
            try
            {
                new hdlVoucherMaster().delete(VoucherMaster);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllVoucherMaster();
        }


        [HttpPost]
        public JsonResult generateVoucherNumber(int companyCode, int branchCode, int voucherCode, DateTime voucherDate)
        {
            using (SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB())
            {

                var voucherTypes = db.Database.SqlQuery<VoucherTypeVM>("select CAST( RANK() over (order by AccountCode,VoucherCode ) AS int) as VoucherTypeRowNumber, VoucherCode,VoucherName,AccountCode,AccountTitle,TransactionType, Category,Frequency,CompanyCode,BranchCode from LOV_VoucherType")
                .Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).ToList();
                var voucherType = voucherTypes.Where(x => x.VoucherCode == voucherCode).FirstOrDefault();
                var assignAccountCodeDisable = voucherType.TransactionType;
                int voucherNumber = new hdlVoucherMaster().getVoucherNumber(companyCode, branchCode, voucherCode, voucherDate);
               Dictionary<int,string> voucherTypeAccount = new Dictionary<int,string>();
                voucherTypeAccount.Add(1, "Bank");
                voucherTypeAccount.Add(2, "Cash");
                return Json(new
                {
                    VoucherNumber = voucherNumber,
                    AccountCode = voucherType.TransactionType.Contains(voucherTypeAccount.Values.ToString()) ? voucherType.AccountCode:null,
                    AccountTitle = voucherType.TransactionType.Contains(voucherTypeAccount.Values.ToString()) ? voucherType.AccountTitle : null
                }, JsonRequestBehavior.AllowGet);
            }
        }
        //Farhat Ullah
        [HttpPost]
        public JsonResult ValidateInstrumentNumber(int companyCode, int branchCode, int instrumentRowNumber, int voucherTypeRowNumber, int instrumentNumber)
        {
            using (
                SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB())
            {
                var voucherTypes =
                    db.Database.SqlQuery<VoucherTypeVM>(
                        "select CAST( RANK() over (order by AccountCode,VoucherCode ) AS int) as VoucherTypeRowNumber, VoucherCode,VoucherName,AccountCode,AccountTitle,TransactionType, Category,Frequency,CompanyCode,BranchCode from LOV_VoucherType")
                        .Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).ToList();
                var voucherType =
                    voucherTypes.Where(x => x.VoucherTypeRowNumber == voucherTypeRowNumber).FirstOrDefault();
                var accountCode = "";
                if (voucherType != null)
                    accountCode = voucherType.AccountCode;
                var InstrumentTypes =
                    db.Database.SqlQuery<InstrumentTypeVM>(
                        "select CAST(RANK() over (order by AccountCode) AS int) as InstrumentTypeRowNumber, AccountCode,InstrumentTypeCode,InstrumentName,CompanyCode,BranchCode from lov_instrument")
                        .Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).ToList();
                var InstrumentType =
                    InstrumentTypes.Where(x => x.InstrumentTypeRowNumber == instrumentRowNumber).FirstOrDefault();
                var instrumentCode = 0;
                if (InstrumentType != null)
                    instrumentCode = InstrumentType.InstrumentTypeCode;
                if (accountCode != "" || instrumentCode != 0)
                {
                    bool ValidNotValid = new hdlVoucherMaster().ValidateInstrumntNumber(companyCode, branchCode,
                        accountCode, instrumentCode, instrumentNumber);
                    return Json(ValidNotValid, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        //Farhat Ullah end
    }
}
