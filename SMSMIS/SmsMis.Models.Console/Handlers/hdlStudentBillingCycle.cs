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
    public class hdlStudentBillingCycle: DbContext
    {
        public hdlStudentBillingCycle() : base("name=ValencySGIEntities") { }
        public DataTable SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            SqlConnection connection = null;
            try
            {
                connection = new hdlCommonMethods().GetConnection(SmsMis.Models.Console.Common.GlobalConstants.connectionString);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, string.Concat("SELECT p.BiilingCycle,d.CompanyCode,d.BranchCode,d.SessionCode,d.StudentNo,d.FullName,StudentRollNo = case when d.StudentRollNo IS null then cast(d.StudentNo as varchar) else d.StudentRollNo end,c.ClassName,IsNew = case when p.companycode IS NULL then cast(1 as bit) else cast(0  as bit) end from Student d inner join Class c on d.CompanyCode = c.CompanyCode and d.ClassCode = c.ClassCode left join StudentBillingCycle p on p.CompanyCode = d.CompanyCode and p.BranchCode = d.BranchCode and p.SessionCode = d.SessionCode and p.StudentNo = d.StudentNo where d.Status = 1 AND d.CompanyCode = ", CompanyCode));
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
        public StudentBillingCycle SelectAll(int CompanyCode,int branchcode,int sessioncode,int studentNo)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.StudentBillingCycle.Where(a => a.CompanyCode == CompanyCode && a.StudentNo == a.StudentNo && a.BranchCode == branchcode && a.SessionCode == sessioncode).FirstOrDefault();
        }
        public void save(StudentBillingCycle StudentBillingCycle, string userId,bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(StudentBillingCycle);
                    if (entry != null)
                    {
                        if (isNew)
                            
                            entry.State = System.Data.Entity.EntityState.Added;
                        else entry.State = System.Data.Entity.EntityState.Modified;

                        context.SaveChanges();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
        public void delete(StudentBillingCycle StudentBillingCycle)
        {
            try
            {
                var context = new SmsMisDB();
                context.StudentBillingCycle.Attach(StudentBillingCycle);
                var entry = context.Entry(StudentBillingCycle);
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




