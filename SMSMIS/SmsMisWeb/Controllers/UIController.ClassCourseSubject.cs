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
        public ContentResult getAllClassCourseSubject()
        {
            //return Json(new hdlClassCourseSubject().SelectAll(), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new hdlClassCourseSubject().SelectAll(), Formatting.None), "text/json");
        }

        public ContentResult getAllClassesEx()
        {
            return Content(JsonConvert.SerializeObject(new hdlClassCourseSubject().SelectAllClass(), Formatting.None), "text/json");
        }

        public ContentResult getAllClassCourses()
        {
            return Content(JsonConvert.SerializeObject(new hdlClassCourseSubject().SelectAllClassCourse(), Formatting.None), "text/json");
        }

        public ContentResult getAllClassCourseSubjects()
        {
            return Content(JsonConvert.SerializeObject(new hdlClassCourseSubject().getAllClassCourseSubjects(), Formatting.None), "text/json"); 
        }
        public ContentResult getAllStudentClassCourseSubjects()
        {
            return Content(JsonConvert.SerializeObject(new hdlClassCourseSubject().getAllStudentClassCourseSubjects(), Formatting.None), "text/json"); 
        }

        


        [HttpPost]
        public ContentResult getAllCompanyClassCourseSubjects(int CompanyCode)
        {
            //return Json(new hdlClassCourseSubject().SelectAllClassCourseSubjects(), JsonRequestBehavior.AllowGet);
            return Content(JsonConvert.SerializeObject(new hdlClassCourseSubject().SelectAllClassCourseSubjects(CompanyCode), Formatting.None), "text/json");
        }


        [HttpPost]
        public ContentResult saveClassCourseSubject(List<ClassCourseSubject> ClassCourseSubject, int iCompanyCode, int iBranchCode, int iClassCode, int iCourseCode)
        {
            try
            {
                //branch.BranchContactPersonList.ToList<BranchContactPerson>().ForEach(i => i.BranchCode = branch.BranchCode);
                if (ClassCourseSubject != null)
                    ClassCourseSubject.ToList<ClassCourseSubject>().ForEach(i => i.AddByUserId = Convert.ToString(Session["User"]));
                new hdlClassCourseSubject().save(ClassCourseSubject, iCompanyCode, iBranchCode, iClassCode, iCourseCode);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllClassCourseSubject();
        }

        [HttpPost]
        public ContentResult deleteClassCourseSubject(Session companySession)
        {
            try
            {
                //new hdlCompanySession().delete(companySession);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Content(JsonConvert.SerializeObject(new { error = ex.Message }));
            }
            return getAllClassCourseSubject();
        }
    }
}