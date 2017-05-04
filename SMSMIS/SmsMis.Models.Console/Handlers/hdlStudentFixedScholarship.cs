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
    public class hdlStudentFixedScholarship: DbContext
    {
        public hdlStudentFixedScholarship() : base("name=ValencySGIEntities") { }
        public DataTable SelectAll(int companycode)
        {
            //SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.StudentFixedScholarship.Where(s => s.CompanyCode == companycode).ToList();
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //from p in db.StudentFixedDiscount.AsEnumerable()
            SqlConnection connection = null;
            try
            {
                connection = new hdlCommonMethods().GetConnection(SmsMis.Models.Console.Common.GlobalConstants.connectionString);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, string.Concat("SELECT d.CompanyCode,d.BranchCode,d.SessionCode,d.StudentNo,Scholarship = case when p.ScholarshipPercentage = 0 then cast(cast(p.ScholarshipAmount as float) AS varchar) else cast(p.ScholarshipPercentage as varchar) + '%' end,p.ScholarshipAmount,p.ScholarshipPercentage,d.FullName,StudentRollNo = case when d.StudentRollNo IS null then cast(d.StudentNo as varchar) else d.StudentRollNo end,c.ClassName,IsNew = case when p.companycode IS NULL then cast(1 as bit) else cast(0  as bit) end from Student d inner join Class c on d.CompanyCode = c.CompanyCode and d.ClassCode = c.ClassCode left join StudentFixedScholarship p on p.CompanyCode = d.CompanyCode and p.BranchCode = d.BranchCode and p.SessionCode = d.SessionCode and p.StudentNo = d.StudentNo WHERE d.Status = 1 AND d.CompanyCode = ", companycode));
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
        //public StudentFixedScholarship SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(StudentFixedScholarship StudentFixedScholarship, string userId,bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(StudentFixedScholarship);
                    if (entry != null)
                    {
                        StudentFixedScholarship.AddDateTime = DateTime.Now;
                        StudentFixedScholarship.AddByUserId = userId;

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
        public void delete(StudentFixedScholarship StudentFixedScholarship)
        {
            try
            {
                var context = new SmsMisDB();
                context.StudentFixedScholarship.Attach(StudentFixedScholarship);
                var entry = context.Entry(StudentFixedScholarship);
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

