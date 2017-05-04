using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SmsMis.Models.Console.Client;
using SmsMis.Models.Console.Admin;
using SmsMis.Models.Console.Handlers.Admin;
using SmsMis.Models.Console.Common;

namespace SmsMis.Models.Console.Handlers.Fee
{
    public class hdlFeeBillMaster : DbContext
    {
        public hdlFeeBillMaster() : base("name=ValencySGIEntities") { }

        public IList<FeeBillMaster> SelectAll(int CompanyCode,int BranchCode,int SessionCode)
        {
            SmsMis.Models.Console.Handlers.Admin.SmsMisDB db = new SmsMis.Models.Console.Handlers.Admin.SmsMisDB();
            return db.FeeBillMasters.Where(s => s.CompanyCode == CompanyCode && s.BranchCode == BranchCode && s.SessionCode == SessionCode).ToList();
        }
        public IList<object> getFeeBillMasterWithStudent(int CompanyCode,int BranchCode,int SessionCode)
        {
            SmsMisDB db = new SmsMisDB();
            var otherItems = (from st in db.Student
                              join fee in db.FeeBillMasters on st.CompanyCode equals fee.CompanyCode
                              where st.BranchCode == fee.BranchCode && st.SessionCode == fee.SessionCode && st.StudentNo == fee.StudentNo
                              join clas in db.Classes on st.CompanyCode equals clas.CompanyCode
                              where clas.ClassCode == fee.ClassCode
                              join session in db.Session on st.CompanyCode equals session.CompanyCode
                              where session.SessionCode == fee.SessionCode
                              select new
                              {
                                  StudentNo = st.StudentNo,
                                  FullName = st.FullName,
                                  ChallanNo = fee.ChallanNo,
                                  SessionName = session.SessionName,
                                  ClassName = clas.ClassName,
                                  TotalAmount = fee.TotalAmount,
                                  DiscountAmount = fee.DiscountAmount,
                                  
                                  OutstandingAmount = fee.NetAmount,
                                  CompanyCode = fee.CompanyCode,
                                  BranchCode = fee.BranchCode,
                                  SessionCode = fee.SessionCode,
                              }).Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode).ToArray();
            return otherItems;
        }
        public IList<object> getFeeBillDetailForFeeReciept(int CompanyCode, int BranchCode, int SessionCode,int ChallanNo)
        {
            SmsMisDB db = new SmsMisDB();
            var otherItems = (from p in db.FeeParticular
                              join d in db.FeeBillDetails on p.CompanyCode equals d.CompanyCode
                              where d.ParticularCode == p.ParticularCode 
                              select new
                              {
                                  ParticularName = p.ParticularName,
                                  TotalAmount = d.TotalAmount,
                                  CompanyCode = d.CompanyCode,
                                  BranchCode = d.BranchCode,
                                  SessionCode = d.SessionCode,
                                  ChallanNo = d.ChallanNo,
                              }).Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.ChallanNo == ChallanNo).ToArray();
            return otherItems;
        }
        public List<CustomFeeBillMaster> SelectFeeParticularRate(int CompanyCode, int BranchCode, int SessionCode, int ClassCode)
        {
            List<CustomFeeBillMaster> C = new List<CustomFeeBillMaster>();
            //ClientContext db = new SmsMisDB();
            SmsMisDB db = new SmsMisDB();
            //SmsMisDB dbo = new SmsMisDB();
            var otherItems = (from st in db.Student
                              join rate in db.FeeParticularRate on st.CompanyCode equals rate.CompanyCode
                              where st.BranchCode == rate.BranchCode && st.SessionCode == rate.SessionCode && st.ClassCode == rate.ClassCode
                              join fee in db.FeeParticular on rate.CompanyCode equals fee.CompanyCode
                              where rate.ParticularCode == fee.ParticularCode
                              from Discount in db.StudentFixedDiscount.Where(b => b.CompanyCode == st.CompanyCode && b.BranchCode == st.BranchCode && b.SessionCode == st.SessionCode && b.StudentNo == st.StudentNo).DefaultIfEmpty()
                              from scholarship in db.StudentFixedScholarship.Where(a => a.CompanyCode == st.CompanyCode && a.BranchCode == st.BranchCode && a.SessionCode == st.SessionCode && a.StudentNo == st.StudentNo).DefaultIfEmpty()

                              select new
                              {
                                  ClassNo = st.ClassCode,
                                  Optional = fee.Optional,
                                  CompanyCode = st.CompanyCode,
                                  BranchCode = st.BranchCode,
                                  SessionCode = st.SessionCode,
                                  StudentNo = st.StudentNo,
                                  FeeParticularCode = rate.ParticularCode,
                                  Rate = (rate.Rate),
                                  Recurring = fee.Recurring,
                                  FirstFeeParticular = fee.FirstFeeParticular,
                                  RegularFeeParticular = fee.RegularFeeParticular,
                                  ScholarshipAllowed = fee.ScholarshipAllowed,
                                  DiscountAllowed = fee.DiscountAllowed,
                                  ScholarshipPercentage = scholarship.ScholarshipPercentage,
                                  ScholarshipAmount = (scholarship.ScholarshipAmount),
                                  DiscountPercentage =Discount.DiscountPercentage,
                                  DiscountAmount = (Discount.DiscountAmount),
                                  //DiscountAmount = scholarship.ScholarshipPercentage > 0.0 ? Convert.ToDecimal((((scholarship.ScholarshipPercentage) * rate.Rate) / 100)) :
                                  //Convert.ToDecimal(scholarship.ScholarshipAmount) > 0 ? Convert.ToDecimal(scholarship.ScholarshipAmount) : 0
                                  // + Convert.ToDecimal(Discount.DiscountPercentage) > 0 ? Convert.ToDecimal((Discount.DiscountPercentage * rate.Rate) / 100) :
                                  //Convert.ToDecimal(Discount.DiscountAmount) > 0 ? Convert.ToDecimal(Discount.DiscountAmount) : 0,//((scholarship.ScholarshipPercentage * rate.Rate) / 100),
                                  //NetAmmount =rate.Rate - (scholarship.ScholarshipPercentage > 0.0 ? (((scholarship.ScholarshipPercentage) * rate.Rate) / 100) :
                                  //double.Parse(Convert.ToString(scholarship.ScholarshipAmount)) > 0.0 ? double.Parse(Convert.ToString(scholarship.ScholarshipAmount)) : 0.0
                                  // + Discount.DiscountPercentage > 0.0 ? (Discount.DiscountPercentage * rate.Rate) / 100 :
                                  //double.Parse(Convert.ToString(Discount.DiscountAmount)) > 0.0 ? double.Parse(Convert.ToString(Discount.DiscountAmount)) : 0.0),
                              }).Where(a => a.Optional == false && a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.ClassNo == ClassCode)
                              /*.Concat(
                             from st1 in db.Student
                             join rate1 in db.FeeParticularRate on st1.CompanyCode equals rate1.CompanyCode
                             where st1.BranchCode == rate1.BranchCode && st1.SessionCode == rate1.SessionCode && st1.ClassCode == rate1.ClassCode
                             join fee1 in db.FeeParticular on rate1.CompanyCode equals fee1.CompanyCode
                             where rate1.ParticularCode == fee1.ParticularCode
                             join sofp in db.StudentOptionalFeeParticular on st1.CompanyCode equals sofp.CompanyCode
                             where st1.BranchCode == sofp.BranchCode && st1.SessionCode == sofp.SessionCode && st1.StudentNo == sofp.StudentNo
                             from scholarship1 in db.StudentFixedScholarship.Where(a => a.CompanyCode == st1.CompanyCode && a.BranchCode == st1.BranchCode && a.SessionCode == st1.SessionCode && a.StudentNo == st1.StudentNo).DefaultIfEmpty()
                             select new
                             {
                                 ClassNo = st1.ClassCode,
                                 Optional = fee1.Optional,
                                 CompanyCode = st1.CompanyCode,
                                 BranchCode = st1.BranchCode,
                                 SessionCode = st1.SessionCode,
                                 StudentNo = st1.StudentNo,
                                 FeeParticularCode = rate1.ParticularCode,
                                 Rate = (rate1.Rate),
                                 Recurring = fee1.Recurring,
                                 FirstFeeParticular = fee1.FirstFeeParticular,
                                 RegularFeeParticular = fee1.RegularFeeParticular,
                                 ScholarshipAllowed = fee1.ScholarshipAllowed,
                                 DiscountAllowed = fee1.DiscountAllowed,
                                 ScholarshipRate = (scholarship1.ScholarshipPercentage),
                                 DiscountAmount = ((scholarship1.ScholarshipPercentage * rate1.Rate) / 100),
                                 NetAmmount = (((100 - scholarship1.ScholarshipPercentage) * rate1.Rate) / 100)
                             }).Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.ClassNo == ClassCode).Concat(
                             from st1 in db.Student
                             join ssubject in db.StudentSubject on st1.CompanyCode equals ssubject.CompanyCode
                             where st1.BranchCode == ssubject.BranchCode && st1.SessionCode == ssubject.SessionCode && st1.ClassCode == ssubject.ClassCode && st1.StudentNo == ssubject.StudentNo
                             join rate1 in db.FeeParticularRate on st1.CompanyCode equals rate1.CompanyCode
                             where st1.BranchCode == rate1.BranchCode && st1.SessionCode == rate1.SessionCode && st1.ClassCode == rate1.ClassCode
                             join fee1 in db.FeeParticular on rate1.CompanyCode equals fee1.CompanyCode
                             where rate1.ParticularCode == fee1.ParticularCode
                             join sofp in db.StudentOptionalFeeParticular on st1.CompanyCode equals sofp.CompanyCode
                             where st1.BranchCode == sofp.BranchCode && st1.SessionCode == sofp.SessionCode && st1.StudentNo == sofp.StudentNo
                             from scholarship1 in db.StudentFixedScholarship.Where(a => a.CompanyCode == st1.CompanyCode && a.BranchCode == st1.BranchCode && a.SessionCode == st1.SessionCode && a.StudentNo == st1.StudentNo).DefaultIfEmpty()
                             select new
                             {
                                 ClassNo = st1.ClassCode,
                                 Optional = fee1.Optional,
                                 CompanyCode = st1.CompanyCode,
                                 BranchCode = st1.BranchCode,
                                 SessionCode = st1.SessionCode,
                                 StudentNo = st1.StudentNo,
                                 FeeParticularCode = rate1.ParticularCode,
                                 Rate = (rate1.Rate),
                                 Recurring = fee1.Recurring,
                                 FirstFeeParticular = fee1.FirstFeeParticular,
                                 RegularFeeParticular = fee1.RegularFeeParticular,
                                 ScholarshipAllowed = fee1.ScholarshipAllowed,
                                 DiscountAllowed = fee1.DiscountAllowed,
                                 ScholarshipRate = (scholarship1.ScholarshipPercentage),
                                 DiscountAmount = ((scholarship1.ScholarshipPercentage * rate1.Rate) / 100),
                                 NetAmmount = (((100 - scholarship1.ScholarshipPercentage) * rate1.Rate) / 100)
                             }).Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.ClassNo == ClassCode)
                */
                              .ToArray();
            foreach(var x in otherItems)
            {
                CustomFeeBillMaster c = new CustomFeeBillMaster();
                c.BranchCode = x.BranchCode;
                c.ClassNo = x.ClassNo;
                c.CompanyCode = x.CompanyCode;
                c.DiscountAllowed = x.DiscountAllowed;
                //c.DiscountAmount =Convert.ToDecimal(x.DiscountAmount);
                c.FeeParticularCode = x.FeeParticularCode;
                c.FirstFeeParticular = x.FirstFeeParticular;
                c.NetAmmount = Convert.ToDecimal( x.Rate - ((x.ScholarshipPercentage > 0.0 ? (((x.ScholarshipPercentage) * x.Rate) / 100) :
                                  Convert.ToDouble(x.ScholarshipAmount) > 0.0 ? Convert.ToDouble(x.ScholarshipAmount) : 0.0)
                                   + (x.DiscountPercentage > 0.0 ? (x.DiscountPercentage * x.Rate) / 100 :
                                  Convert.ToDouble(x.DiscountAmount) > 0.0 ? Convert.ToDouble(x.DiscountAmount) : 0.0)));
                c.Optional = x.Optional;
                c.Rate = Convert.ToDecimal(x.Rate);
                c.Recurring = x.Recurring;
                c.RegularFeeParticular = x.RegularFeeParticular;
                c.ScholarshipAllowed = x.ScholarshipAllowed;
                c.ScholarshipPercentage = Convert.ToDecimal(x.ScholarshipPercentage);
                c.ScholarshipAmount = x.ScholarshipAmount;
                c.DiscountPercentage = Convert.ToDecimal(x.DiscountPercentage);
                c.DiscountAmount = (x.ScholarshipPercentage > 0.0 ? Convert.ToDecimal((((x.ScholarshipPercentage) * x.Rate) / 100)) :
                    Convert.ToDecimal(x.ScholarshipAmount) > 0 ? Convert.ToDecimal(x.ScholarshipAmount) : 0)
                     + (Convert.ToDecimal(x.DiscountPercentage) > 0 ? Convert.ToDecimal((x.DiscountPercentage * x.Rate) / 100) :
                    Convert.ToDecimal(x.DiscountAmount) > 0 ? Convert.ToDecimal(x.DiscountAmount) : 0);//((scholarship.ScholarshipPercentage * rate.Rate) / 100),x.DiscountAmount;
                c.SessionCode = x.SessionCode;
                c.StudentNo = x.StudentNo;
                C.Add(c);
            }
            return C;
        }
        public void save(string UserId, int CompanyCode, int BranchCode, int SessionCode, int BankCode, DateTime IssueDate, DateTime DueDate, List<Class> Class, List<monthList> MonthList)
        {
            try
            {
                
                using (var context = new SmsMisDB())
                {
                    //if (FeeBillMaster != null)
                    {
                        //FeeBillMasters.ToList<FeeBillMaster>().ForEach(i => i.AddDateTime = DateTime.Now);
                        //FeeBillMasters.ToList<FeeBillMaster>().ForEach(i => i.AddByUserId = UserId);
                        //FeeBillMasters.ToList<FeeBillMaster>().ForEach(i => i.CompanyCode = CompanyCode);
                        //FeeBillMasters.ToList<FeeBillMaster>().ForEach(i => i.BranchCode = BranchCode);
                        //FeeBillMasters.ToList<FeeBillMaster>().ForEach(i => i.SessionCode = SessionCode);
                        //FeeBillMasters.ToList<FeeBillMaster>().ForEach(i => i.ClassCode = BankCode);
                        //foreach(var x in MonthList)
                        char[] split= {','};

                         string feeterm =  MonthList[0].name.Split(split)[0];
                        feeterm =  feeterm + "," + MonthList[MonthList.Count - 1].name.Split(split)[0];
                        string feeperiod = "";
                        foreach (var x in MonthList)
                            feeperiod = string.IsNullOrEmpty(feeperiod) ?  x.name.Split(split)[0] : feeperiod + "," + x.name.Split(split)[0];
                        foreach (var row in Class)
                        {
                            List<Student> students = context.Student.Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.ClassCode == row.ClassCode).ToList();
                            
                            
                            IList<CustomFeeBillMaster> obj = SelectFeeParticularRate(CompanyCode, BranchCode, SessionCode, row.ClassCode);
                            foreach (Student student in students)
                            {
                                FeeBillMaster FeeBillMasters = new FeeBillMaster();
                                var mainentry = context.Entry(FeeBillMasters);
                            if (mainentry != null)
                            {
                                
                                    FeeBillMasters.ChallanNo = Functions.getNextPk("FeeBillMaster", "ChallanNo", string.Concat("WHERE CompanyCode =", CompanyCode, " AND BranchCode= ", BranchCode, " AND SessionCode= ", SessionCode));
                                    FeeBillMasters.AddByUserId = UserId;
                                    FeeBillMasters.AddDateTime = DateTime.Now;

                                    FeeBillMasters.CompanyCode = CompanyCode;
                                    FeeBillMasters.BranchCode = BranchCode;
                                    FeeBillMasters.SessionCode = SessionCode;
                                    FeeBillMasters.ClassCode = row.ClassCode;
                                    FeeBillMasters.StudentNo = student.StudentNo;
                                    FeeBillMasters.ChallanType = "F";
                                    FeeBillMasters.FeeTerm = feeterm;
                                    FeeBillMasters.FeePeriod = feeperiod;
                                    FeeBillMasters.BankCode = BankCode;
                                    FeeBillMasters.IssueDate = IssueDate;
                                    FeeBillMasters.DueDate = DueDate;
                                    FeeBillMasters.AttendanceFineAmount = 0;

                                    List<FeeBillDetailMonthly> feeBillDetailMonthly = new List<FeeBillDetailMonthly>();
                                    List<FeeBillDetail> feebilldetail = new List<FeeBillDetail>();
                                    decimal totalamount = 0,netamount = 0,discountamount = 0;
                                    foreach (CustomFeeBillMaster record in obj.Where(b => b.StudentNo == student.StudentNo))
                                    {
                                        FeeBillDetail f = new FeeBillDetail();
                                        f.BranchCode = BranchCode;
                                        f.ChallanNo = FeeBillMasters.ChallanNo;
                                        f.CompanyCode = CompanyCode;
                                        f.ParticularCode = record.FeeParticularCode;
                                        f.SessionCode = SessionCode;                                        

                                        List<StudentBillingCycle> billingcycle = context.StudentBillingCycle.Where(a => a.CompanyCode == CompanyCode && a.BranchCode == BranchCode && a.SessionCode == SessionCode && a.StudentNo == student.StudentNo).ToList();
                                        if (record.Recurring.ToLower() == "monthly")
                                        {
                                            if (billingcycle[0].BiilingCycle <= MonthList.Count)
                                            {
                                                f.DiscountAmount = record.DiscountAmount * billingcycle[0].BiilingCycle;
                                                f.NetAmount = record.NetAmmount * billingcycle[0].BiilingCycle;
                                                f.ParticularCode = record.FeeParticularCode;
                                                f.SessionCode = SessionCode;
                                                f.TotalAmount = record.Rate * billingcycle[0].BiilingCycle;
                                                totalamount = totalamount + f.TotalAmount;
                                                netamount = netamount + f.NetAmount;
                                                discountamount = f.DiscountAmount + discountamount;
                                            }
                                        }
                                        else
                                        {
                                            f.DiscountAmount = record.DiscountAmount;
                                            f.NetAmount = record.NetAmmount;
                                            f.TotalAmount = record.Rate;
                                            totalamount = totalamount + f.TotalAmount;
                                            netamount = netamount + f.NetAmount;
                                            discountamount = f.DiscountAmount + discountamount;
                                        }
                                            feebilldetail.Add(f);
                                        int srNo = 1;
                                        if (record.Recurring.ToLower() == "monthly" && billingcycle[0].BiilingCycle <= MonthList.Count)
                                        foreach (StudentBillingCycle sbc in billingcycle)
                                        {
                                            foreach (var x in MonthList)
                                            {
                                                FeeBillDetailMonthly Monthly = new FeeBillDetailMonthly();
                                                Monthly.BranchCode = BranchCode;
                                                Monthly.ChallanNo = FeeBillMasters.ChallanNo;
                                                Monthly.CompanyCode = FeeBillMasters.CompanyCode;
                                                Monthly.DiscountAmount = FeeBillMasters.DiscountAmount;
                                                Monthly.NetAmount = FeeBillMasters.NetAmount;
                                                Monthly.ParticularCode = record.FeeParticularCode;
                                                Monthly.SessionCode = SessionCode;
                                                Monthly.SrNo = srNo;
                                                Monthly.StudentNo = FeeBillMasters.StudentNo;
                                                Monthly.TotalAmount = record.Rate;
                                                Monthly.TransactionDate = x.startDate;
                                                srNo++;
                                                feeBillDetailMonthly.Add(Monthly);
                                            }
                                        }
                                    }
                                    
                                    FeeBillMasters.TotalAmount = totalamount;
                                    FeeBillMasters.NetAmount = netamount;
                                    FeeBillMasters.DiscountAmount = discountamount;
                                    FeeBillMasters.ScholarshipAmount = 0;
                                    FeeBillMasters.FeeBillDetailMonthly = feeBillDetailMonthly;
                                    FeeBillMasters.FeeBillDetail = feebilldetail;
                                    mainentry.State = EntityState.Added;
                                    context.FeeBillMasters.Add(FeeBillMasters);

                                    if (FeeBillMasters.FeeBillDetail != null && FeeBillMasters.FeeBillDetail.Count > 0)
                                        FeeBillMasters.FeeBillDetail.ToList<FeeBillDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Added);
                                    //context.FeeBillDetails.ToList().Where(i => i.CompanyCode == FeeBillMasters.CompanyCode && i.BranchCode == FeeBillMasters.BranchCode && i.SessionCode == FeeBillMasters.SessionCode && i.ChallanNo == FeeBillMasters.ChallanNo).ToList<FeeBillDetail>().ForEach(entry => context.Entry(entry).State = EntityState.Deleted);

                                    context.SaveChanges();
                                }
                            }
                        }
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
    }

    public class CustomFeeBillMaster
    {
        public int ClassNo  { get; set; }
        public bool Optional  { get; set; }
        public int CompanyCode  { get; set; }
        public int BranchCode  { get; set; }
        public int SessionCode  { get; set; }
        public int StudentNo  { get; set; }
        public int FeeParticularCode  { get; set; }
        public decimal Rate { get; set; }
        public string Recurring  { get; set; }
        public bool FirstFeeParticular { get; set; }
        public bool RegularFeeParticular { get; set; }
        public bool ScholarshipAllowed { get; set; }
        public bool DiscountAllowed { get; set; }
        public decimal ScholarshipPercentage { get; set; }
        public decimal ScholarshipAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal NetAmmount { get; set; }
    }
}

