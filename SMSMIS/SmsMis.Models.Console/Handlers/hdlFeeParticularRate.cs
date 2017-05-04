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
    public class hdlFeeParticularRate : DbContext
    {
        public hdlFeeParticularRate() : base("name=ValencySGIEntities") { }
        public IList<FeeParticularRate> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeParticularRate.ToList();
        }
        
        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(IList<FeeParticularRate> FeeParticularRate, string userId, int CompanyCode, int BranchCode, int SessionCode, int ClassCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {

                    if (FeeParticularRate != null)
                    {
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.AddDateTime = DateTime.Now);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.AddByUserId = userId);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.CompanyCode = CompanyCode);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.BranchCode = BranchCode);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.SessionCode = SessionCode);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.ClassCode = ClassCode);
                        //if(FeeParticularRate)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        foreach (var row in FeeParticularRate)
                        {
                            context.FeeParticularRate.Add(row);
                        }
                        //entry.State = EntityState.Added;
                        context.FeeParticularRate.ToList<FeeParticularRate>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode && s.ClassCode == ClassCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.FeeParticularRate.ToList<FeeParticularRate>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode && s.ClassCode == ClassCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
        public void delete(IList<FeeParticularRate> FeeParticularRate)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    //context.FeeParticularRate.Attach(FeeParticularRate);
                    //var entry = context.Entry(FeeParticularRate);

                    if (FeeParticularRate != null)
                    {

                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.AddDateTime = DateTime.Now);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.AddByUserId = FeeParticularRate[0].AddByUserId);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.CompanyCode = FeeParticularRate[0].CompanyCode);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.BranchCode = FeeParticularRate[0].BranchCode);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.SessionCode = FeeParticularRate[0].SessionCode);
                        FeeParticularRate.ToList<FeeParticularRate>().ForEach(i => i.ClassCode = FeeParticularRate[0].ClassCode);
                        //entry.State = System.Data.Entity.EntityState.Deleted;
                        //foreach (var row in FeeParticularRate)
                        //{
                        //    context.FeeParticularRate.Add(row);
                        //}
                        context.FeeParticularRate.ToList<FeeParticularRate>().Where(s => s.CompanyCode == FeeParticularRate[0].CompanyCode && s.BranchCode == FeeParticularRate[0].BranchCode && s.SessionCode == FeeParticularRate[0].SessionCode && s.ClassCode == FeeParticularRate[0].ClassCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                        //}
                        //else
                        //    context.FeeParticularRate.ToList<FeeParticularRate>().Where(s => s.CompanyCode == FeeParticularRate[0].CompanyCode && s.BranchCode == FeeParticularRate[0].BranchCode && s.SessionCode == FeeParticularRate[0].SessionCode && s.ClassCode == FeeParticularRate[0].ClassCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
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
