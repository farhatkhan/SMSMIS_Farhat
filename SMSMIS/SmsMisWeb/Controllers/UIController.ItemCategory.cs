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
        // GET: ClientControllerItemCategory

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllItemCategoryCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemCategory().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedItemCategory(int companyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemCategory().SelectAllApproved(companyCode), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemCategory().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveItemCategory(ItemCategory ItemCategory, int companyId)
        {
            try
            {
                new hdlItemCategory().save(ItemCategory, Convert.ToString(Session["User"]), companyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemCategoryCompany(ItemCategory.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteItemCategory(ItemCategory ItemCategory)
        {
            try
            {
                new hdlItemCategory().delete(ItemCategory);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemCategoryCompany(ItemCategory.CompanyCode);
        }
    }
}