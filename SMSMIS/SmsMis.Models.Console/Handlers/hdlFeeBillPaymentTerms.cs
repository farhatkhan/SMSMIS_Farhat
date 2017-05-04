using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlFeeBillPaymentTerms : DbContext
    {
        public hdlFeeBillPaymentTerms() : base("name=ValencySGIEntities") { }
        
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public IList<FeeBillPaymentTerms> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeBillPaymentTerms.ToList();
        }
        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(IList<FeeBillPaymentTerms> FeeBillPaymentTerms, string userId, int CompanyCode, string BillType)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (FeeBillPaymentTerms != null)
                    {
                        FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().ForEach(i => i.AddDateTime = DateTime.Now);
                        FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().ForEach(i => i.AddByUserId = userId);
                        FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().ForEach(i => i.CompanyCode = CompanyCode);
                        FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().ForEach(i => i.BillType = BillType);

                        foreach (var row in FeeBillPaymentTerms)
                        {
                            context.FeeBillPaymentTerms.Add(row);
                        }
                        context.FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().Where(s => s.CompanyCode == CompanyCode && s.BillType == BillType).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                        
                    }
                    else
                        context.FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().Where(s => s.CompanyCode == CompanyCode && s.BillType == BillType).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
        public void delete(IList<FeeBillPaymentTerms> FeeBillPaymentTerms)
        {
            try
            {

                using (var context = new SmsMisDB()) 
                {
                    // var mainentry = context.Entry(FeeBillPaymentTerms);
                    //if (mainentry != null)
                    //{
                    if (FeeBillPaymentTerms != null)
                    {
                        FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().ForEach(i => i.AddDateTime = DateTime.Now);

                        //foreach (var row in FeeBillPaymentTerms)
                        //{
                        //    context.FeeBillPaymentTerms.Add(row);
                        //}
                        //mainentry.State = EntityState.Deleted;
                        context.FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().Where(s => s.CompanyCode == FeeBillPaymentTerms[0].CompanyCode && s.BillType == FeeBillPaymentTerms[0].BillType).ToList().ForEach(entry2 => context.Entry(entry2).State = System.Data.Entity.EntityState.Deleted);
                    }
                    else
                        context.FeeBillPaymentTerms.ToList<FeeBillPaymentTerms>().Where(s => s.CompanyCode == FeeBillPaymentTerms[0].CompanyCode && s.BillType == FeeBillPaymentTerms[0].BillType).ToList().ForEach(entry2 => context.Entry(entry2).State = System.Data.Entity.EntityState.Deleted);

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
