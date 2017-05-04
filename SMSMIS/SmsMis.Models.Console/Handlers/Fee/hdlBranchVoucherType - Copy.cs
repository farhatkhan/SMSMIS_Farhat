using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlBranchVoucherType: DbContext
    {
        public hdlBranchVoucherType() : base("name=ValencySGIEntities") { }
        public IList<BranchVoucherType> SelectAll()
        {
            SmsMis.Models.Console.Client.ClientContext db = new SmsMis.Models.Console.Client.ClientContext();
            return db.BranchVoucherType.ToList();
        }
        public void save(IList<BranchVoucherType> BranchVoucherType, string userId, int CompanyCode, int BranchCode)
        {
            try
            {
                using (var context = new ClientContext())
                {
                    if (BranchVoucherType != null)
                    {
                        BranchVoucherType.ToList<BranchVoucherType>().ForEach(i => i.AddDateTime = DateTime.Now);
                        BranchVoucherType.ToList<BranchVoucherType>().ForEach(i => i.AddByUserId = userId);
                        BranchVoucherType.ToList<BranchVoucherType>().ForEach(i => i.CompanyCode = CompanyCode);
                        BranchVoucherType.ToList<BranchVoucherType>().ForEach(i => i.BranchCode = BranchCode);
                        foreach (var row in BranchVoucherType)
                        {
                            context.BranchVoucherType.Add(row);
                        }
                        context.BranchVoucherType.ToList<BranchVoucherType>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                        
                    }
                    else
                        context.BranchVoucherType.ToList<BranchVoucherType>().Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
        public void delete(IList<BranchVoucherType> BranchVoucherType)
        {
            try
            {
                using (var context = new ClientContext())
                {
                    if (BranchVoucherType != null)
                    {
                        BranchVoucherType.ToList<BranchVoucherType>().ForEach(i => i.AddDateTime = DateTime.Now);

                        foreach (var row in BranchVoucherType)
                        {
                            context.BranchVoucherType.Add(row);
                        }
                        context.BranchVoucherType.ToList<BranchVoucherType>().Where(s => s.CompanyCode == BranchVoucherType[0].CompanyCode && s.BranchCode == BranchVoucherType[0].BranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = System.Data.Entity.EntityState.Deleted);
                    }
                    else
                        context.BranchVoucherType.ToList<BranchVoucherType>().Where(s => s.CompanyCode == BranchVoucherType[0].CompanyCode && s.BranchCode == BranchVoucherType[0].BranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = System.Data.Entity.EntityState.Deleted);

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
