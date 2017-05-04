using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlStudentFeeTermScholarship: DbContext
    {
        public hdlStudentFeeTermScholarship() : base("name=ValencySGIEntities") { }
        public DataTable SelectAll(int CompanyCode)
        {
            //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.StudentFeeTermScholarship.Where(s => s.CompanyCode == CompanyCode).ToList();
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //from p in db.StudentFixedDiscount.AsEnumerable()
            SqlConnection connection = null;
            try
            {
                connection = new hdlCommonMethods().GetConnection(SmsMis.Models.Console.Common.GlobalConstants.connectionString);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, string.Concat("SELECT f.FeeTermName, p.FeeTermCode,p.ScholarshipRate,d.CompanyCode,d.BranchCode,d.SessionCode,d.StudentNo,d.FullName,StudentRollNo = case when d.StudentRollNo IS null then cast(d.StudentNo as varchar) else d.StudentRollNo end,c.ClassName,IsNew = case when p.companycode IS NULL then cast(1 as bit) else cast(0  as bit) end from Student d inner join Class c on d.CompanyCode = c.CompanyCode and d.ClassCode = c.ClassCode left join StudentFeeTermScholarship p on p.CompanyCode = d.CompanyCode and p.BranchCode = d.BranchCode and p.SessionCode = d.SessionCode and p.StudentNo = d.StudentNo left join FeeTerm f on f.CompanyCode = p.CompanyCode and f.FeeTermCode = p.FeeTermCode where d.Status = 1 AND d.CompanyCode = ", CompanyCode));
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
        //public StudentFeeTermScholarship SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(StudentFeeTermScholarship StudentFeeTermScholarship, string userId,bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(StudentFeeTermScholarship);
                    if (entry != null)
                    {
                        StudentFeeTermScholarship.AddDateTime = DateTime.Now;
                        StudentFeeTermScholarship.AddByUserId = userId;

                        if (isNew)

                            entry.State = System.Data.Entity.EntityState.Added;
                        else entry.State = System.Data.Entity.EntityState.Modified;

                        context.SaveChanges();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //throw ex;
            }
            catch (Exception ex)
            {
                // throw ex;
            }
        }
        public void delete(StudentFeeTermScholarship StudentFeeTermScholarship)
        {
            try
            {
                var context = new SmsMisDB();
                context.StudentFeeTermScholarship.Attach(StudentFeeTermScholarship);
                var entry = context.Entry(StudentFeeTermScholarship);
                if (entry != null)
                {
                    entry.State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
                throw ex;
            }
        }
    }
}



