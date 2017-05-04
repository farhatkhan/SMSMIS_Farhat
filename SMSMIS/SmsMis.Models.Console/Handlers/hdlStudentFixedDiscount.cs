using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlStudentFixedDiscount : DbContext
    {
        public hdlStudentFixedDiscount() : base("name=ValencySGIEntities") { }
        public DataTable SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //from p in db.StudentFixedDiscount.AsEnumerable()
            SqlConnection connection = null;
            try
            {
                connection = new hdlCommonMethods().GetConnection(SmsMis.Models.Console.Common.GlobalConstants.connectionString);
                DataTable dtCompany = SqlHelper.ExecuteDataTable(connection, CommandType.Text, string.Concat("SELECT p.DiscountTypeCode,d.CompanyCode,d.BranchCode,d.SessionCode,d.StudentNo,Discounts = case when p.DiscountPercentage = 0 then cast(cast(p.DiscountAmount as float) AS varchar) else cast(p.DiscountPercentage as varchar) + '%' end,p.DiscountAmount,p.DiscountPercentage,d.FullName,StudentRollNo = case when d.StudentRollNo IS null then cast(d.StudentNo as varchar) else d.StudentRollNo end,c.ClassName,p.AddByUserId,IsNew = case when p.companycode IS NULL then cast(1 as bit) else cast(0  as bit) end from Student d inner join Class c on d.CompanyCode = c.CompanyCode and d.ClassCode = c.ClassCode left join StudentFixedDiscount p on p.CompanyCode = d.CompanyCode and p.BranchCode = d.BranchCode and p.SessionCode = d.SessionCode and p.StudentNo = d.StudentNo where d.Status = 1 AND d.CompanyCode = ", companycode));
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
            //var otherItems = (from d in db.Student
            //                  join c in db.Classes on d.CompanyCode equals c.CompanyCode where d.ClassCode == c.ClassCode
            //                  from p in db.StudentFixedDiscount.Where(b => b.CompanyCode == d.CompanyCode && b.BranchCode == d.BranchCode && b.SessionCode == d.SessionCode && b.StudentNo == d.StudentNo).DefaultIfEmpty()
            //                  select new
            //                  {
            //                      CompanyCode = d.CompanyCode,
            //                      BranchCode = d.BranchCode,
            //                      SessionCode = d.SessionCode,
            //                      StudentNo = p.StudentNo,
            //                      //Discounts = p.DiscountPercentage == 0 ? Convert.ToString(p.DiscountAmount) : string.Concat(p.DiscountPercentage,"%"),
            //                      DiscountAmount = p.DiscountAmount,
            //                      DiscountPercentage = p.DiscountPercentage,
            //                      FullName = d.FullName,
            //                      //StudentRollNo = string.IsNullOrEmpty(d.StudentRollNo) ? Convert.ToString(d.StudentNo) : d.StudentRollNo,
            //                      ClassName = c.ClassName,
            //                      AddByUserId = p.AddByUserId ,
            //                      //AddDateTime = p.AddDateTime
            //                  }).Where(a => a.CompanyCode == companycode).ToArray();
            //return otherItems;

            //return db.StudentFixedDiscount.Where(s=>s.CompanyCode == companycode).ToList();
        }
        //public StudentFixedDiscount SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(StudentFixedDiscount StudentFixedDiscount, string userId, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(StudentFixedDiscount);
                    if (entry != null)
                    {
                        StudentFixedDiscount.AddDateTime = DateTime.Now;
                        StudentFixedDiscount.AddByUserId = userId;

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
        public void delete(StudentFixedDiscount StudentFixedDiscount)
        {
            try
            {
                var context = new SmsMisDB();
                context.StudentFixedDiscount.Attach(StudentFixedDiscount);
                var entry = context.Entry(StudentFixedDiscount);
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

