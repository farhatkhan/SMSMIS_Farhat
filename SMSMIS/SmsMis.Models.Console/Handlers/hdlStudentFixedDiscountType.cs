using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlStudentFixedDiscountType : DbContext
    {
        public hdlStudentFixedDiscountType() : base("name=ValencySGIEntities") { }
        public IList<StudentFixedDiscountType> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.StudentFixedDiscountTypes.Where(a => a.CompanyCode == CompanyCode).ToList();
        }
        public IList<StudentFixedDiscountType> SelectAllActive(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.StudentFixedDiscountTypes.Where(a => a.CompanyCode == CompanyCode && a.Status == true).ToList();
        }
        //public StudentFixedDiscountType SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(StudentFixedDiscountType StudentFixedDiscountType, string userId, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(StudentFixedDiscountType);
                    if (entry != null)
                    {
                        StudentFixedDiscountType.AddDateTime = DateTime.Now;
                        StudentFixedDiscountType.AddByUserId = userId;

                        if (isNew)
                        {
                            StudentFixedDiscountType.DiscountTypeCode = Functions.getNextPk("StudentFixedDiscountType", "DiscountTypeCode", string.Concat(" WHERE CompanyCode=", StudentFixedDiscountType.CompanyCode, " AND BranchCode=", StudentFixedDiscountType.BranchCode));

                            entry.State = EntityState.Added;
                        }
                        else entry.State = EntityState.Modified;

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
        public void delete(StudentFixedDiscountType StudentFixedDiscountType)
        {
            try
            {
                var context = new SmsMisDB();
                context.StudentFixedDiscountTypes.Attach(StudentFixedDiscountType);
                var entry = context.Entry(StudentFixedDiscountType);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
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



