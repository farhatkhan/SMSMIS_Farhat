using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlStudentOptionalFeeParticular : DbContext
    {
        public hdlStudentOptionalFeeParticular() : base("name=ValencySGIEntities") { }
        public IList<StudentOptionalFeeParticular> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.StudentOptionalFeeParticular.Where(s=>s.CompanyCode == CompanyCode).ToList();
        }
        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(IList<StudentOptionalFeeParticular> StudentOptionalFeeParticular, string userId, int CompanyCode, int BranchCode, int SessionCode, int StudentNo)
        {
            try
            {
                using (var context = new SmsMisDB())
                {

                    if (StudentOptionalFeeParticular != null)
                    {
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.AddDateTime = DateTime.Now);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.AddByUserId = userId);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.CompanyCode = CompanyCode);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.BranchCode = BranchCode);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.SessionCode = SessionCode);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.StudentNo = StudentNo);
                        //if(StudentOptionalFeeParticular)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        foreach (var row in StudentOptionalFeeParticular)
                        {
                            context.StudentOptionalFeeParticular.Add(row);
                        }
                        //entry.State = EntityState.Added;
                        context.StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode && s.StudentNo == StudentNo).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode && s.StudentNo == StudentNo).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

                    context.SaveChanges();
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
        public void delete(IList<StudentOptionalFeeParticular> StudentOptionalFeeParticular)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (StudentOptionalFeeParticular != null)
                    {
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.AddDateTime = DateTime.Now);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.AddByUserId = StudentOptionalFeeParticular[0].AddByUserId);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.CompanyCode = StudentOptionalFeeParticular[0].CompanyCode);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.BranchCode = StudentOptionalFeeParticular[0].BranchCode);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.SessionCode = StudentOptionalFeeParticular[0].SessionCode);
                        StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().ForEach(i => i.StudentNo = StudentOptionalFeeParticular[0].StudentNo);
                        foreach (var row in StudentOptionalFeeParticular)
                        {
                            context.StudentOptionalFeeParticular.Add(row);
                        }
                        context.StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().Where(s => s.CompanyCode == StudentOptionalFeeParticular[0].CompanyCode && s.BranchCode == StudentOptionalFeeParticular[0].BranchCode && s.SessionCode == StudentOptionalFeeParticular[0].SessionCode && s.StudentNo == StudentOptionalFeeParticular[0].StudentNo).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.StudentOptionalFeeParticular.ToList<StudentOptionalFeeParticular>().Where(s => s.CompanyCode == StudentOptionalFeeParticular[0].CompanyCode && s.BranchCode == StudentOptionalFeeParticular[0].BranchCode && s.SessionCode == StudentOptionalFeeParticular[0].SessionCode && s.StudentNo == StudentOptionalFeeParticular[0].StudentNo).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
