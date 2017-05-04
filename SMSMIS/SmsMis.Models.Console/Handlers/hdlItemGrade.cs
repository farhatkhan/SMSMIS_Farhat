using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlItemGrade : DbContext
    {
        public hdlItemGrade() : base("name=ValencySGIEntities") { }
        public IList<ItemGrade> SelectAll(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemGrade.Where(a => a.CompanyCode == companyCode).ToList();
        }
        public IList<ItemGrade> SelectAllApproved(int companyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.ItemGrade.Where(a => a.CompanyCode == companyCode && a.Status == true).ToList();
        }

        public void save(ItemGrade ItemGrade, string userId, int CompanyCode)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(ItemGrade);
                    if (entry != null)
                    {
                        ItemGrade.AddDateTime = DateTime.Now;
                        ItemGrade.AddByUserId = userId;

                        if (ItemGrade.GradeCode == 0)
                        {
                            ItemGrade.GradeCode = Functions.getNextPk("ItemGrade", "ItemGrade.GradeCode", " WHERE ItemGrade.CompanyCode = " + ItemGrade.CompanyCode);

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
        public void delete(ItemGrade ItemGrade)
        {
            try
            {
                var context = new SmsMisDB();
                //context.ItemGrade.Attach(ItemGrade);
                var entry = context.Entry(ItemGrade);
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
