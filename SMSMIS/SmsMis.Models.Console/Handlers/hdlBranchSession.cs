using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Admin;

namespace SmsMis.Models.Console.Handlers.Admin
{
    public class hdlBranchSession : DbContext
    {
        public hdlBranchSession()

            : base("name=ValencySGIEntities")
        { }

        public IList<BranchSession> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.BranchSession.ToList();
        }

        public void save(List<BranchSession> branchSession, int iCompanyCode, int iBranchCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (branchSession != null)
                    {
                        branchSession.ToList<BranchSession>().ForEach(i => i.AddDateTime = DateTime.Now);

                        foreach (var row in branchSession)
                        {
                            context.BranchSession.Add(row);
                        }
                        context.BranchSession.ToList<BranchSession>().Where(s => s.CompanyCode == branchSession[0].CompanyCode && s.BranchCode == branchSession[0].BranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    else
                        context.BranchSession.ToList<BranchSession>().Where(s => s.CompanyCode == iCompanyCode && s.BranchCode == iBranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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

        public void delete(List<BranchSession> branchSession, int iCompanyCode, int iBranchCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    if (branchSession != null)
                    {
                        branchSession.ToList<BranchSession>().ForEach(i => i.AddDateTime = DateTime.Now);
                        context.BranchSession.ToList<BranchSession>().Where(s => s.CompanyCode == branchSession[0].CompanyCode && s.BranchCode == branchSession[0].BranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);
                    }
                    //else
                    //    context.BranchSession.ToList<BranchSession>().Where(s => s.CompanyCode == iCompanyCode && s.BranchCode == iBranchCode).ToList().ForEach(entry2 => context.Entry(entry2).State = EntityState.Deleted);

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

    }
}
