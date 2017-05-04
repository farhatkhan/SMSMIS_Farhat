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
        public JsonResult getAllCOALevels(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Fee.hdlCOALevels().SelectCOALevels(CompanyCode), JsonRequestBehavior.AllowGet);
        }

    }
}