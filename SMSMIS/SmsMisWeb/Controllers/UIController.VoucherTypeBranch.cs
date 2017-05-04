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
        // GET: ClientControllerVoucherTypeBranch

        public JsonResult getAllVoucherTypeBranch(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherTypeBranch().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult getAllVoucherTypeBranch(int CompanyCode)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherTypeBranch().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlVoucherTypeBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveVoucherTypeBranch(IList<VoucherTypeBranch> VoucherTypeBranch,int companycode,int branchcode)
        {
            try
            {
                new hdlVoucherTypeBranch().save(VoucherTypeBranch,Convert.ToString(Session["User"]), companycode, branchcode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllVoucherTypeBranch(companycode);
        }

        [HttpPost]
        public JsonResult deleteVoucherTypeBranch(IList<VoucherTypeBranch> VoucherTypeBranch)
        {
            try
            {
                new hdlVoucherTypeBranch().delete(VoucherTypeBranch);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllVoucherTypeBranch(VoucherTypeBranch[0].CompanyCode);
        }
    }
}