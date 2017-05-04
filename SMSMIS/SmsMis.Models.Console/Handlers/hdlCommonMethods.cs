using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using SmsMis.Models.Console.Admin;

namespace SmsMis.Models.Console.Handlers
{
    public class hdlCommonMethods : DbContext
    {

        public List<Bank> SelectByCompany(int companyID)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Bank.Where(s => s.CompanyCode == companyID).ToList();
        }
        
        #region Shahzeb Common

        //public DataTable SelectAllCompanies()
        //{
        //    // SqlConnection that will be used to execute the sql commands
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = GetConnection(SQL_CONN_STRING);
        //        DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, "SELECT * FROM Company with(nolock)");
        //        if (dtCompany.Rows.Count > 0)
        //        {
        //            return dtCompany;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExceptionTranslater(ex);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open)
        //            connection.Close();
        //    }
        //}

        //public DataTable SelectAllActiveCompany()
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = GetConnection(SQL_CONN_STRING);
        //        DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, "SELECT * FROM Company Where isnull(status,0) = 1");
        //        if (dtCompany.Rows.Count > 0)
        //        {
        //            return dtCompany;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExceptionTranslater(ex);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open)
        //            connection.Close();
        //    }
        //}

        //private string SQL_CONN_STRING = GlobalConstants.connectionString;
        //private SqlConnection GetConnection(string tstrConnectionString)
        //{
        //    SqlConnection dbconnection = new SqlConnection(tstrConnectionString);
        //    try
        //    {
        //        dbconnection.Open();
        //        return dbconnection;
        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        throw new ExceptionTranslater(ex);
        //    }
        //}

        //public IList<Branch> SelectAllBranches()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList();
        //}
        //public IList<Session> SelectAllSessions()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Session.ToList();
        //}
        //public IList<Session> SelectActiveSessions()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Session.Where(s => s.Status == true).ToList();
        //}

        //public IList<Branch> SelectAllActiveBranches()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.Where(s => s.Status == true).ToList();
        //}
        public IList<Branch> SelectAllActiveBranches(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Branch.Where(s => s.Status == true && s.CompanyCode == companycode).ToList();
        }

        //public IList<Class> SelectAllClasses()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Classes.ToList();
        //}
        //public IList<Class> SelectAllActiveClasses()//int CompanyCode
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Classes.Where(s => s.Status == true).ToList();
        //    //return db.Classes.Where(s => s.Status == true && s.CompanyCode == CompanyCode).ToList();
        //}

        //public IList<Course> SelectAllCourses()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Course.ToList();
        //}
        //public IList<Course> SelectAllActiveCourses()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Course.Where(s => s.Status == true).ToList();
        //}

        //public IList<Section> SelectAllSecions()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Section.ToList();
        //}
        //public IList<Section> SelectAllActiveSections()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Section.Where(s => s.Status == true).ToList();
        //}

        //public IList<Subject> SelectAllSubjects()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Subject.ToList();
        //}
        //public IList<Subject> SelectAllActiveSubjects()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Subject.Where(s => s.Status == true).ToList();
        //}

        //public DataTable SelectAllClassWithCompanyAndBranch()
        //{
        //    //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    //DataTable dt = new DbContext("name=ValencySGIEntities").Database.SqlQuery("Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,css.CourseCode,css.SubjectCode,co.CourseName,su.SubjectName,cl.ClassName from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode");

