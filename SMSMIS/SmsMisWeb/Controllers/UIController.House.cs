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
        public JsonResult getAllHouse(string strValues)
        {
            return Json(new hdlHouse().SelectAll(strValues), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void saveHouse(House House)
        {
            try
            {
                House.AddByUserId = Convert.ToString(Session["User"]);
                new hdlHouse().save(House);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            //return getAllHouse(House.CompanyCode, House.BranchCode);
        }

        [HttpPost]
        public void deleteHouse(House House)
        {
            try
            {
                new hdlHouse().delete(House);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            //return getAllHouse(House.CompanyCode, House.BranchCode);
        }
    }
}