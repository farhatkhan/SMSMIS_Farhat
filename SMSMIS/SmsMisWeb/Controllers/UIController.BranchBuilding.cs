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
        [HttpPost]
        public JsonResult getAllBranchBuilding(string strValues)
        {
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlBranchBuilding().SelectAll(strValues), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void saveBranchBuilding(BranchBuilding BranchBuilding)
        {
            try
            {
                BranchBuilding.AddByUserId = Convert.ToString(Session["User"]);
                new hdlBranchBuilding().save(BranchBuilding);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            //return getAllBranchBuilding();
        }

        [HttpPost]
        public void deleteBranchBuilding(BranchBuilding BranchBuilding)
        {
            try
            {
                new hdlBranchBuilding().delete(BranchBuilding);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            //return getAllBranchBuilding();
        }
   }
}