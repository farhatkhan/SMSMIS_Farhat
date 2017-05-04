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
    public class hdlCostCenter : DbContext
    {
        public hdlCostCenter() : base("name=ValencySGIEntities") { }
        public IList<CostCenter> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CostCenter.Where(s => s.CompanyCode == CompanyCode).ToList();
        }

        public IList<CostCenter> SelectAllCostCenterBranch(int CompanyCode, int BranchCode)
        {
            SmsMisDB db = new SmsMisDB();
            var costCenterCodes = db.CostCenterBranch.Where(x => x.CompanyCode == CompanyCode && x.BranchCode == BranchCode).Select(x => x.CostCenterCode).ToList();
            return db.CostCenter.Where(x => costCenterCodes.Contains(x.CostCenterCode)).ToList();
        }


        
        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(CostCenter CostCenter, string userId, int CompanyCode)
        {
            try
            {

                using (var context = new SmsMisDB())
                {
                    var mainentry = context.Entry(CostCenter);
                    if (mainentry != null)
                    {
                        CostCenter.AddDateTime = DateTime.Now;
                        CostCenter.AddByUserId = userId;
                        CostCenter.CompanyCode = CompanyCode;
                        if (CostCenter.CostCenterCode == 0)
                        {
                            CostCenter.CostCenterCode = Functions.getNextPk("CostCenter", "CostCenterCode", string.Concat(" Where CompanyCode =", CostCenter.CompanyCode));
                            mainentry.State = EntityState.Added;
                            context.CostCenter.Add(CostCenter);
                        }
                        else
                        {
                            if (CostCenter.CostCenterBranch != null && CostCenter.CostCenterBranch.Count > 0)
                            {
                                CostCenter.CostCenterBranch.ToList().ForEach(i => { i.CompanyCode = CostCenter.CompanyCode; });
                                //CostCenter.CostCenterBranch.ToList().ForEach(i => { i.BranchCode = CostCenter.BranchCode; });
                                CostCenter.CostCenterBranch.ToList().ForEach(i => { i.CostCenterCode = CostCenter.CostCenterCode; });
                                //CostCenter.CostCenterBranch.ToList().ForEach(i => { i.VoucherCode = CostCenter.VoucherCode; });

                            }
                            mainentry.State = EntityState.Modified;
                        }
                        //if(CostCenter)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        //foreach (var row in CostCenter)
                        if (CostCenter.CostCenterBranch != null && CostCenter.CostCenterBranch.Count > 0)
                            CostCenter.CostCenterBranch.ToList<CostCenterBranch>().ForEach(entry => context.Entry(entry).State = EntityState.Added);
                        context.CostCenterBranch.ToList().Where(i => i.CostCenterCode == CostCenter.CostCenterCode && i.CompanyCode == CostCenter.CompanyCode).ToList<CostCenterBranch>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
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
        public void delete(IList<CostCenter> CostCenter)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (CostCenter != null)
                    {
                        CostCenter.ToList<CostCenter>().ForEach(i => i.AddDateTime = DateTime.Now);
                        CostCenter.ToList<CostCenter>().ForEach(i => i.AddByUserId = CostCenter[0].AddByUserId);
                        CostCenter.ToList<CostCenter>().ForEach(i => i.CompanyCode = CostCenter[0].CompanyCode);

                        foreach (var row in CostCenter)
                        {
                            context.CostCenter.Add(row);
                        }
                        context.CostCenter.ToList<CostCenter>().Where(s => s.CompanyCode == CostCenter[0].CompanyCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.CostCenter.ToList<CostCenter>().Where(s => s.CompanyCode == CostCenter[0].CompanyCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
