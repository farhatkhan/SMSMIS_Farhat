using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Models;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlInstrumentType : DbContext
    {public hdlInstrumentType() : base("name=ValencySGIEntities") { }
    public IList<InstrumentType> SelectAll()
    {
        try
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.InstrumentType.ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
        public IList<InstrumentType> SelectAllActive()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.InstrumentType.Where(s=>s.Status==true).ToList();
        }
        public IList<InstrumentType> SelectAllActiveManageSerial()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.InstrumentType.Where(s => s.Status == true && s.ManageSerial == true).ToList();
        }
		public IList<InstrumentTypeVM> SelectAllApprove(int companyCode, int branchCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var InstrumentTypes = db.Database.SqlQuery<InstrumentTypeVM>("select CAST(RANK() over (order by AccountCode) AS int) as InstrumentTypeRowNumber, AccountCode,InstrumentTypeCode,InstrumentName,CompanyCode,BranchCode from lov_instrument")
                    .Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).ToList();
            return InstrumentTypes;
        }
        
        //public InstrumentType SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(InstrumentType InstrumentType, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(InstrumentType);
                    if (entry != null)
                    {
                        InstrumentType.AddDateTime = DateTime.Now;
                        InstrumentType.AddByUserId = userId;

                        if (InstrumentType.InstrumentCode == 0)
                        {
                            InstrumentType.InstrumentCode = Functions.getNextPk("InstrumentType", InstrumentType.InstrumentCode, 0);
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
        public void delete(InstrumentType InstrumentType)
        {
            try
            {
                var context = new SmsMisDB();
                context.InstrumentType.Attach(InstrumentType);
                var entry = context.Entry(InstrumentType);
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



