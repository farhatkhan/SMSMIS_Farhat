using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;
using System.Data;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class hdlClassCourseSubject : DbContext
    {
        public hdlClassCourseSubject()

            : base("name=ValencySGIEntities")
        { }

        //public IList<ClassCourseSubject> SelectAll()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.ClassCourseSubject.ToList();
        //}


        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            var context = new SmsMisDB();
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Select ccs.*,st.SubjectName from ClassCourseSubject ccs with(nolock) inner join Subject st with(nolock) on ccs.SubjectCode = st.SubjectCode";
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public DataTable SelectAllClass()
        {
            //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //DataTable dt = new DbContext("name=ValencySGIEntities").Database.SqlQuery("Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode");

            DataTable dt = new DataTable();
            var context = new SmsMisDB();
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode where co.Status = 1 and su.Status =1 and cl.Status = 1";
                        cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode, cl.ClassName from ClassCourseSubject css with(nolock) inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode";
                        //cmd.CommandText = "Select * from ClassCourseSubject";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }
        public DataTable SelectAllClassCourse()
        {
            //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //DataTable dt = new DbContext("name=ValencySGIEntities").Database.SqlQuery("Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode");

            DataTable dt = new DataTable();
            var context = new SmsMisDB();
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode where co.Status = 1 and su.Status =1 and cl.Status = 1";
                        cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,co.CourseName from ClassCourseSubject css with(nolock) inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode";
                        //cmd.CommandText = "Select * from ClassCourseSubject";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }
        public DataTable getAllClassCourseSubjects()
        {
            DataTable dt = new DataTable();
            var context = new SmsMisDB();
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode "; //where co.Status = 1 and su.Status =1 and cl.Status = 1
                        //cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.Mandatory, cl.ClassName from ClassCourseSubject css with(nolock) inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode";
                        //cmd.CommandText = "Select * from ClassCourseSubject";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getAllStudentClassCourseSubjects()
        {
            DataTable dt = new DataTable();
            var context = new SmsMisDB();
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName,css.Mandatory,ss.StudentNo from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode left outer join StudentSubject ss with(nolock) on ss.CompanyCode =css.CompanyCode and ss.ClassCode =cl.ClassCode and ss.SubjectCode=su.SubjectCode";
                        cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable SelectAllClassCourseSubjects(int CompanyCode)
        {
            //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //DataTable dt = new DbContext("name=ValencySGIEntities").Database.SqlQuery("Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode");

            DataTable dt = new DataTable();
            var context = new SmsMisDB();
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        //cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode where co.Status = 1 and su.Status =1 and cl.Status = 1 AND css.CompanyCode = " + CompanyCode;
                        cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode where co.Status = 1 and su.Status =1 and cl.Status = 1 AND css.CompanyCode = " + CompanyCode;
                        //cmd.CommandText = "Select * from ClassCourseSubject";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public void save(List<ClassCourseSubject> ClassCourseSubject, int iCompanyCode, int iBranchCode, int iClassCode, int iCourseCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (ClassCourseSubject != null)
                    {
                        ClassCourseSubject.ToList<ClassCourseSubject>().ForEach(i => i.AddDateTime = DateTime.Now);

                        foreach (var row in ClassCourseSubject)
                        {
                            context.ClassCourseSubject.Add(row);
                        }
                        context.ClassCourseSubject.ToList<ClassCourseSubject>().Where(s => s.CompanyCode == iCompanyCode && s.BranchCode == iBranchCode && s.ClassCode == iClassCode && s.CourseCode == iCourseCode).ToList().ForEach(entry2 => context.Entry(entry2).State = System.Data.Entity.EntityState.Deleted);
                    }
                    else
                        context.ClassCourseSubject.ToList<ClassCourseSubject>().Where(s => s.CompanyCode == iCompanyCode && s.BranchCode == iBranchCode && s.ClassCode == iClassCode && s.CourseCode == iCourseCode).ToList().ForEach(entry2 => context.Entry(entry2).State = System.Data.Entity.EntityState.Deleted);

                    context.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
