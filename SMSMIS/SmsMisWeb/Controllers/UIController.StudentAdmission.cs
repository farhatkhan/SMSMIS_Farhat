using System;
using System.Web.Mvc;
using SmsMis.Models.Console;
using Newtonsoft.Json;
using System.Data;
using SmsMis.Models.Console.Handlers.Client;
using SmsMis.Models.Console.Client;
using System.Collections.Generic;

namespace SmsMisWeb.Controllers
{
    public partial class UIController
    {
        public JsonResult getAllStudent()
        {
            //var result = new { Result = "Successed", ID = "32" };
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAllStudentsByCompanyBranchSession(string strValues)
        {
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAllStudentsByCompanyBranchSession(strValues), JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult getAllStudents(string strValues)
        {
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAllStudent(strValues), JsonRequestBehavior.AllowGet);
        }

        public ContentResult getClassWithSnap(string strValues)
        {
            DataTable dt = new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectClassWithSnap(strValues);
            return Content(JsonConvert.SerializeObject(dt, Formatting.None), "text/json");
            //return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectClassWithSnap(strValues), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAllStudentofCompany(int CompanyCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAll(CompanyCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllStudentofBranch(int CompanyCode,int BranchCode)
        {
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAll(CompanyCode,BranchCode), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult load(int CompanyCode, int BranchCode, int SessionCode, int StudentNo)
        {
            return Json(new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAll(CompanyCode, BranchCode, SessionCode, StudentNo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult saveStudent(Student Student,string imgFile)
        {
            try
            {
                Student.AddByUserId = Convert.ToString(Session["User"]);
                string path = Server.MapPath("~/upload/students/");
                new hdlStudent().save(Student, imgFile, path);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }

            var result = new { dt = new SmsMis.Models.Console.Handlers.Client.hdlStudent().SelectAll(), ID = Student.StudentNo };
            return Json(result, JsonRequestBehavior.AllowGet);
            //return getAllStudent();
        }

        [HttpPost]
        public JsonResult saveStudentAdmission(string strStudentNo, StudentAdmission stdAdmission, List<StudentAdmissionSubject> lstAdmissionSubjects)
        {
            try
            {
                new hdlStudent().saveStudentAdmission(strStudentNo, stdAdmission, lstAdmissionSubjects);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudent();
        }

        

        [HttpPost]
        public JsonResult deleteStudent(Student Student)
        {
            try
            {
                new hdlStudent().delete(Student);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllStudent();
        }
    }
}