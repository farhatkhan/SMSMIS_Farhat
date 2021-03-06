﻿using System;
using System.Web.Mvc;
using SmsMis.Models.Console;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Handlers.Client;
using SmsMis.Models.Console.Client;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        public JsonResult getAllStudentAdmission()
        {
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAll(), JsonRequestBehavior.AllowGet);
        }
    }
}