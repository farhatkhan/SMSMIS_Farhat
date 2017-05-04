using SmsMisWeb.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmsMisWeb.Views.Report
{
    public partial class FeeReports : System.Web.UI.Page
    {
        protected string ReportProcedure = string.Empty;
        protected string ReportName = string.Empty;
        protected int BranchID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int iReportID = Convert.ToInt32(Request.QueryString["ReportID"].ToString());
                int CompanyID = Convert.ToInt32(Request.QueryString["CompanyCode"]);
                if (Request.QueryString["BranchCode"] != null)
                    BranchID = Convert.ToInt32(Request.QueryString["BranchCode"]);

                


                if (iReportID > 0)
                {
                   if (GlobalConstants.StudentFEEBILL_Report == iReportID)
                    {
                        //ReportProcedure = "select c.CompanyName,branch.BranchName,Class.ClassName,s.SessionName, st.FullName, fmaster.FeeTerm from FeeBillMaster fmaster inner join FeeBillDetail fdetail on fmaster.CompanyCode = fdetail.CompanyCode and fmaster.BranchCode = fdetail.BranchCode and fmaster.SessionCode = fdetail.SessionCode and fmaster.ChallanNo = fdetail.ChallanNo inner join Company c on c.CompanyCode = fmaster.CompanyCode left join Bank on bank.CompanyCode = fmaster.CompanyCode and bank.BranchCode = fmaster.BranchCode and bank.BankCode = fmaster.BankCode --left join FeeBillPaymentTerms term on term.CompanyCode = fmaster.CompanyCode and term.BillType='R' left join Branch on Branch.CompanyCode = fmaster.CompanyCode and Branch.BranchCode = fmaster.BranchCode left join Class on Class.CompanyCode = fmaster.CompanyCode left join [Session] s on s.CompanyCode = fmaster.CompanyCode and s.SessionCode = fmaster.SessionCode left outer join Student st on fmaster.StudentNo = st.StudentNo and st.CompanyCode = fmaster.CompanyCode and st.BranchCode = fmaster.BranchCode and st.SessionCode = fmaster.SessionCode where fmaster.CompanyCode = @CompanyCode and fmaster.BranchCode = @BranchCode and fmaster.SessionCode = @SessionCode and  fmaster.ClassCode = @ClassCode; SELECT * FROM FeeBillPaymentTerms WHERE companycode = @CompanyCode";
                        ReportProcedure = "select c.CompanyCode, fmaster.BranchCode, fmaster.SessionCode, fmaster.ClassCode, fmaster.StudentNo,bank.BankName, c.Phone1,bank.AccountNo,fmaster.ChallanNo, fmaster.FeeTerm, fmaster.IssueDate, fmaster.DueDate, c.CompanyName,branch.BranchName,Class.ClassName,s.SessionName, st.FullName ,fdetail.NetAmount detailNetAmount,fmaster.NetAmount,p.ParticularName from FeeBillMaster fmaster inner join FeeBillDetail fdetail on fdetail.CompanyCode = fmaster.CompanyCode and fdetail.BranchCode = fmaster.BranchCode and fdetail.SessionCode = fmaster.SessionCode and fdetail.ChallanNo = fmaster.ChallanNo inner join FeeParticular p on p.CompanyCode = fdetail.CompanyCode and p.ParticularCode = fdetail.ParticularCode inner join Company c on c.CompanyCode = fmaster.CompanyCode left join Bank on bank.CompanyCode = fmaster.CompanyCode and bank.BranchCode = fmaster.BranchCode and bank.BankCode = fmaster.BankCode  left join Branch on Branch.CompanyCode = fmaster.CompanyCode and Branch.BranchCode = fmaster.BranchCode left join Class on Class.CompanyCode = fmaster.CompanyCode left join [Session] s on s.CompanyCode = fmaster.CompanyCode and s.SessionCode = fmaster.SessionCode left outer join Student st on fmaster.StudentNo = st.StudentNo and st.CompanyCode = fmaster.CompanyCode and st.BranchCode = fmaster.BranchCode and st.SessionCode = fmaster.SessionCode where fmaster.CompanyCode = @CompanyCode and fmaster.BranchCode = @BranchCode and fmaster.SessionCode = @SessionCode and  fmaster.ClassCode in(@ClassCode) and fmaster.FeeTerm = @FeeTerm ; SELECT * FROM FeeBillPaymentTerms WHERE companycode = 1";
                        ReportName = "Report/rptStudentFeeBill.rdlc";
                    }else 
                   if (GlobalConstants.VoucherMaster_Report == iReportID)
                   {
                       ReportProcedure = "select coa.AccountTitle, CompanyName,BranchName,vm.*,vd.*,c.CurrencyName,c.ShortName from VoucherMaster vm inner join VoucherDetail vd on vm.CompanyCode = vd.CompanyCode and vm.BranchCode = vd.BranchCode and vm.VoucherDate = vd.VoucherDate and vm.VoucherNo = vd.VoucherNo inner join Company on company.CompanyCode = vm.CompanyCode inner join Branch on Branch.BranchCode = vm.BranchCode and Branch.CompanyCode = vm.CompanyCode inner join ChartOfAccounts coa on coa.CompanyCode = vm.CompanyCode and vd.AccountCode = coa.AccountCode left join Currency c on vm.CurrencyCode = c.CurrencyCode where vm.CompanyCode = @CompanyCode and vm.BranchCode = @BranchCode and vm.VoucherDate = @VoucherDate and vm.VoucherNo = @VoucherNo and vm.VoucherCode = @VoucherCode";
                       ReportName = "Report/rptVoucherMaster.rdlc";
                   }
                   else if(GlobalConstants.IssuedChallan_Report == iReportID)
                   {
                       ReportProcedure = "select c.CompanyName,b.BranchName,s.SessionName,cls.ClassName,isnull(st.StudentRollNo,st.StudentNo) StudentNo, st.FullName,fee.IssueDate,fee.FeePeriod,fee.DueDate,fee.TotalAmount,fee.ChallanNo from FeeBillMaster fee inner join Company c on c.CompanyCode = fee.CompanyCode inner join Branch b on b.CompanyCode = fee.CompanyCode and b.BranchCode = fee.BranchCode inner join Session s on s.SessionCode = fee.SessionCode and s.CompanyCode = fee.CompanyCode inner join Class cls on cls.CompanyCode = fee.CompanyCode and cls.ClassCode = fee.ClassCode inner join Student st on st.CompanyCode = fee.CompanyCode and st.BranchCode = fee.BranchCode and st.SessionCode = fee.SessionCode and st.ClassCode = fee.ClassCode and st.StudentNo = fee.StudentNo Where fee.CompanyCode =@CompanyCode and fee.BranchCode = @BranchCode and fee.SessionCode = @SessionCode and fee.ClassCode in (@ClassCode)";
                       ReportName = "Report/rptIssuedChallan.rdlc";
                   }
                   else if (GlobalConstants.FeeReceipt_Report == iReportID)
                   {
                       ReportProcedure = "SELECT c.CompanyName,b.BranchName,b.Phone1, cls.ClassName,ISNULL(s.StudentRollNo,s.StudentNo) StudentRollNo, s.FullName,fee.ChallanNo,fee.ReceiptNo,fee.ReceiptDate,fbm.FeePeriod,fbm.DueDate,fee.WaivedAmount,fee.ReceivedAmount,fee.OutstandingAmount,fbm.DiscountAmount,fbm.TotalAmount ,p.ParticularName,d.TotalAmount as Amount from smsmis..FeeReceipt fee inner join FeeBillMaster fbm on fee.CompanyCode = fbm.CompanyCode and fee.BranchCode = fbm.BranchCode and fee.SessionCode = fbm.SessionCode and fee.ChallanNo = fbm.ChallanNo inner join FeeBillDetail d on fbm.CompanyCode = d.CompanyCode and fbm.BranchCode = d.BranchCode and fbm.SessionCode = d.SessionCode and fbm.ChallanNo = d.ChallanNo inner join FeeParticular p on p.CompanyCode = d.CompanyCode and p.ParticularCode = d.ParticularCode inner join Branch b on fbm.BranchCode = b.BranchCode and fbm.CompanyCode = b.CompanyCode inner join Company c on fbm.CompanyCode = c.CompanyCode inner join Student s on s.StudentNo = fbm.StudentNo and s.BranchCode = fbm.BranchCode and fbm.SessionCode = s.SessionCode and fbm.CompanyCode = s.CompanyCode inner join Class cls on cls.CompanyCode = fbm.CompanyCode and cls.ClassCode = fbm.ClassCode where Fee.CompanyCode = @CompanyCode and fee.BranchCode = @BranchCode and fee.ReceiptNo = @ReceiptNo and fee.ReceiptDate = @ReceiptDate ";
                       ReportName = "Report/rptFeeReceipt.rdlc";
                   }
                   else if (GlobalConstants.ChartOfAccount_Report == iReportID)
                   {
                       ReportProcedure = "spChartOfAccounts @CompanyCode,@BranchCode ";
                       ReportName = "Report/rptCOAList.rdlc";
                   }
                   else if (GlobalConstants.printVoucher_Report == iReportID)
                   {
                       ReportProcedure = "select coa.AccountTitle, CompanyName,BranchName,vm.*,vd.*,c.CurrencyName,c.ShortName from VoucherMaster vm inner join VoucherDetail vd on vm.CompanyCode = vd.CompanyCode and vm.BranchCode = vd.BranchCode and vm.VoucherDate = vd.VoucherDate and vm.VoucherNo = vd.VoucherNo inner join Company on company.CompanyCode = vm.CompanyCode inner join Branch on Branch.BranchCode = vm.BranchCode and Branch.CompanyCode = vm.CompanyCode inner join ChartOfAccounts coa on coa.CompanyCode = vm.CompanyCode and vd.AccountCode = coa.AccountCode left join VoucherType vt on vt.CompanyCode = vm.CompanyCode and vt.VoucherCode = vm.VoucherCode left join Currency c on vm.CurrencyCode = c.CurrencyCode WHERE Company.CompanyCode = @CompanyCode and (@BranchCode is null or Branch.BranchCode in (select items from dbo.Split(@BranchCode))) and (@VoucherType is null or vt.VoucherCode in (select items from dbo.Split(@VoucherType))) and (@COA is null or coa.accountcode in (select items from dbo.Split(@COA))) and vm.VoucherDate  >= isnull(@FromDate,vm.VoucherDate) and vm.VoucherDate <= isnull(@ToDate,vm.VoucherDate) and vm.VoucherNo >= isnull(@FromNo,vm.VoucherNo) and vm.VoucherNo <= isnull(@ToNo,vm.VoucherNo)";
                       ReportName = "Report/rptVoucherMaster.rdlc";
                   }
                   else if (GlobalConstants.General_Journal_Report == iReportID)
                   {
                       ReportProcedure = "Select coa.AccountTitle, CompanyName,BranchName,vm.*,vd.*,c.CurrencyName,c.ShortName from VoucherMaster vm inner join VoucherDetail vd on vm.CompanyCode = vd.CompanyCode and vm.BranchCode = vd.BranchCode and vm.VoucherDate = vd.VoucherDate and vm.VoucherNo = vd.VoucherNo inner join Company on company.CompanyCode = vm.CompanyCode inner join Branch on Branch.BranchCode = vm.BranchCode and Branch.CompanyCode = vm.CompanyCode inner join ChartOfAccounts coa on coa.CompanyCode = vm.CompanyCode and vd.AccountCode = coa.AccountCode left join VoucherType vt on vt.CompanyCode = vm.CompanyCode and vt.VoucherCode = vm.VoucherCode left join Currency c on vm.CurrencyCode = c.CurrencyCode WHERE Company.CompanyCode = @CompanyCode and (@BranchCode is null or Branch.BranchCode in (select items from dbo.Split(@BranchCode))) and (@VoucherType is null or vt.VoucherCode in (select items from dbo.Split(@VoucherType))) and (@COA is null or coa.accountcode in (select items from dbo.Split(@COA))) and vm.VoucherDate  >= isnull(@FromDate,vm.VoucherDate) and vm.VoucherDate <= isnull(@ToDate,vm.VoucherDate) and vm.VoucherNo >= isnull(@FromNo,vm.VoucherNo) and vm.VoucherNo <= isnull(@ToNo,vm.VoucherNo)";
                       ReportName = "Report/rptPrintVoucherMaster.rdlc";
                   }
                   else if (GlobalConstants.INQUERY_LIST_REPORT == iReportID)
                   {
                       ReportProcedure = "SELECT st.Gender,c.CompanyName,b.BranchName,s.SessionName,cls.ClassName,isnull(st.StudentRollNo,st.StudentNo) StudentNo, st.FullName FROM Student st inner join Company c on c.CompanyCode = st.CompanyCode inner join Branch b on b.CompanyCode = st.CompanyCode and b.BranchCode = st.BranchCode inner join Session s on s.SessionCode = st.SessionCode and s.CompanyCode = st.CompanyCode inner join Class cls on cls.CompanyCode = st.CompanyCode and cls.ClassCode = st.ClassCode Where st.FormReceived= 0 and st.CompanyCode =isnull(@CompanyCode,st.companycode) and st.BranchCode = isnull(@BranchCode,st.branchcode) and st.SessionCode = isnull(@SessionCode,st.SessionCode) and st.ClassCode = isnull(@ClassCode,st.ClassCode)";
                       ReportName = "Report/rptInquiryList.rdlc";
                   }
                }
                else
                    return;

                SqlCommand cmdLatest = OpenConnection(ReportProcedure);
                if (GlobalConstants.StudentFEEBILL_Report == iReportID)
                {
                    
                    int SessionID = Convert.ToInt32(Request.QueryString["SessionCode"]);
                    int BankID = Convert.ToInt32(Request.QueryString["BankCode"]);
                    string FeeTerm = Request.QueryString["MonthCode"];
                    string ClassID = Request.QueryString["ClassCode"];                    
                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID), new SqlParameter("@BranchCode", BranchID), new SqlParameter("@SessionCode", SessionID), new SqlParameter("@ClassCode", ClassID), new SqlParameter("@FeeTerm", FeeTerm) };
                    cmdLatest.Parameters.AddRange(parameterList);
                }
                else if (GlobalConstants.VoucherMaster_Report == iReportID)
                {

                    int vNo = Convert.ToInt32(Request.QueryString["VoucherNo"]);
                    string vDate = (Request.QueryString["VoucherDate"]);
                    int vCode = Convert.ToInt32(Request.QueryString["VoucherCode"]);

                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID), new SqlParameter("@BranchCode", BranchID), new SqlParameter("@VoucherNo", vNo), new SqlParameter("@VoucherDate", vDate), new SqlParameter("@VoucherCode", vCode) };
                    cmdLatest.Parameters.AddRange(parameterList);
                }
                else if (GlobalConstants.IssuedChallan_Report == iReportID )
                {
                    int SessionID = Convert.ToInt32(Request.QueryString["SessionCode"]);
                    string ClassID = Request.QueryString["ClassCode"];
                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID), new SqlParameter("@BranchCode", BranchID), new SqlParameter("@SessionCode", SessionID), new SqlParameter("@ClassCode", ClassID) };
                    cmdLatest.Parameters.AddRange(parameterList);
                }
                else if (GlobalConstants.INQUERY_LIST_REPORT == iReportID)
                {
                    int SessionID = Convert.ToInt32(Request.QueryString["SessionCode"]);
                    int ClassID = Convert.ToInt16( Request.QueryString["ClassCode"]);
                    string Gender = Request.QueryString["Gender"];
                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID), 
                       BranchID == 0 ?  new SqlParameter("@BranchCode", DBNull.Value) : new SqlParameter("@BranchCode", BranchID), 
                        SessionID == 0 ? new SqlParameter("@SessionCode", DBNull.Value):new SqlParameter("@SessionCode", SessionID), 
                        ClassID == 0 ? new SqlParameter("@ClassCode", DBNull.Value) : new SqlParameter("@ClassCode", ClassID),
                    string.IsNullOrEmpty(Gender)? new SqlParameter("@Gender", DBNull.Value) : new SqlParameter("@Gender", ClassID)};
                    
                    cmdLatest.Parameters.AddRange(parameterList);
            }
                else if (GlobalConstants.FeeReceipt_Report == iReportID)
                {
                    int ReceiptNo = Convert.ToInt32(Request.QueryString["ReceiptNo"]);

                    string ReceiptDate = Request.QueryString["ReceiptDate"];
                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID), new SqlParameter("@BranchCode", BranchID), new SqlParameter("@ReceiptNo", ReceiptNo), new SqlParameter("@ReceiptDate", ReceiptDate) };
                    cmdLatest.Parameters.AddRange(parameterList);
                }
                else if (GlobalConstants.ChartOfAccount_Report == iReportID)
                {
                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID), new SqlParameter("@BranchCode", BranchID) };
                    cmdLatest.Parameters.AddRange(parameterList);
                }
                else if (GlobalConstants.General_Journal_Report == iReportID || GlobalConstants.printVoucher_Report == iReportID)
                {
                    string VoucherType = string.Empty, ToNo = string.Empty, FromNo = string.Empty, ToDate = string.Empty, FromDate = string.Empty, COA = string.Empty, BranchCodes = string.Empty;
                    if (Request.QueryString["BranchCodes"] != null)
                        BranchCodes = (Request.QueryString["BranchCodes"]);
                    if (Request.QueryString["VoucherType"] != null)
                        VoucherType = (Request.QueryString["VoucherType"]);
                    if (Request.QueryString["COA"] != null)
                        COA = Request.QueryString["COA"];
                    if (Request.QueryString["FromDate"] != null)
                        FromDate = Request.QueryString["FromDate"];
                    if (Request.QueryString["ToDate"] != null)
                        ToDate = Request.QueryString["ToDate"];
                    if (Request.QueryString["FromVoucher"] != null)
                        FromNo = Request.QueryString["FromVoucher"];
                    if (Request.QueryString["ToVoucher"] != null)
                        ToNo = Request.QueryString["ToVoucher"];
                    SqlParameter[] parameterList = new SqlParameter[] { new SqlParameter("@CompanyCode", CompanyID)
                    , string.IsNullOrEmpty(BranchCodes) ? new SqlParameter("@BranchCode",   DBNull.Value) : new SqlParameter("@BranchCode",   BranchCodes)
                    , string.IsNullOrEmpty(VoucherType)? new SqlParameter("@VoucherType", DBNull.Value) : new SqlParameter("@VoucherType", VoucherType)
                    , string.IsNullOrEmpty(COA)?  new SqlParameter("@COA", DBNull.Value): new SqlParameter("@COA", COA)
                    , string.IsNullOrEmpty(FromDate)? new SqlParameter("@FromDate", DBNull.Value) : new SqlParameter("@FromDate", FromDate)
                    , string.IsNullOrEmpty(ToDate)? new SqlParameter("@ToDate", DBNull.Value) : new SqlParameter("@ToDate", ToDate)
                    , string.IsNullOrEmpty(FromNo)? new SqlParameter("@FromNo", DBNull.Value) : new SqlParameter("@FromNo", FromNo)
                    , string.IsNullOrEmpty(ToNo)? new SqlParameter("@ToNo", DBNull.Value): new SqlParameter("@ToNo", ToNo)};
                    cmdLatest.Parameters.AddRange(parameterList);
                }
                
                //"/Report/Report?CompanyCode=" + $scope.selectedObject.CompanyCode + "&ReportID=108&BranchCode=" + arrbranch.toString() + 
                //"&VoucherType=" + arrVType.toString() + "&FromDate=" + fromDate.getFullYear() + '-' + (fromDate.getMonth() + 1) + '-' + fromDate.getDate() + "&ToDate=" + toDate.getFullYear() + '-' + (toDate.getMonth() + 1) + '-' + toDate.getDate() + "&FromVoucher=" + $scope.selectedObject.FromVoucher + "&ToVoucher=" + $scope.selectedObject.ToVoucher;
                DataSet dt = GetDataSet(cmdLatest);
                ReportViewer1.Reset();
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportViewer1.LocalReport.ReportPath = ReportName;
                ReportViewer1.LocalReport.DataSources.Clear();
                // Setting Parameters
                SetReportParameters(iReportID, dt);
            }
        }

        private void SetReportParameters(int iReportID, DataSet dt)
        {
            
            // Parameters Section
            if (GlobalConstants.StudentFEEBILL_Report == iReportID)
            {
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("StudentFeeBill", dt.Tables[0]));
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("FeeBillPaymentTerms", dt.Tables[1]));
                string imagePath = new Uri(Server.MapPath(string.Format("~/Upload/Companies/3.PNG"))).AbsoluteUri;
                Microsoft.Reporting.WebForms.ReportParameter parameter1 = new Microsoft.Reporting.WebForms.ReportParameter("ImagePath", imagePath);
                ReportViewer1.LocalReport.SetParameters(parameter1);
            }
            if (GlobalConstants.VoucherMaster_Report == iReportID || GlobalConstants.INQUERY_LIST_REPORT == iReportID || GlobalConstants.IssuedChallan_Report == iReportID || GlobalConstants.FeeReceipt_Report == iReportID || GlobalConstants.ChartOfAccount_Report == iReportID || GlobalConstants.printVoucher_Report == iReportID || GlobalConstants.General_Journal_Report == iReportID)
            {
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt.Tables[0]));
            }
            if(GlobalConstants.ChartOfAccount_Report == iReportID){

                Microsoft.Reporting.WebForms.ReportParameter parameter1 = new Microsoft.Reporting.WebForms.ReportParameter("isBranch", BranchID > 0 ? "1" : "0");
                ReportViewer1.LocalReport.SetParameters(parameter1);
            }
        }

        public SqlCommand OpenConnection(string strSpName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSpName;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            con.Open();
            return cmd;
        }
        public DataSet GetDataSet(SqlCommand cmd)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}