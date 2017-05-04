using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valency.Models.Console;
using Valency.Models.Console.Admin;

namespace ValencyWeb.Controllers
{
	public partial class AdminController : Controller
	{
        //[HttpPost]
        //public JsonResult saveBranch(comOperations branch)
        //{
        //    try
        //    {


        //        new hdlBPMOperations().save(branch);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 500;
        //        return Json(new { error = ex.Message });
        //    }
        //    return getBranches();
        //}
               // [HttpPost]
        public JsonResult getOperation()
        {
            try
            {
                return Json(new hdlBPMOperations().getAllOperations(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            
        }
        
        public JsonResult getOperations(int DepartmentID)
        {
            return Json(new Valency.Models.Console.Admin.hdlBPMOperations().getOperations(DepartmentID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getOperationsEx(int DepartmentID)
        {
            return Json(new Valency.Models.Console.Admin.hdlBPMOperations().getOperationsEx(DepartmentID), JsonRequestBehavior.AllowGet);
        }
        
        //public JsonResult getBranches()
        //{
        //    return Json(new Valency.Models.Console.Admin.hdlBPMOperations().SelectAll(), JsonRequestBehavior.AllowGet);
        //}

        // [HttpGet]
        //public JsonResult getBranchesEx(Guid usertypeID)
        //{
        //    return Json(new Valency.Models.Console.Admin.hdlBPMOperations().SelectAllEx(usertypeID), JsonRequestBehavior.AllowGet);
        //}
       // [HttpPost]
        //public JsonResult deleteBranch(comOperations branch)
        //{
        //    try
        //    {
        //        new hdlBPMOperations().delete(branch);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 500;
        //        return Json(new { error = ex.Message });
        //    }
        //    return getBranches();
        //}
	}
}