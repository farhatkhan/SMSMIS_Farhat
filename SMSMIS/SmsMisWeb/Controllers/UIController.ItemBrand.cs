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
        // GET: ClientControllerItemBrand
        
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getAllItemBrandCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemBrand().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult getApprovedItemBrand(int companyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemBrand().SelectAllApproved(companyCode), JsonRequestBehavior.AllowGet);
        }
        
        //public JsonResult getBranch(int companyID)
        //{
        //    return Json(new SmsMis.Models.Console.Handlers.Fee.hdlItemBrand().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveItemBrand(ItemBrand ItemBrand, int companyId)
        {
            try
            {
                new hdlItemBrand().save(ItemBrand, Convert.ToString(Session["User"]), companyId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemBrandCompany(ItemBrand.CompanyCode);
        }

        [HttpPost]
        public JsonResult deleteItemBrand(ItemBrand ItemBrand)
        {
            try
            {
                new hdlItemBrand().delete(ItemBrand);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllItemBrandCompany(ItemBrand.CompanyCode);
        }
    }
}