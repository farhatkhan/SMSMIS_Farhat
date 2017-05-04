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
    public class hdlFeeReceipt : DbContext
    {
        public hdlFeeReceipt() : base("name=ValencySGIEntities") { }
        public IList<FeeReceipt> SelectAll()
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeReceipt.ToList();
        }
        public IList<FeeReceipt> SelectAll(int companycode,int BranchCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeReceipt.Where(s => s.CompanyCode == companycode && s.BranchCode == BranchCode).ToList();
            
        }
        public IList<object> SelectAll(int CompanyCode, int BranchCode, int SessionCode, int ReceiptNo)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var otherItems = (from fr in db.FeeReceipt
                              join fee in db.FeeBillMasters on fr.CompanyCode equals fee.CompanyCode
                              where fr.BranchCode == fee.BranchCode && fr.SessionCode == fee.SessionCode && fr.ChallanNo == fee.ChallanNo
                              join st in db.Student on fee.CompanyCode equals st.CompanyCode
                              where st.BranchCode == fee.BranchCode && st.SessionCode == fee.SessionCode && st.StudentNo == fee.StudentNo
                              
                              join clas in db.Classes on st.CompanyCode equals clas.CompanyCode
                              where clas.ClassCode == fee.ClassCode
                              join session in db.Session on st.CompanyCode equals session.CompanyCode
                              where session.SessionCode == fee.SessionCode
                              select new
                              {
                                  ReceiptDate = fr.ReceiptDate,
                                  ReceiptNo=fr.ReceiptNo,ReceivedAt = 
                                  fr.ReceivedAt,InstrumentCode = fr.InstrumentCode,
                                  InstrumentDate = fr.InstrumentDate,
                                  InstrumentNo = fr.InstrumentNo,
                                  Remarks = fr.Remarks,
                                  OutstandingAmount = fr.OutstandingAmount,
                                  WaivedAmount = fr.WaivedAmount,
                                  StudentNo = st.StudentNo,
                                  FullName = st.FullName,
                                  ChallanNo = fr.ChallanNo,
                                  SessionName = session.SessionName,
                                  ClassName = clas.ClassName,
                                  TotalAmount = fee.TotalAmount,
                                  DiscountAmount = fee.DiscountAmount,
                                  CompanyCode = fr.CompanyCode,
                                  BranchCode = fr.BranchCode,
                                  SessionCode = fr.SessionCode,
                              }).Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.ReceiptNo == ReceiptNo).ToArray();
            return otherItems;
        }
        //public FeeReceipt SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}

        public void save(FeeReceipt FeeReceipt, string userId)
        {
            try
            {
                using (var context = new SmsMisDB())
                {
                    var entry = context.Entry(FeeReceipt);
                    if (entry != null)
                    {
                        FeeReceipt.AddDateTime = DateTime.Now;
                        FeeReceipt.AddByUserId = userId;
                        if (FeeReceipt.ReceiptNo == 0)
                        {
                            FeeReceipt.ReceiptNo = Functions.getNextPk("FeeReceipt", "ReceiptNo", string.Concat(" Where CompanyCode = ", FeeReceipt.CompanyCode, " AND BranchCode = ", FeeReceipt.BranchCode));
                            entry.State = EntityState.Added;
                        }
                        else entry.State = EntityState.Modified;

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
        public void delete(FeeReceipt FeeReceipt)
        {
            try
            {
                var context = new SmsMisDB();
                context.FeeReceipt.Attach(FeeReceipt);
                var entry = context.Entry(FeeReceipt);
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
