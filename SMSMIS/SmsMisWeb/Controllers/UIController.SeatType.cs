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
        public JsonResult getAllSeatType()
        {
            DataTable dt = new CompanyHandler().SelectAll();
            return Json(new SmsMis.Models.Console.Handlers.Admin.hdlSeatType().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveSeatType(SeatType SeatType)
        {
            try
            {
                SeatType.AddByUserId = Convert.ToString(Session["User"]);
                new hdlSeatType().save(SeatType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllSeatType();
        }

        [HttpPost]
        public JsonResult deleteSeatType(SeatType SeatType)
        {
            try
            {
                new hdlSeatType().delete(SeatType);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllSeatType();
        }
   }
}