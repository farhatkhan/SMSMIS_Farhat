using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Common;
using SmsMis.Models.Console.Handlers.Admin;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlVoucherMaster : DbContext
    {
        public hdlVoucherMaster() : base("name=ValencySGIEntities") { }


        public IList<VoucherMaster> SelectAll()
        {
            using (SmsMisDB db = new SmsMisDB())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<VoucherMaster> voucherMaster = db.VoucherMaster.ToList();
                return voucherMaster;
            }


        }
        public IList<VoucherMaster> SelectAll(int CompanyCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var voucherMasters = db.VoucherMaster.Where(a => a.CompanyCode == CompanyCode).OrderBy(v => v.VoucherNo).ToList();
            return voucherMasters;
        }
        public IList<VoucherMaster> SelectAll(int companycode, int branchcode, int voucherCode, int voucherNo, DateTime voucherDate)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.VoucherMaster.Where(a => a.CompanyCode == companycode && a.BranchCode == branchcode && a.VoucherCode == voucherCode && a.VoucherNo == voucherNo && a.VoucherDate == voucherDate).ToList();
        }
        //public VoucherMaster SelectByIDs(int companyID)
        //{
        //    SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
        //    return db.Branch.ToList().Where(s => s.CompanyCode == companyID).ToList();
        //}
        //Start Farhat Ullah
        public bool ValidateInstrumntNumber(int companyCode, int branchCode, string accountCode, int instrumentCode, int instrumentNo)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var val = db.InstrumentSerialDetail.Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode
                                                       && x.AccountCode == accountCode && x.InstrumentTypeCode == instrumentCode && x.InstrumentNo == instrumentNo).FirstOrDefault();
            if (val != null)
                return true;
            return false;
        }
        //End By Farhat Ullah
        public int getVoucherNumber(int companyCode, int branchCode, int voucherTypeCode, DateTime voucherDate)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            var voucherNumberList = db.VoucherMaster.Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).ToList();
            int voucherNumber = voucherNumberList.Count.Equals(0) ? 1 : voucherNumberList.Max(x => x.VoucherNo);
            //int voucherNumber = db.VoucherMaster.Where(x => x.CompanyCode == companyCode && x.BranchCode == branchCode).Max(x => x.VoucherNo);
            var voucherFrequency = db.VoucherType.FirstOrDefault(x => x.VoucherCode == voucherTypeCode).Frequency;
            voucherNumber = 0;
            if (voucherFrequency == "Y") //Yearly
            {
                DateTime fromDate;
                DateTime toDate;
                var fiscalYear =
                    db.FiscalYear.FirstOrDefault(e => e.CompanyCode == companyCode && e.BranchCode == branchCode);
                fromDate = fiscalYear.StartDate;
                toDate = fiscalYear.EndDate;
                DateTime dt = System.DateTime.Now;
                var voucherNo =
                    db.VoucherMaster.Where(
                        x =>
                            x.CompanyCode == companyCode && x.BranchCode == branchCode && x.VoucherDate >= fromDate &&
                            voucherDate <= toDate).ToList();
                if (voucherNo != null && voucherNo.Count > 0)
                {
                    voucherNumber = voucherNo.Max(e => e.VoucherNo);
                }
                voucherNumber = voucherNumber > 0 ? voucherNumber + 1 : 1;
            }
            else if (voucherFrequency.Equals("M")) //Monthly
            {
                //Farhat Ullah
                voucherNumber = db.VoucherMaster.Where(
                    x =>
                        x.CompanyCode == companyCode && x.BranchCode == branchCode &&
                        x.VoucherDate.Month == voucherDate.Month && x.VoucherDate.Year == voucherDate.Year)
                    .Max(x => x.VoucherNo);
                voucherNumber = voucherNumber > 0 ? voucherNumber + 1 : 1;
            }
            else //Continuously
            {
                voucherNumber++;
            }
            return voucherNumber;
        }

        public void save(VoucherMaster VoucherMaster, string userId)
        {
            /*START - By Fakhar*/
            VoucherMaster.ModuleCode = -1;
            VoucherMaster.FormCode = -1;
            var voucherCode = VoucherMaster.VoucherNo;
            userId = "-1";

            using (SmsMisDB db = new SmsMisDB())
            {
                try
                {
                    DateTime voucherDate = System.DateTime.Now;
                    var chk = db.VoucherMaster.Where(x => x.VoucherNo == voucherCode).Select(x => x.VoucherNo).ToList();
                    if (chk.Count > 0)
                    {
                        if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
                        {
                            VoucherMaster voucherMaster = db.VoucherMaster.Where(vm => vm.VoucherNo == VoucherMaster.VoucherNo).FirstOrDefault();
                            List<VoucherDetail> lstVoucherDetail = VoucherMaster.VoucherDetail.ToList();
                            List<VoucherDetail> voucherDetail = voucherMaster.VoucherDetail.Where(vm => vm.VoucherNo == voucherMaster.VoucherNo).ToList();
                            // VoucherMaster objVoucherMaster = new VoucherMaster();
                            voucherMaster.AddByUserId = "1";//VoucherMaster.AddByUserId;
                            voucherMaster.AddDateTime = System.DateTime.Now.Date;// VoucherMaster.AddDateTime;
                            voucherMaster.BranchCode = VoucherMaster.BranchCode;
                            voucherMaster.CBCode = VoucherMaster.CBCode;
                            voucherMaster.CompanyCode = VoucherMaster.CompanyCode;
                            voucherMaster.CurrencyCode = VoucherMaster.CurrencyCode;
                            voucherMaster.ExchangeRate = VoucherMaster.ExchangeRate;
                            voucherMaster.FormCode = VoucherMaster.FormCode;
                            voucherMaster.InstrumentCode = VoucherMaster.InstrumentCode;
                            voucherMaster.InstrumentDate = VoucherMaster.InstrumentDate;
                            voucherMaster.InstrumentNo = VoucherMaster.InstrumentNo;
                            voucherMaster.ModuleCode = VoucherMaster.ModuleCode;
                            voucherMaster.PartyCode = VoucherMaster.PartyCode;
                            voucherMaster.Remarks = VoucherMaster.Remarks;
                            voucherMaster.SubParty = VoucherMaster.SubParty;
                            voucherMaster.VoucherCode = VoucherMaster.VoucherCode;
                            voucherMaster.VoucherDate = VoucherMaster.VoucherDate;
                            voucherDate = VoucherMaster.VoucherDate;
                            voucherMaster.VoucherNo = VoucherMaster.VoucherNo;
                            db.VoucherMaster.Attach(voucherMaster);
                            db.Entry(voucherMaster).State = EntityState.Modified;
                            //db.VoucherMaster.Add(voucherMaster);
                            int serialNo = 1;
                            List<VoucherDetail> obj = new List<VoucherDetail>();

                            for (int i = 0; i < voucherDetail.Count; i++)
                            {
                                voucherDetail[i].CompanyCode = voucherMaster.CompanyCode;
                                voucherDetail[i].BranchCode = voucherMaster.BranchCode;
                                voucherDetail[i].VoucherDate = voucherMaster.VoucherDate;
                                voucherDetail[i].VoucherNo = voucherMaster.VoucherNo;
                                voucherDetail[i].VoucherDate = voucherDate;
                                voucherDetail[i].SrNo = serialNo;
                                voucherDetail[i].AccountCode = lstVoucherDetail[i].AccountCode;
                                voucherDetail[i].Narration = lstVoucherDetail[i].Narration;
                                voucherDetail[i].CCCode = lstVoucherDetail[i].CCCode;
                                voucherDetail[i].EmployeeCode = lstVoucherDetail[i].EmployeeCode;
                                voucherDetail[i].ProjectCode = lstVoucherDetail[i].ProjectCode;
                                voucherDetail[i].AnalysisCode = lstVoucherDetail[i].AnalysisCode;
                                voucherDetail[i].FC_Debit = lstVoucherDetail[i].FC_Debit;
                                voucherDetail[i].FC_Credit = lstVoucherDetail[i].FC_Credit;
                                voucherDetail[i].LC_Debit = lstVoucherDetail[i].LC_Debit;
                                voucherDetail[i].LC_Credit = lstVoucherDetail[i].LC_Credit;
                                serialNo++;
                                obj.Add(voucherDetail[i]);
                                db.VoucherDetail.Attach(voucherDetail[i]);
                                db.Entry(voucherDetail[i]).State = EntityState.Modified;
                                //db.VoucherDetail.Add(item);
                            }

                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        List<VoucherDetail> lstVoucherDetail = VoucherMaster.VoucherDetail.ToList();
                        List<VoucherDetail> obj = new List<VoucherDetail>();
                        if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
                        {
                            VoucherMaster objVoucherMaster = new VoucherMaster();
                            objVoucherMaster.AddByUserId = "1";//VoucherMaster.AddByUserId;
                            objVoucherMaster.AddDateTime = System.DateTime.Now.Date;// VoucherMaster.AddDateTime;
                            objVoucherMaster.BranchCode = VoucherMaster.BranchCode;
                            objVoucherMaster.CBCode = VoucherMaster.CBCode;
                            objVoucherMaster.CompanyCode = VoucherMaster.CompanyCode;
                            objVoucherMaster.CurrencyCode = VoucherMaster.CurrencyCode;
                            objVoucherMaster.ExchangeRate = VoucherMaster.ExchangeRate;
                            objVoucherMaster.FormCode = VoucherMaster.FormCode;
                            objVoucherMaster.InstrumentCode = VoucherMaster.InstrumentCode;
                            objVoucherMaster.InstrumentDate = Convert.ToDateTime(VoucherMaster.VoucherDate);
                            objVoucherMaster.InstrumentNo = VoucherMaster.InstrumentNo;
                            objVoucherMaster.ModuleCode = VoucherMaster.ModuleCode;
                            objVoucherMaster.PartyCode = VoucherMaster.PartyCode;
                            objVoucherMaster.Remarks = VoucherMaster.Remarks;
                            objVoucherMaster.SubParty = VoucherMaster.SubParty;
                            objVoucherMaster.VoucherCode = VoucherMaster.VoucherCode;
                            objVoucherMaster.VoucherDate = Convert.ToDateTime(VoucherMaster.VoucherDate);
                            objVoucherMaster.VoucherNo = VoucherMaster.VoucherNo;
                            db.Entry(objVoucherMaster).State = EntityState.Added;
                            db.VoucherMaster.Add(objVoucherMaster);
                            //db.SaveChanges();
                            int serialNo = 1;
                            for (int i = 0; i < lstVoucherDetail.Count; i++)
                            {
                                lstVoucherDetail[i].CompanyCode = VoucherMaster.CompanyCode;
                                lstVoucherDetail[i].BranchCode = VoucherMaster.BranchCode;
                                lstVoucherDetail[i].VoucherCode = VoucherMaster.VoucherCode;
                                lstVoucherDetail[i].VoucherNo = VoucherMaster.VoucherNo;
                                lstVoucherDetail[i].VoucherDate = Convert.ToDateTime(VoucherMaster.VoucherDate);
                                lstVoucherDetail[i].SrNo = serialNo;
                                lstVoucherDetail[i].AccountCode = lstVoucherDetail[i].AccountCode;
                                lstVoucherDetail[i].Narration = lstVoucherDetail[i].Narration;
                                lstVoucherDetail[i].CCCode = lstVoucherDetail[i].CCCode;
                                lstVoucherDetail[i].EmployeeCode = lstVoucherDetail[i].EmployeeCode;
                                lstVoucherDetail[i].ProjectCode = lstVoucherDetail[i].ProjectCode;
                                lstVoucherDetail[i].AnalysisCode = lstVoucherDetail[i].AnalysisCode;
                                lstVoucherDetail[i].FC_Debit = lstVoucherDetail[i].FC_Debit;
                                lstVoucherDetail[i].FC_Credit = lstVoucherDetail[i].FC_Credit;
                                lstVoucherDetail[i].LC_Debit = lstVoucherDetail[i].LC_Debit;
                                lstVoucherDetail[i].LC_Credit = lstVoucherDetail[i].LC_Credit;
                                serialNo++;
                                obj.Add(lstVoucherDetail[i]);
                                db.VoucherDetail.Attach(lstVoucherDetail[i]);
                                db.Entry(lstVoucherDetail[i]).State = EntityState.Added;
                                db.VoucherDetail.Add(lstVoucherDetail[i]);
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //try
            //{
            //    //using (var context = new SmsMisDB())
            //    //{
            //    //    //var mainentry = context.Entry(VoucherMaster);
            //    //    //if (mainentry != null)
            //    //    //{
            //    //        int count = 0;
            //    //        VoucherMaster.AddDateTime = DateTime.Now;
            //    //        VoucherMaster.AddByUserId = userId;
            //    //        if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
            //    //            foreach (var x in VoucherMaster.VoucherDetail)
            //    //            {
            //    //                x.SrNo = count;
            //    //                count++;
            //    //            }
            //    //        if (VoucherMaster.VoucherNo == 0)
            //    //        {
            //    //            VoucherMaster.VoucherNo = Functions.getNextPk("VoucherMaster", "VoucherNo", string.Concat(" Where CompanyCode =", VoucherMaster.CompanyCode, " AND BranchCode=", VoucherMaster.BranchCode, " AND voucherCode=", VoucherMaster.VoucherCode, " AND year(VoucherDate)=", VoucherMaster.VoucherDate.Year, " AND month(VoucherDate)=", VoucherMaster.VoucherDate.Month, " AND day(VoucherDate)=", VoucherMaster.VoucherDate.Day));
            //    //            //mainentry.State = EntityState.Added;
            //    //            context.VoucherMaster.Add(VoucherMaster);
            //    //        }
            //    //        else
            //    //        {
            //    //            if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
            //    //            {
            //    //                VoucherMaster.VoucherDetail.ToList().ForEach(i => { i.CompanyCode = VoucherMaster.CompanyCode; });
            //    //                VoucherMaster.VoucherDetail.ToList().ForEach(i => { i.BranchCode = VoucherMaster.BranchCode; });
            //    //                VoucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherCode = VoucherMaster.VoucherCode; });
            //    //                VoucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherNo = VoucherMaster.VoucherNo; });
            //    //                VoucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherDate = VoucherMaster.VoucherDate; });
            //    //            }
            //    //            //mainentry.State = EntityState.Modified;

            //    //        }
            //    //        //if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
            //    //            //VoucherMaster.VoucherDetail.ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Added);
            //    //        //context.VoucherDetail.ToList().Where(i => i.VoucherNo == VoucherMaster.VoucherNo).ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
            //    //        context.SaveChanges();
            //    //    //}
            //    //}
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            //{

            //}
            //catch (Exception ex)
            //{
            //}
        }
        public void delete(VoucherMaster voucherMaster)
        {
            try
            {
                using (var context = new SmsMisDB())
                {

                    //var context = new SmsMisDB();

                    //var mainentry = context.Entry(voucherMaster);
                    if (voucherMaster != null)
                    {
                        if (voucherMaster.VoucherDetail != null && voucherMaster.VoucherDetail.Count > 0)
                        {
                            List<VoucherDetail> deleteVoucherDetail = context.VoucherDetail.Where(vd => vd.VoucherNo == voucherMaster.VoucherNo).ToList();

                            voucherMaster.VoucherDetail.ToList().ForEach(i => { i.CompanyCode = voucherMaster.CompanyCode; });
                            voucherMaster.VoucherDetail.ToList().ForEach(i => { i.BranchCode = voucherMaster.BranchCode; });
                            voucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherCode = voucherMaster.VoucherCode; });
                            voucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherNo = voucherMaster.VoucherNo; });
                            voucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherDate = voucherMaster.VoucherDate; });
                            foreach (VoucherDetail item in deleteVoucherDetail)
                            {

                                //context.Entry(item).State = EntityState.Deleted;
                                context.VoucherDetail.Attach(item);
                                context.VoucherDetail.Remove(item);
                                context.SaveChanges();
                            }
                            VoucherMaster vm = context.VoucherMaster.Where(vmaster => vmaster.VoucherNo == voucherMaster.VoucherNo).FirstOrDefault();
                            //context.Entry(vm).State = EntityState.Deleted;
                            context.VoucherMaster.Attach(vm);
                            context.VoucherMaster.Remove(vm);
                            context.SaveChanges();
                        }
                        else
                        {
                            VoucherMaster vmCopy = context.VoucherMaster.Where(vmaster => vmaster.VoucherNo == voucherMaster.VoucherNo).FirstOrDefault();
                            //context.Entry(vm).State = EntityState.Deleted;
                            context.VoucherMaster.Attach(vmCopy);
                            context.VoucherMaster.Remove(vmCopy);
                            context.SaveChanges();
                        }
                        // mainentry.State = EntityState.Deleted;
                        //if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
                        //VoucherMaster.VoucherDetail.ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

                        // context.VoucherDetail.ToList().Where(i => i.VoucherNo == voucherMaster.VoucherNo).ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
                        //context.VoucherD.ToList().Where(i => i.VoucherNo == VoucherMaster.VoucherNo).ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

                        //context.VoucherMaster.Attach(VoucherMaster);

                        //context.SaveChanges();
                    }
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
        //public void delete(VoucherMaster voucherMaster)
        //{
        //    try
        //    {
        //        using (SmsMisDB context = new SmsMisDB())
        //        {

        //            //var context = new SmsMisDB();

        //            var mainentry = context.Entry(voucherMaster);
        //            if (mainentry != null)
        //            {
        //                if (voucherMaster.VoucherDetail != null && voucherMaster.VoucherDetail.Count > 0)
        //                {
        //                    List<VoucherDetail> deleteVoucherDetail = voucherMaster.VoucherDetail.ToList();
        //                    context.VoucherMaster.Attach(voucherMaster);

        //                    voucherMaster.VoucherDetail.ToList().ForEach(i => { i.CompanyCode = voucherMaster.CompanyCode; });
        //                    voucherMaster.VoucherDetail.ToList().ForEach(i => { i.BranchCode = voucherMaster.BranchCode; });
        //                    voucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherCode = voucherMaster.VoucherCode; });
        //                    voucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherNo = voucherMaster.VoucherNo; });
        //                    voucherMaster.VoucherDetail.ToList().ForEach(i => { i.VoucherDate = voucherMaster.VoucherDate; });
        //                    foreach (VoucherDetail item in deleteVoucherDetail)
        //                    {
        //                        context.VoucherDetail.Attach(item);
        //                        context.VoucherDetail.Remove(item);
        //                        context.SaveChanges();
        //                    }
        //                    context.VoucherMaster.Remove(voucherMaster);
        //                    context.SaveChanges();
        //                }

        //                // mainentry.State = EntityState.Deleted;
        //                //if (VoucherMaster.VoucherDetail != null && VoucherMaster.VoucherDetail.Count > 0)
        //                //VoucherMaster.VoucherDetail.ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

        //                // context.VoucherDetail.ToList().Where(i => i.VoucherNo == voucherMaster.VoucherNo).ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);
        //                //context.VoucherD.ToList().Where(i => i.VoucherNo == VoucherMaster.VoucherNo).ToList<VoucherDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

        //                //context.VoucherMaster.Attach(VoucherMaster);

        //                //context.SaveChanges();
        //            }
        //            //var entry = context.Entry(ChartOfAccounts);
        //            //context.ChartOfAccounts.Remove(ChartOfAccounts);
        //        }
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //    {
        //        //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw SmsMis.Models.Console.Common.ExceptionTranslater.translate(ex);
        //        throw ex;
        //    }
        //}
        //public bool ValidateInstrumntNumber(int companyCode, int branchCode, string accountCode, int instrumentCode,
        //          int instrumentNumber)
        //      {
        //          using (var context = new SmsMisDB())
        //          {
        //              bool endResult = false;
        //              var result =
        //                  context.InstrumentSerialDetail.FirstOrDefault(
        //                      x =>
        //                          x.CompanyCode == companyCode && x.BranchCode == branchCode && x.AccountCode == accountCode &&
        //                          x.InstrumentTypeCode == instrumentCode && x.InstrumentNo == instrumentNumber).InstrumentNo;
        //              if (result != null)
        //                  endResult = true;
        //              return endResult;
        //          }
        //      }
    }
}
