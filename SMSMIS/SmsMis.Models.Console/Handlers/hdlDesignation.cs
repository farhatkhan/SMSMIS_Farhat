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
    public class hdlDesignation : DbContext
    {
        public hdlDesignation()

            : base("name=ValencySGIEntities")
        { }

        public IList<Designation> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Designation.Where(a => a.CompanyCode == companycode).ToList();
        }

        public void save(Designation Designation, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Designation.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Designation);

                    if (entry != null)
                    {
                        if (isNew)
                        {
                            Designation.DesignationCode = Functions.getNextPk("Designation", Designation.DesignationCode, Designation.CompanyCode);
                            if (Designation.DesignationBranch != null && Designation.DesignationBranch.Count > 0)
                                Designation.DesignationBranch.ToList().ForEach(i => { i.DesignationCode = Designation.DesignationCode; });
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            if (Designation.DesignationBranch != null && Designation.DesignationBranch.Count > 0)
                                Designation.DesignationBranch.ToList().ForEach(i => { i.DesignationCode = Designation.DesignationCode; });

                            entry.State = EntityState.Modified;
                        }



                        if (Designation.DesignationBranch != null && Designation.DesignationBranch.Count > 0)
                            Designation.DesignationBranch.ToList<DesignationBranch>().ForEach(x => context.Entry(x).State = EntityState.Added);

                        context.DesignationBranch.ToList().Where(i => i.DesignationCode == Designation.DesignationCode && i.CompanyCode == Designation.CompanyCode).ToList<DesignationBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);

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
        public void delete(Designation Designation)
        {
            try
            {
                var context = new SmsMisDB();
                context.Designation.Attach(Designation);
                var entry = context.Entry(Designation);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.DesignationBranch.ToList().Where(i => i.DesignationCode == Designation.DesignationCode && i.CompanyCode == Designation.CompanyCode).ToList<DesignationBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
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
