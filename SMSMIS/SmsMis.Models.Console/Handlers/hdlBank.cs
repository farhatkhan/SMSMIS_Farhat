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
    public class hdlBank : DbContext
    {
        public hdlBank() : base("name=ValencySGIEntities") { }
        public IList<Bank> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Bank.ToList();
        }
        public Bank SelectBankByAccountCode(string AccountCode,int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Bank.Where(s => s.AccountCode == AccountCode && s.CompanyCode == CompanyCode).FirstOrDefault();
        }
        public void save(Bank Bank, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(Bank);
                    if (entry != null)
                    {
                        //Bank.AddDateTime = DateTime.Now;
                        //Bank.UserId = userId;
                        Bank bank = SelectBankByAccountCode(Bank.AccountCode, Bank.CompanyCode);
                        if (bank == null)
                        {
                            //Bank.BankCode = Functions.getNextPk("Bank", "BankCode", " WHERE CompanyCode = "+ Bank.CompanyCode); 

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
        public void delete(Bank Bank)
        {
            try
            {
                var context = new SmsMisDB();
                context.Bank.Attach(Bank);
                var entry = context.Entry(Bank);
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



