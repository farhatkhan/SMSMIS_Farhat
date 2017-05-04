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
    public class hdlCompanySession : DbContext
    {
        public hdlCompanySession()

            : base("name=ValencySGIEntities")
        { }

        //public IList<Session> SelectAll()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Session.ToList();
        //}
        //public IList<Session> SelectActiveSession()
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Session.Where(s=>s.Status == true).ToList();
        //}
        public IList<Session> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Session.Where(s => s.CompanyCode == companycode).ToList();
        }

        public void save(Session companySession)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    companySession.AddDateTime = DateTime.Now;
                    var entry = context.Entry(companySession);

                    if (entry != null)
                    {
                        if (companySession.SessionCode == 0)
                        {
                            companySession.SessionCode = Functions.getNextPk("Session", companySession.SessionCode,companySession.CompanyCode);
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            entry.State = EntityState.Modified;
                        }
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
        public void delete(Session companySession)
        {
            try
            {
                var context = new SmsMisDB();
                context.Session.Attach(companySession);
                var entry = context.Entry(companySession);
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
        //public comBranch SelectByID(System.Guid BranchID)
        //{
        //    comBranch branch = new SGIValencyDB().comBranchList.Find(BranchID);
        //    return branch;
        //}
    }
}
