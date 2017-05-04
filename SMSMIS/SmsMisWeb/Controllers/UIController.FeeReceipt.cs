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
        // GET: ClientControllerFeeReceipt
        public JsonResult getAllFeeReceipt()
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeReceipt().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAllFeeReceiptCompanyBranchWise(int CompanyCode,int BranchCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlFeeReceipt().SelectAll(CompanyCode,BranchCode), JsonRequestBehavior.AllowGet);
        }
        public ContentResult getAllFeeReceiptData(int CompanyCode, int BranchCode, int SessionCode, int ChallanNo, int ReceiptNo)
        {
            return Content(JsonConvert.SerializeObject(new { mainObject = new SmsMis.Models.Console.Handlers.Fee.hdlFeeReceipt().SelectAll(CompanyCode, BranchCode, SessionCode, ReceiptNo), subObject = new SmsMis.Models.Console.Handlers.Fee.hdlFeeBillMaster().getFeeBillDetailForFeeReciept(CompanyCode, BranchCode, SessionCode, ChallanNo) }, Formatting.None), "text/json");
            //return Json({a: }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ContentResult saveFeeReceipt(FeeReceipt FeeReceipt)
        {
            try
            {
                new hdlFeeReceipt().save(FeeReceipt, Convert.ToString(Session["User"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return Content(JsonConvert.SerializeObject(new { mainObject = getAllFeeReceiptCompanyBranchWise(FeeReceipt.CompanyCode, FeeReceipt.BranchCode), subObject = FeeReceipt }, Formatting.None), "text/json");
            //return getAllFeeReceiptCompanyBranchWise(FeeReceipt.CompanyCode, FeeReceipt.BranchCode);
        }

        [HttpPost]
        public JsonResult deleteFeeReceipt(FeeReceipt FeeReceipt)
        {
            try
            {
                new hdlFeeReceipt().delete(FeeReceipt);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllFeeReceiptCompanyBranchWise(FeeReceipt.CompanyCode, FeeReceipt.BranchCode);
        }
    }
}