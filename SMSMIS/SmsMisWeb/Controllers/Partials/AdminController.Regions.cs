using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Admin;

namespace ValencyWeb.Controllers
{
    public partial class AdminController : Controller
    {

        public JsonResult getRegions()
        {
            return Json(new hdlRegions().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveRegion(comRegion region)
        {
            try
            {
                new hdlRegions().save(region, Convert.ToInt32(Session["UserId"]), Convert.ToBoolean(Session["isAdmin"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getRegions();
        }
        [HttpPost]
        public JsonResult deleteRegion(comRegion region)
        {
            try
            {
                new hdlRegions().delete(region, Convert.ToInt32(Session["UserId"]), Convert.ToBoolean(Session["isAdmin"])); 
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getRegions();
        }
    }
}