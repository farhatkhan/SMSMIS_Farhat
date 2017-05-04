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
    public class hdlAnalysisType : DbContext
    {
        public hdlAnalysisType() : base("name=ValencySGIEntities") { }
        public IList<AnalysisType> SelectAllCompanyAnalysisType(int CompanyCode)
        {
            SmsMisDB db = new SmsMisDB();
            return db.AnalysisType.Where(s => s.CompanyCode == CompanyCode).ToList();
        }

        public IList<AnalysisType> SelectAllBranchAnalysisType(int CompanyCode, int BranchCode)
        {
            SmsMisDB db = new SmsMisDB();
            var analysisTypeCodes = db.AnalysisTypeBranch.Where(x => x.CompanyCode == CompanyCode && x.BranchCode == BranchCode).Select(x => x.AnalysisTypeCode).ToList();
            return db.AnalysisType.Where(x => analysisTypeCodes.Contains(x.AnalysisTypeCode)).ToList();
        }
        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(AnalysisType AnalysisType, string userId, int CompanyCode)
        {
            try
            {

                using (var context = new SmsMisDB())
                {
                    var mainentry = context.Entry(AnalysisType);
                    if (mainentry != null)
                    {
                        AnalysisType.AddDateTime = DateTime.Now;
                        AnalysisType.AddByUserId = userId;
                        AnalysisType.CompanyCode = CompanyCode;
                        if (AnalysisType.AnalysisTypeCode == 0)
                        {
                            AnalysisType.AnalysisTypeCode = Functions.getNextPk("AnalysisType", "AnalysisTypeCode", string.Concat(" Where CompanyCode =", AnalysisType.CompanyCode));
                            mainentry.State = EntityState.Added;
                            context.AnalysisType.Add(AnalysisType);
                        }
                        else
                        {
                            if (AnalysisType.AnalysisTypeBranch != null && AnalysisType.AnalysisTypeBranch.Count > 0)
                            {
                                AnalysisType.AnalysisTypeBranch.ToList().ForEach(i => { i.CompanyCode = AnalysisType.CompanyCode; });
                                //AnalysisType.AnalysisTypeBranch.ToList().ForEach(i => { i.BranchCode = AnalysisType.BranchCode; });
                                AnalysisType.AnalysisTypeBranch.ToList().ForEach(i => { i.AnalysisTypeCode = AnalysisType.AnalysisTypeCode; });
                                //AnalysisType.AnalysisTypeBranch.ToList().ForEach(i => { i.VoucherCode = AnalysisType.VoucherCode; });

                            }
                            mainentry.State = EntityState.Modified;
                        }
                        //if(AnalysisType)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        //foreach (var row in AnalysisType)
                        if (AnalysisType.AnalysisTypeBranch != null && AnalysisType.AnalysisTypeBranch.Count > 0)
                            AnalysisType.AnalysisTypeBranch.ToList<AnalysisTypeBranch>().ForEach(entry => context.Entry(entry).State = EntityState.Added);
                        context.AnalysisTypeBranch.ToList().Where(i => i.AnalysisTypeCode == AnalysisType.AnalysisTypeCode && i.CompanyCode == AnalysisType.CompanyCode).ToList<AnalysisTypeBranch>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
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
        public void delete(IList<AnalysisType> AnalysisType)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (AnalysisType != null)
                    {
                        AnalysisType.ToList<AnalysisType>().ForEach(i => i.AddDateTime = DateTime.Now);
                        AnalysisType.ToList<AnalysisType>().ForEach(i => i.AddByUserId = AnalysisType[0].AddByUserId);
                        AnalysisType.ToList<AnalysisType>().ForEach(i => i.CompanyCode = AnalysisType[0].CompanyCode);

                        foreach (var row in AnalysisType)
                        {
                            context.AnalysisType.Add(row);
                        }
                        context.AnalysisType.ToList<AnalysisType>().Where(s => s.CompanyCode == AnalysisType[0].CompanyCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.AnalysisType.ToList<AnalysisType>().Where(s => s.CompanyCode == AnalysisType[0].CompanyCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
