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
    public class hdlType : DbContext
    {
        public hdlType()

            : base("name=ValencySGIEntities")
        { }

        public IList<SmsMis.Models.Console.Admin.Type> SelectAll(int companycode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Type.Where(a => a.CompanyCode == companycode).ToList();
        }

        public void save(SmsMis.Models.Console.Admin.Type Type, bool isNew)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    Type.AddDateTime = DateTime.Now;
                    var entry = context.Entry(Type);

                    if (entry != null)
                    {
                        if (isNew)
                        {
                            Type.TypeCode = Functions.getNextPk("Type", Type.TypeCode, Type.CompanyCode);
                            if (Type.TypeBranch != null && Type.TypeBranch.Count > 0)
                                Type.TypeBranch.ToList().ForEach(i => { i.TypeCode = Type.TypeCode; });
                            entry.State = EntityState.Added;
                        }
                        else
                        {
                            if (Type.TypeBranch != null && Type.TypeBranch.Count > 0)
                                Type.TypeBranch.ToList().ForEach(i => { i.TypeCode = Type.TypeCode; });

                            entry.State = EntityState.Modified;
                        }



                        if (Type.TypeBranch != null && Type.TypeBranch.Count > 0)
                            Type.TypeBranch.ToList<TypeBranch>().ForEach(x => context.Entry(x).State = EntityState.Added);

                        context.TypeBranch.ToList().Where(i => i.TypeCode == Type.TypeCode && i.CompanyCode == Type.CompanyCode).ToList<TypeBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);

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
        public void delete(SmsMis.Models.Console.Admin.Type Type)
        {
            try
            {
                var context = new SmsMisDB();
                context.Type.Attach(Type);
                var entry = context.Entry(Type);
                if (entry != null)
                {
                    entry.State = EntityState.Deleted;
                    context.TypeBranch.ToList().Where(i => i.TypeCode == Type.TypeCode && i.CompanyCode == Type.CompanyCode).ToList<TypeBranch>().ForEach(s => context.Entry(s).State = EntityState.Deleted);
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
