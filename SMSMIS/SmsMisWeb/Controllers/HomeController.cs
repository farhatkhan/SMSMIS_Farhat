using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;

namespace ValencyWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(comUsers user)
        {
            if (ModelState.IsValid)
            {
                Guid userId = new ClientContext().performLogin(user, "");
                if (!Guid.Empty.Equals(userId))
                {
                    // Added by Shah: 2014-09-08
                    Session["isAdmin"] = false;
                    Session["UserId"] = userId;
                    hdlCommon HD = new hdlCommon();
                    //Session["Links"] = HD.getLinks(userId); //TODO
                    return RedirectToAction("UI");
                }
                else
                {
                    ViewBag.errorMsg = "Invalid username or password";
                }
            }
            return Index();
        }
        public ActionResult UI()
        {
            return View();
        }
    }
}