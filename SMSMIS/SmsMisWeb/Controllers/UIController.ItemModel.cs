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
        // GET: ClientControllerItemModel

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllItemModelCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemModel().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedItemModel(int companyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemModel().SelectAllApproved(companyCode), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemModel().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveItemModel(ItemModel ItemModel, int companyId)
        {
            try
            {
                new hdlItemModel().save(ItemModel, Convert.ToString(Session["User"]), companyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemModelCompany(ItemModel.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteItemModel(ItemModel ItemModel)
        {
            try
            {
                new hdlItemModel().delete(ItemModel);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemModelCompany(ItemModel.CompanyCode);
        }
    }
}