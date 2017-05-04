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
    public class hdlSeatType : DbContext
    {
        public hdlSeatType()

            : base("name=ValencySGIEntities")
        { }

        public IList<SeatType> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.SeatType.ToList();
        }

        public void save(SeatType SeatType)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    SeatType.AddDateTime = DateTime.Now;
                    var entry = context.Entry(SeatType);

                    if (entry != null)
                    {
                        if (SeatType.SeatTypeCode == 0)
                        {
                            SeatType.SeatTypeCode = Functions.getNextPk("SeatType", SeatType.SeatTypeCode, SeatType.CompanyCode);
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
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void delete(SeatType SeatType)
        {
            try
            {
                var context = new SmsMisDB();
                context.SeatType.Attach(SeatType);
                var entry = context.Entry(SeatType);
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