        //    DataTable dt = new DataTable();
        //    var context = new SmsMisDB();
        //    var conn = context.Database.Connection;
        //    var connectionState = conn.State;
        //    try
        //    {
        //        using (context)
        //        {
        //            if (connectionState != ConnectionState.Open)
        //                conn.Open();
        //            using (var cmd = conn.CreateCommand())
        //            {
        //                //cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode,cl.ClassName,css.Mandatory from ClassCourseSubject css with(nolock) inner join Course co with(nolock) on css.CourseCode = co.CourseCode and css.CompanyCode = co.CompanyCode inner join [Subject] su with(nolock) on css.SubjectCode = su.SubjectCode inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode where co.Status = 1 and su.Status =1 and cl.Status = 1";
        //                cmd.CommandText = "Select distinct css.CompanyCode,css.BranchCode,css.ClassCode, cl.ClassName from ClassCourseSubject css with(nolock) inner join Class cl with(nolock) on cl.ClassCode = css.ClassCode and cl.CompanyCode = css.CompanyCode";
        //                //cmd.CommandText = "Select * from ClassCourseSubject";
        //                cmd.CommandType = CommandType.Text;
        //                //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    dt.Load(reader);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connectionState != ConnectionState.Open)
        //            conn.Close();
        //    }
        //    return dt;
        //}
        public DataTable SelectAllCompanies()
        {
            // SqlConnection that will be used to execute the sql commands
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(SQL_CONN_STRING);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, "SELECT * FROM Company with(nolock)");
                if (dtCompany.Rows.Count > 0)
                {
                    return dtCompany;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ExceptionTranslater(ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public DataTable SelectAllActiveCompany()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection(SQL_CONN_STRING);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "prGetAllActiveCompanies");
                if (dtCompany.Rows.Count > 0)
                {
                    return dtCompany;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ExceptionTranslater(ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public string SQL_CONN_STRING = GlobalConstants.connectionString;
        public SqlConnection GetConnection(string tstrConnectionString)
        {
            SqlConnection dbconnection = new SqlConnection(tstrConnectionString);
            try
            {
                dbconnection.Open();
                return dbconnection;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ExceptionTranslater(ex);
            }
        }

        public IList<Branch> SelectAllBranches()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Branch.ToList();
        }
        public IList<Session> SelectAllSessions()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Session.ToList();
        }
        public IList<Session> SelectActiveSessions()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Session.Where(s => s.Status == true).ToList();
        }

        public IList<Branch> SelectAllActiveBranches()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Branch.Where(s => s.Status == true).ToList();
        }

        public IList<Class> SelectAllClasses()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Classes.ToList();
        }
        public IList<Class> SelectAllActiveClasses()//int CompanyCode
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Classes.Where(s => s.Status == true).ToList();
            //return db.Classes.Where(s => s.Status == true && s.CompanyCode == CompanyCode).ToList();
        }
        public IList<Class> SelectAllActiveClasses(int CompanyCode)//
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Classes.Where(s => s.Status == true && s.CompanyCode == CompanyCode).ToList();
            //return db.Classes.Where(s => s.Status == true && s.CompanyCode == CompanyCode).ToList();
        }
        public IList<Course> SelectAllCourses()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Course.ToList();
        }
        public IList<Course> SelectAllActiveCourses()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Course.Where(s => s.Status == true).ToList();
        }

        public IList<Section> SelectAllSecions()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Section.ToList();
        }
        public IList<Section> SelectAllActiveSections()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Section.Where(s => s.Status == true).ToList();
        }

        public IList<Subject> SelectAllSubjects()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Subject.ToList();
        }

        public IList<Subject> SelectAllActiveSubjects()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Subject.Where(s => s.Status == true).ToList();
        }

        public DataTable SelectAllStudentActiveSubjects()
        {
            //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.Subject.Where(s => s.Status == true).ToList();

            //Select ss.SubjectCode,ss.SubjectName,ccs.CompanyCode,ccs.BranchCode,ccs.ClassCode,ccs.CourseCode from Subject ss with(nolock) inner join ClassCourseSubject ccs with(nolock) on ss.SubjectCode=ccs.SubjectCode Where ss.Status=1

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

                        cmd.CommandText = "Select ss.SubjectCode,ss.SubjectName,ccs.CompanyCode,ccs.BranchCode,ccs.ClassCode,ccs.CourseCode from Subject ss with(nolock) inner join ClassCourseSubject ccs with(nolock) on ss.SubjectCode=ccs.SubjectCode Where ss.Status=1";
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

        public DataTable SelectAllClassWithCompanyAndBranch()
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
        #endregion

        public IList<Grade> SelectAllActiveGrades()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Grade.Where(a => a.Status == true).ToList();
        }
        public IList<Employee> SelectAllActiveEmployee()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Employee.Where(a => a.Status == true).ToList();
        }
        public IList<Session> SelectActiveSessions(int companyid)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Session.Where(s => s.Status == true && s.CompanyCode == companyid).ToList();
        }
        
    }
}
