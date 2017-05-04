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
    public class hdlHouse : DbContext
    {
        public hdlHouse()

            : base("name=ValencySGIEntities")
        { }

        public IList<House> SelectAll(string strValues)
        {
            string[] strArray = strValues.ToString().Split('/');

            int CompanyCode = Convert.ToInt32(strArray[0]);
            int BranchCode = Convert.ToInt32(strArray[1]);
        

            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.Houses.Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode).ToList();
        }

        public void save(House House)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    House.AddDateTime = DateTime.Now;
                    var entry = context.Entry(House);

                    if (entry != null)
                    {
                        if (House.HouseCode == 0)
                        {
                            House.HouseCode = Functions.getNextPk("House", House.HouseCode, House.CompanyCode, House.BranchCode);
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
        public void delete(House House)
        {
            try
            {
                var context = new SmsMisDB();
                context.Houses.Attach(House);
                var entry = context.Entry(House);
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
