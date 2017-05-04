using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;
using System.Data.SqlClient;
using SmsMis.Models.Console.ViewModel;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlVoucherType: DbContext
    {
        public hdlVoucherType() : base("name=ValencySGIEntities") { }
        public IList<VoucherType> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.VoucherType.ToList();
        }
        public IList<VoucherType> SelectAll(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.VoucherType.Where(a => a.CompanyCode == companyCode).ToList();
        }

        public IList<VoucherTypeVM> SelectAllApproved(int companyCode, int branchCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            
            IList<VoucherTypeVM> voucherTypes = db.Database.SqlQuery<VoucherTypeVM>("select CAST( RANK() over (order by AccountCode,VoucherCode ) AS int) as VoucherTypeRowNumber, VoucherCode,VoucherName,AccountCode,AccountTitle,TransactionType, Category,Frequency,CompanyCode,BranchCode,CurrencyName, CurrencyCode, LocalCurrencyCode, CurrencySymbol, LocalCurrencySymbol from LOV_VoucherType")
                .Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).ToList();
            
            return voucherTypes;
            
        }
        public IList<VoucherType> SelectAllApproved(int companyCode,string exclude)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.VoucherType.Where(a => a.CompanyCode == companyCode && a.Status == true && a.Category != exclude).ToList();
        }

        public void save(VoucherType VoucherType, string userId, int CompanyCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(VoucherType);
                    if (entry != null)
                    {
                        VoucherType.AddDateTime = DateTime.Now;
                        VoucherType.AddByUserId = userId;

                        if (VoucherType.VoucherCode == 0)
                        {
                            VoucherType.VoucherCode = Functions.getNextPk("VoucherType", "VoucherType.VoucherCode", " WHERE VoucherType.CompanyCode = " + VoucherType.CompanyCode);

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
        public void delete(VoucherType VoucherType)
        {
            try
            {
                var context = new SmsMisDB();
                //context.VoucherType.Attach(VoucherType);
                var entry = context.Entry(VoucherType);
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
