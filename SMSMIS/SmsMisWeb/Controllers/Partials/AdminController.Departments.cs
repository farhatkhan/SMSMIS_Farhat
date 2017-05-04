using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmsMis.Models.Console;
using SmsMis.Models.Console.Admin;

namespace ValencyWeb.Controllers
{
    public partial class AdminController
    {
        
        public JsonResult getAllOperations()
        {
            return Json(new hdlDepartments().SelectAllOperations(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getDepartOperations(int DepartmentID)
        {
            comDepartments dept = new hdlDepartments().SelectByID(DepartmentID);
            return Json(dept.comUserOperationList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDepartments()
        {
            return Json(new hdlDepartments().SelectAll(), JsonRequestBehavior.AllowGet);
        }
   
        [HttpPost]
        public JsonResult getDepartment(int DepartmentID)
        {
            try
            {
                SGIValencyDB db=new SGIValencyDB();
                comDepartments dept = new hdlDepartments().SelectByID(DepartmentID);
                

                return Json(dept, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }

        }

        public JsonResult getDepartmentOperations()
        {
            try
            {
                return Json(new hdlBPMOperations().getDepartmentOperations(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }

        }

        [HttpPost]
        public JsonResult saveDepartment(comDepartments department)
        {
            try
            {
                new hdlDepartments().save(department, Convert.ToInt32(Session["UserId"]), Convert.ToBoolean(Session["isAdmin"]));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getDepartments();
        }
        [HttpPost]
        public JsonResult deleteDepartment(comDepartments department)
        {
            try
            {
                new hdlDepartments().delete(department);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new { error = ex.Message });
            }
            return getDepartments();
        }
    }
}