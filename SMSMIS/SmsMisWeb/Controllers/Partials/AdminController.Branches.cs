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
        [HttpPost]
        public JsonResult saveBranch(comBranch branch)
        {
            try
            {


                new hdlBranch().save(branch);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getBranches();
        }
                [HttpPost]
        public JsonResult getBranch(Guid BranchID)
        {
            try
            {
                return Json(new hdlBranch().SelectByID(BranchID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            
        }
        
        public JsonResult getBranches()
        {
            return Json(new SmsMis.Models.Console.Admin.hdlBranch().SelectAll(), JsonRequestBehavior.AllowGet);
        }

        // [HttpGet]
        //public JsonResult getBranchesEx(Guid usertypeID)
        //{
        //    return Json(new SmsMis.Models.Console.Admin.hdlBranch().SelectAllEx(usertypeID), JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult deleteBranch(comBranch branch)
        {
            try
            {
                new hdlBranch().delete(branch);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getBranches();
        }
	}
}