using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsMis.Models.Console.Admin;
using SmsMis.Models.Console.Handlers;

namespace ValencyWeb.Controllers
{
    public partial class AdminController : Controller
    {
        private int UserID
        {
            get
            {
                return Convert.ToInt32(Session["UserId"]);
            }
        }
        private bool isAdmin
        {
            get
            {
                return Convert.ToBoolean(Session["isAdmin"]);
            }
        }
        public JsonResult getUserTypes()
        {
            return Json(new hdlUserTypes().SelectAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult saveUserType(comUserTypes objcomUserTypes)
        {
            try
            {
                new hdlUserTypes().save(objcomUserTypes, UserID, isAdmin);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getUserTypes();
        }
        [HttpPost]
        public JsonResult deleteUserType(comUserTypes obj)
        {
            try
            {
                new hdlUserTypes().delete(obj, Convert.ToInt32(Session["UserId"]), Convert.ToBoolean(Session["isAdmin"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getUserTypes();
        }

        [HttpPost]
        public JsonResult getUserType(Guid UserTypeID)
        {
            try
            {
                return Json(new hdlUserTypes().SelectByID(UserTypeID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }

        }

        public JsonResult getAllAccessTypes()
        {
            try
            {
                return Json(new hdlUserTypes().SelectAllAccessTypes(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
        }
    }
}