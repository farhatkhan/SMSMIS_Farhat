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
    public class hdlCOAGroup : DbContext
    {
        public hdlCOAGroup() : base("name=ValencySGIEntities") { }
        public IList<object> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var otherItems = (from p in db.COAGroup
                              from e in db.ChartOfAccounts.Where(e => e.CompanyCode == p.CompanyCode && e.AccountCode == p.AccountCode).DefaultIfEmpty()
                              select new
                              {
                                  CompanyCode = p.CompanyCode,
                                  AccountCode = p.AccountCode,
                                  AccountTitle = e.AccountTitle,
                                  Code = p.Code,
                                  Description = p.Description,
                                  ShortName = p.ShortName
                              }).Where(a => a.CompanyCode == CompanyCode).ToArray();
            return otherItems;
            //return db.COAGroup.Where(a => a.CompanyCode == CompanyCode).ToList();
        }

        //public IList<Branch> SelectBranch(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(IList<COAGroup> COAGroup, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {

                    if (COAGroup != null)
                    {
                        COAGroup.ToList<COAGroup>().ForEach(i => i.AddDateTime = DateTime.Now);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.AddByUserId = userId);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.CompanyCode = COAGroup[0].CompanyCode);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.Code = COAGroup[0].Code);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.Description = COAGroup[0].Description);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.ShortName = COAGroup[0].ShortName);

                        //if(COAGroup)
                        //Student.StudentNo = Functions.getNextPk("Student", Student.StudentNo, Student.CompanyCode, Student.BranchCode, Student.SessionCode);
                        foreach (var row in COAGroup)
                        {
                            context.COAGroup.Add(row);
                        }
                        //entry.State = EntityState.Added;
                        context.COAGroup.ToList<COAGroup>().Where(s => s.CompanyCode == COAGroup[0].CompanyCode && s.Code == COAGroup[0].Code ).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.COAGroup.ToList<COAGroup>().Where(s => s.CompanyCode == COAGroup[0].CompanyCode && s.Code == COAGroup[0].Code).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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
        public void delete(IList<COAGroup> COAGroup)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    //context.COAGroup.Attach(COAGroup);
                    //var entry = context.Entry(COAGroup);

                    if (COAGroup != null)
                    {

                        COAGroup.ToList<COAGroup>().ForEach(i => i.AddDateTime = DateTime.Now);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.AddByUserId = COAGroup[0].AddByUserId);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.CompanyCode = COAGroup[0].CompanyCode);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.Code = COAGroup[0].Code);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.Description = COAGroup[0].Description);
                        COAGroup.ToList<COAGroup>().ForEach(i => i.ShortName = COAGroup[0].ShortName);
                        
                        //entry.State = System.Data.Entity.EntityState.Deleted;
                        //foreach (var row in COAGroup)
                        //{
                        //    context.COAGroup.Add(row);
                        //}
                        context.COAGroup.ToList<COAGroup>().Where(s => s.CompanyCode == COAGroup[0].CompanyCode && s.Code == COAGroup[0].Code).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                        //}
                        //else
                        //    context.COAGroup.ToList<COAGroup>().Where(s => s.CompanyCode == COAGroup[0].CompanyCode && s.BranchCode == COAGroup[0].BranchCode && s.SessionCode == COAGroup[0].SessionCode && s.ClassCode == COAGroup[0].ClassCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
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
