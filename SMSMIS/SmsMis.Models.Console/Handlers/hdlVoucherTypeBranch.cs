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
    public class hdlVoucherTypeBranch: DbContext
    {
        public hdlVoucherTypeBranch() : base("name=ValencySGIEntities") { } 
        public IList<object> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var otherItems = (from p in db.VoucherTypeBranch
                              from e in db.ChartOfAccounts.Where(e => e.CompanyCode == p.CompanyCode && e.AccountCode == p.AccountCode).DefaultIfEmpty()
                              //from rate in db.COABranch.Where(b => b.CompanyCode == p.CompanyCode && b.AccountCode == p.AccountCode).DefaultIfEmpty()
                              select new
                              {
                                  CompanyCode = p.CompanyCode,
                                  BranchCode = p.BranchCode,
                                  VoucherCode = p.VoucherCode,
                                  Status = p.Status,
                                  AccountCode = p.AccountCode,
                                  AccountTitle = e.AccountTitle
                              }).Where(a => a.CompanyCode == CompanyCode ).ToArray();
            return otherItems;//.Where(z => z.BranchCode == null || z.BranchCode == BranchCode).ToArray();
            //return otherItems;
            //return db.VoucherTypeBranch.Where(a => a.CompanyCode == CompanyCode).ToList();
        }
        
        public void save(IList<VoucherTypeBranch> VoucherTypeBranch, string userId, int CompanyCode, int BranchCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (VoucherTypeBranch != null)
                    {
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.AddDateTime = DateTime.Now);
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.AddByUserId = userId);
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.CompanyCode = CompanyCode);
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.BranchCode = BranchCode);
                        foreach (var row in VoucherTypeBranch)
                        {
                            context.VoucherTypeBranch.Add(row);
                        }
                        context.VoucherTypeBranch.ToList<VoucherTypeBranch>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.VoucherCode == VoucherTypeBranch[0].VoucherCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                        
                    }
                    else
                        context.VoucherTypeBranch.ToList<VoucherTypeBranch>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.VoucherCode == VoucherTypeBranch[0].VoucherCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
        public void delete(IList<VoucherTypeBranch> VoucherTypeBranch)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (VoucherTypeBranch != null)
                    {
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.AddDateTime = DateTime.Now);
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.CompanyCode = VoucherTypeBranch[0].CompanyCode);
                        VoucherTypeBranch.ToList<VoucherTypeBranch>().ForEach(i => i.BranchCode = VoucherTypeBranch[0].BranchCode);

                        context.VoucherTypeBranch.ToList<VoucherTypeBranch>().Where(s => s.CompanyCode == VoucherTypeBranch[0].CompanyCode && s.BranchCode == VoucherTypeBranch[0].BranchCode && s.VoucherCode == VoucherTypeBranch[0].VoucherCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

                    }
                    

                    context.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
