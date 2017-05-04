using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Common;

namespace SmsMisWeb.Controllers
{
    public partial class ReportController : Controller
    {
        [HttpGet]
        public ActionResult Report()
        {
            return View();
        }
        [HttpGet]
        public ActionResult rptParamApplicationForm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult rptParamApplicationList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult rptParamClassList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult rptParamSubjectCombinationForm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult rptParamSubjectCombinationFormModule()
        {
            return View();
        }
    }
}


