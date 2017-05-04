using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlCSMaster : DbContext
    {
        private string SQL_CONN_STRING = GlobalConstants.connectionString;
        public hdlCSMaster() : base("name=ValencySGIEntities") { }

        public IList<CSDataAs> SelectAllCSDataAs()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CSDataAs.ToList();
        }
        public IList<CSFontStyle> SelectAllCSFontStyle()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CSFontStyle.ToList();
        }
        public IList<CSObjectBorder> SelectAllCSObjectBorder()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CSObjectBorder.ToList();
        }
        public IList<CSRowAction> SelectAllCSRowAction()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CSRowAction.ToList();
        }
        


        public IList<CSMaster> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CSMaster.Where(a => a.CompanyCode == CompanyCode).ToList();
        }
        public IList<CSMaster> SelectAll(int companycode, int ReportCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.CSMaster.Where(a => a.CompanyCode == companycode && a.ReportCode == ReportCode).ToList();
        }
        //public CSMaster SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(CSMaster CSMaster, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var mainentry = context.Entry(CSMaster);
                    if (mainentry != null)
                    {
                        int count = 0;
                        CSMaster.AddDateTime = DateTime.Now;
                        CSMaster.UserId = userId;
                        if (CSMaster.CSDetail != null && CSMaster.CSDetail.Count > 0)
                            foreach (var x in CSMaster.CSDetail)
                            {
                                x.SrNo = count;
                                //x.NoteCode = x.NoteCode == null ? string.Empty: x.NoteCode;
                                //x.FontStyle = x.FontStyle == null ? string.Empty : x.FontStyle;
                                //x.TopBorder = x.TopBorder == null ? string.Empty : x.TopBorder;
                                //x.BottomBorder = x.BottomBorder == null ? string.Empty : x.BottomBorder;
                                count++;
                            }
                        if (CSMaster.ReportCode == 0)
                        {
                            CSMaster.ReportCode = Functions.getNextPk("CSMaster", "ReportCode", string.Concat(" Where CompanyCode =", CSMaster.CompanyCode));
                            mainentry.State = System.Data.Entity.EntityState.Added;
                            context.CSMaster.Add(CSMaster);
                        }
                        else
                        {
                            if (CSMaster.CSDetail != null && CSMaster.CSDetail.Count > 0)
                            {
                                CSMaster.CSDetail.ToList().ForEach(i => { i.CompanyCode = CSMaster.CompanyCode; });
                                CSMaster.CSDetail.ToList().ForEach(i => { i.ReportCode = CSMaster.ReportCode; });

                            }
                            mainentry.State =System.Data.Entity.EntityState.Modified;

                        }
                        if (CSMaster.CSDetail != null && CSMaster.CSDetail.Count > 0)
                            CSMaster.CSDetail.ToList<CSDetail>().ForEach(entry => context.Entry(entry).State = System.Data.Entity.EntityState.Added);
                        context.CSDetail.ToList().Where(i => i.CompanyCode == CSMaster.CompanyCode && i.ReportCode == CSMaster.ReportCode).ToList<CSDetail>().ForEach(entry => context.Entry(entry).State = System.Data.Entity.EntityState.Deleted);
                        context.SaveChanges();
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }
        public int delete(CSMaster CSMaster)
        {
            try
            {
                using (var context = new SmsMisDB())
                {

                    
                    SqlConnection connection = null;
                    SqlParameter[] aParms = new SqlParameter[] { new SqlParameter("@CompanyCode", CSMaster.CompanyCode), new SqlParameter("@ReportCode", CSMaster.ReportCode) };
                    try
                    {
                        connection = GetConnection(SQL_CONN_STRING);
                        if (connection == null)
                            return -2;
                        int i = SqlHelper.ExecuteNonQuery(connection, CommandType.Text, "Delete from CSDetail where CompanyCode=@CompanyCode and reportCode = @ReportCode; Delete from CSMaster WHERE CompanyCode=@CompanyCode and reportCode = @ReportCode;", aParms);
                        return i;
                    }
                    catch (Exception ex)
                    {
                        throw new ExceptionTranslater(ex);
                    }
                    finally
                    {
                        if (connection != null && connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                    //var context = new SmsMisDB();

                    //var mainentry = context.Entry(CSMaster);
                    //if (mainentry != null)
                    //{
                    //    if (CSMaster.CSDetail != null && CSMaster.CSDetail.Count > 0)
                    //    {
                    //        CSMaster.CSDetail.ToList().ForEach(i => { i.CompanyCode = CSMaster.CompanyCode; });
                    //        CSMaster.CSDetail.ToList().ForEach(i => { i.ReportCode = CSMaster.ReportCode; });

                    //    }

                    //    //mainentry.State = EntityState.Deleted;
                    //    //if (CSMaster.CSDetail != null && CSMaster.CSDetail.Count > 0)
                    //    //CSMaster.CSDetail.ToList<CSDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

                    //    context.CSDetail.ToList().Where(i => i.CompanyCode == CSMaster.CompanyCode && i.ReportCode == CSMaster.ReportCode).ToList<CSDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
                    //    //context.VoucherD.ToList().Where(i => i.VoucherNo == CSMaster.VoucherNo).ToList<CSDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

                    //    context.CSMaster.Attach(CSMaster);
                    //    mainentry.State = EntityState.Deleted;
                    //    context.SaveChanges();
                    //}
                    //var entry = context.Entry(ChartOfAccounts);
                    //context.ChartOfAccounts.Remove(ChartOfAccounts);
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
        private SqlConnection GetConnection(string tstrConnectionString)
        {
            SqlConnection dbconnection = new SqlConnection(tstrConnectionString);
            try
            {
                dbconnection.Open();
                return dbconnection;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ExceptionTranslater(ex);
            }
        }
    }
}
