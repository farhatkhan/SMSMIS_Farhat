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
    public class hdlDocType : DbContext
    {
        public hdlDocType()

            : base("name=ValencySGIEntities")
        { }

        public IList<DocType> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.DocType.ToList();
        }

        public void save(DocType DocType)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    DocType.AddDateTime = DateTime.Now;
                    var entry = context.Entry(DocType);

                    if (entry != null)
                    {
                        if (DocType.DocCode == 0)
                        {
                            DocType.DocCode = Functions.getNextPk("DocType", DocType.DocCode, 0);
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
        public void delete(DocType DocType)
        {
            try
            {
                var context = new SmsMisDB();
                context.DocType.Attach(DocType);
                var entry = context.Entry(DocType);
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
