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
    public class hdlChartOfAccounts : DbContext
    {
        public hdlChartOfAccounts() : base("name=ValencySGIEntities") { }
        public IList<ChartOfAccounts> SelectCodes(int CompanyCode,/*string AccountType,*/ string levelID)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ChartOfAccounts.ToList().Where(s => s.CompanyCode == CompanyCode /*&& s.AccountType == AccountType*/ && s.LevelId == levelID && s.Status == true).ToList();
        }
        public IList<COABranch> SelectBranchCodes(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.COABranch.ToList().Where(s => s.CompanyCode == CompanyCode).ToList();
        }
        public IList<Object> SelectCodes(int CompanyCode,int BranchCode, string AccountType, string levelID)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var otherItems = (from st in db.ChartOfAccounts
                              from rate in db.COABranch.Where(b => b.CompanyCode == st.CompanyCode && b.AccountCode == st.AccountCode).DefaultIfEmpty()
                              select new
                              {
                                  BranchCode = (int?) rate.BranchCode,
                                  AccountCode = st.AccountCode,
                                  AccountTitle = st.AccountTitle,
                                  AccountType = st.AccountType,
                                  LevelId = st.LevelId,
                                  CompanyCode = st.CompanyCode,
                                  Status = st.Status

                              }).Where(s => s.CompanyCode == CompanyCode && s.LevelId == levelID && s.AccountType == AccountType ).ToArray();
            return otherItems.Where(z=>z.BranchCode == null || z.BranchCode == BranchCode).ToArray();
        }
        public IList<Object> SelectCodes(int CompanyCode, int BranchCode, string AccountType)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var otherItems = (from st in db.ChartOfAccounts
                              from rate in db.COABranch.Where(b => b.CompanyCode == st.CompanyCode && b.AccountCode == st.AccountCode).DefaultIfEmpty()
                              select new
                              {
                                  BranchCode = (int?)rate.BranchCode,
                                  AccountCode = st.AccountCode,
                                  AccountTitle = st.AccountTitle,
                                  AccountType = st.AccountType,
                                  LevelId = st.LevelId,
                                  CompanyCode = st.CompanyCode,
                                  Status = st.Status

                              }).Where(s => s.CompanyCode == CompanyCode && s.AccountType == AccountType).ToArray();
            return otherItems.Where(z => z.BranchCode == null || z.BranchCode == BranchCode).ToArray();
        }
        public IList<object> SelectAllActiveCOA(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.ChartOfAccounts.ToList().Where(s => s.CompanyCode == CompanyCode).ToList();


            var otherItems = (from p in db.ChartOfAccounts
                              //from e in db.COABranch.Where(e => e.CompanyCode == p.CompanyCode ).DefaultIfEmpty()
                              select new
                              {
                                  CompanyCode = p.CompanyCode,
                                  AccountCode = p.AccountCode,
                                  AccountTitle = p.AccountTitle
                              }).Where(a => a.CompanyCode == CompanyCode).ToArray();
            return otherItems;
        }
        public IList<object> SelectAllCOA(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            //return db.ChartOfAccounts.ToList().Where(s => s.CompanyCode == CompanyCode).ToList();


            var otherItems = (from p in db.ChartOfAccounts
                              join d in db.COALevels on p.CompanyCode equals d.CompanyCode
                              //from e in db.COABranch.Where(e => e.CompanyCode == p.CompanyCode ).DefaultIfEmpty()
                              where d.LevelId == p.LevelId
                              select new
                              {
                                  CompanyCode = p.CompanyCode,
                                  AccountCode = p.AccountCode,
                                  AccountTitle = p.AccountTitle,
                                  ShortName = p.ShortName,
                                  Remarks = p.Remarks,
                                  LevelId = p.LevelId,
                                  AccountType = p.AccountType,
                                  ParentAccountCode = p.ParentAccountCode,
                                  Status = p.Status,
                                  AddByUserId = p.AddByUserId,
                                  AddDateTime = p.AddDateTime,
                                  LevelColor = d.LevelColor
                              }).Where(a => a.CompanyCode == CompanyCode).ToArray();
            return otherItems;
        }
        public void save(ChartOfAccounts ChartOfAccounts, string userId, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var mainentry = context.Entry(ChartOfAccounts);
                    if (mainentry != null)
                    {
                        ChartOfAccounts.AddDateTime = DateTime.Now;
                        ChartOfAccounts.AddByUserId = userId;
                        if (isNew)
                        {
                            //ChartOfAccounts.VoucherCode = Functions.getNextPk("VoucherType", ChartOfAccounts.VoucherCode, ChartOfAccounts.CompanyCode);
                            mainentry.State = EntityState.Added;
                            context.ChartOfAccounts.Add(ChartOfAccounts);
                        }
                        else mainentry.State = EntityState.Modified;
                        if (ChartOfAccounts.COABranch != null && ChartOfAccounts.COABranch.Count > 0)
                            ChartOfAccounts.COABranch.ToList<COABranch>().ForEach(entry => context.Entry(entry).State = EntityState.Added);
                        context.COABranch.ToList().Where(i => i.CompanyCode == ChartOfAccounts.CompanyCode && i.AccountCode == ChartOfAccounts.AccountCode).ToList<COABranch>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
                        context.SaveChanges();
                        Bank bnk = new hdlBank().SelectBankByAccountCode(ChartOfAccounts.AccountCode, ChartOfAccounts.CompanyCode);
                        if (bnk != null && ChartOfAccounts.AccountType != "B")
                            new hdlBank().delete(bnk);
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



        public void delete(ChartOfAccounts ChartOfAccounts)
        {
            using (var context = new SmsMisDB())
            {

                //var context = new SmsMisDB();
                Bank bnk= new hdlBank().SelectBankByAccountCode(ChartOfAccounts.AccountCode, ChartOfAccounts.CompanyCode);
                
                context.ChartOfAccounts.Attach(ChartOfAccounts);
                var entry = context.Entry(ChartOfAccounts);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.COABranch.ToList().Where(i => i.CompanyCode == ChartOfAccounts.CompanyCode && i.AccountCode == ChartOfAccounts.AccountCode).ToList<COABranch>().ForEach(s => context.Entry(s).State = System.Data.Entity.EntityState.Deleted);
                    context.SaveChanges();
                    if(bnk != null )
                    new hdlBank().delete(bnk);
                }
                //var entry = context.Entry(ChartOfAccounts);
                //context.ChartOfAccounts.Remove(ChartOfAccounts);
            }

        }
    }
}
