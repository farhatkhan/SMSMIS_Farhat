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
    public partial class CommonrdlcReport : System.Web.UI.Page
    {
        protected string ReportProcedure = string.Empty;
        protected string ReportName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int iReportID = Convert.ToInt32(Request.QueryString["ReportID"].ToString());
                if (iReportID > 0)
                {
                    if (GlobalConstants.COMPANY_REPORT == iReportID)
                    {
                        ReportProcedure = "spGetAllCompanies";
                        ReportName = "Report/rptCompany.rdlc";
                    }
                    else if (GlobalConstants.DEPARTMENT_REPORT == iReportID)
                    {
                        ReportProcedure = "spGetAllBranches";
                        ReportName = "Report/rptBranches.rdlc";
                    }
                    else if (GlobalConstants.STUDENTFEE_REPORT == iReportID)
                    {
                        ReportProcedure = "Select distinct StudentNo,Convert(date,getdate()) as dateofIssue,FullName,cls.ClassName,st.Amount,'Admin' as UserName,Case When ((Select COUNT(1) from Branch Where CompanyCode = st.CompanyCode) = 1) Then c.CompanyName Else c.CompanyName  + ' - ' + b.BranchName End companyName,'Fee Receipt' ReportName,c.Address,coalesce(c.Phone1,c.Phone2,c.Phone3,c.Phone4),c.Eamil1,c.LogoPath from Student st with(nolock) inner join Class cls with(nolock) on st.CompanyCode = cls.CompanyCode And st.ClassCode = cls.ClassCode inner join [User] u with(nolock) on u.UserId = st.AddByUserId inner join Company c with(nolock) on c.CompanyCode = st.CompanyCode inner join Branch b with(nolock) on c.CompanyCode = b.CompanyCode and st.BranchCode = b.BranchCode  Where st.StudentNo = @StudentNo And st.CompanyCode = @CompanyCode And st.BranchCode = @BranchCode And st.SessionCode = @SessionCode";
                        ReportName = "Report/rptStudentReceipt.rdlc";
                    }
                    else if (GlobalConstants.APPLICATIONFORM_REPORT == iReportID)
                    {
                        ReportProcedure = "Select st.StudentNo as SrNo,st.StudentNo as FormNo,Convert(varchar,st.AddDateTime,103) as [Date],st.FullName as [Name],ClassName as [Class],st.Address,coalesce(st.StudentCellNo,st.Landline1,st.Landline2) as Phone,st.StudentEmail as [Email], st.Amount as [Price], Convert(varchar,getdate(),103) as [DueDate],'Admin' as [IssuedBy], Remarks,co.CompanyName,Convert(varchar,@DateFrom,103) as DateFrom,Convert(varchar,@DateTo,103) as DateTo,coalesce(co.Phone1,co.Phone2,co.Phone3,co.Phone4) as Phone,coalesce(co.Eamil1,co.Email2) as Email from student st with(nolock) 	inner join Class cls with(nolock) on st.ClassCode = cls.ClassCode and st.CompanyCode = cls.CompanyCode 	inner join Company co with(nolock) on co.CompanyCode = st.CompanyCode 	WHERE ST.CompanyCode = @CompanyCode 	AND ST.BranchCode = @BranchCode 	AND ST.SessionCode = @SessionCode 	AND ST.ClassCode = @ClassCode 	AND ST.Gender = @Gender 	AND convert(date,ST.AddDateTime) >= convert(date,@DateFrom) and convert(date,ST.AddDateTime) < convert(date,@DateTo)";
                        ReportName = "Report/rptApplicationForm.rdlc";
                    }
                    else if (GlobalConstants.APPLICATIONLIST_REPORT == iReportID)
                    {
                        ReportProcedure = "Select distinct st.StudentNo,st.FullName,sn.SessionName,cl.ClassName,st.Gender,co.CompanyName,br.BranchName from Student st with(nolock) inner join Company co with(nolock) on st.CompanyCode = co.CompanyCode  inner join Branch br with(nolock) on br.CompanyCode = st.CompanyCode and br.BranchCode = st.BranchCode   inner join [Session] sn with(nolock) on sn.CompanyCode = st.CompanyCode and sn.SessionCode = st.SessionCode  inner join [Class] cl with(nolock) on cl.ClassCode = st.ClassCode and cl.CompanyCode = st.CompanyCode Where  st.CompanyCode = @CompanyCode and  1 = Case When isnull(@BranchCode,0) > 0 And st.BranchCode = @BranchCode then 1 When isnull(@BranchCode,0) = 0 then 1 Else 0 End And 1 = Case When isnull(@SessionCode,0) > 0 And st.SessionCode = @SessionCode then 1 When isnull(@SessionCode,0) = 0 then 1 Else 0 End And 1 = Case When isnull(@ClassCode,0) > 0 And st.ClassCode = @ClassCode then 1 When isnull(@ClassCode,0) = 0 then 1 Else 0 End And 1 = Case When isnull(@Gender,'') <> '' And st.Gender = @Gender then 1 When isnull(@Gender,'') = '' then 1 Else 0 End And st.StudentRollNo is null";
                        ReportName = "Report/rptApplicationList.rdlc";
                    }
                    else if (GlobalConstants.CLASSLIST_REPORT == iReportID)
                    {
                        ReportProcedure = "Select distinct st.StudentNo,st.FullName,sn.SessionName,cl.ClassName,st.Gender,co.CompanyName,br.BranchName from Student st with(nolock) inner join Company co with(nolock) on st.CompanyCode = co.CompanyCode   inner join Branch br with(nolock) on br.CompanyCode = st.CompanyCode and br.BranchCode = st.BranchCode    inner join [Session] sn with(nolock) on sn.CompanyCode = st.CompanyCode and sn.SessionCode = st.SessionCode   inner join [Class] cl with(nolock) on cl.ClassCode = st.ClassCode and cl.CompanyCode = st.CompanyCode  inner join StudentAdmissionSubject sas with(nolock) on sas.CompanyCode = st.CompanyCode and sas.StudentRollNo = st.StudentRollNo inner join StudentAdmission adm with(nolock) on adm.StudentRollNo = st.StudentRollNo And adm.CompanyCode = st.CompanyCode and adm.BranchCode = st.BranchCode and adm.SessionCode = st.SessionCode Where  st.CompanyCode = @CompanyCode and   1 = Case When isnull(@BranchCode,0) > 0 And st.BranchCode = @BranchCode then 1 When isnull(@BranchCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SessionCode,0) > 0 And st.SessionCode = @SessionCode then 1 When isnull(@SessionCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@ClassCode,0) > 0 And st.ClassCode = @ClassCode then 1 When isnull(@ClassCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SubjectCode,0) > 0 And sas.SubjectCode = @SubjectCode then 1 When isnull(@SubjectCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@CourseCode,0) > 0 And sas.CourseCode = @CourseCode then 1 When isnull(@CourseCode,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@Gender,'') <> '' And st.Gender = @Gender then 1 When isnull(@Gender,'') = '' then 1 Else 0 End  And 1 = Case When isnull(@ClassSection,0) > 0 And adm.SectionCode = @ClassSection then 1 When isnull(@ClassSection,0) = 0 then 1 Else 0 End  And 1 = Case When isnull(@SubjectSection,0) > 0 And sas.SectionCode = @SubjectSection then 1 When isnull(@SubjectSection,0) = 0 then 1 Else 0 End  And st.StudentRollNo is not null";
                        ReportName = "Report/rptClassList.rdlc";
                    }  
                    else if (GlobalConstants.SUBJECTCOMBINATION_REPORT == iReportID)
                    {
                        ReportProcedure = "Select distinct st.StudentRollNo,st.StudentNo,st.FullName,sn.SessionName,cl.ClassName,st.Gender,co.CompanyName,br.BranchName,sj.SubjectName from Student st with(nolock) inner join Company co with(nolock) on st.CompanyCode = co.CompanyCode inner join Branch br with(nolock) on br.CompanyCode = st.CompanyCode and br.BranchCode = st.BranchCode  inner join [Session] sn with(nolock) on sn.CompanyCode = st.CompanyCode and sn.SessionCode = st.SessionCode inner join [Class] cl with(nolock) on cl.ClassCode = st.ClassCode and cl.CompanyCode = st.CompanyCode inner join StudentAdmissionSubject sas with(nolock) on sas.CompanyCode = st.CompanyCode and sas.StudentRollNo = st.StudentRollNo and sas.ClassCode = cl.ClassCode inner join Subject sj with(nolock) on sj.SubjectCode = sas.SubjectCode and sj.Status = 1";
                        ReportName = "Report/rptSubjectCombination.rdlc";
                    }
                }
                else
                    return;

                SqlCommand cmdLatest = OpenConnection(ReportProcedure);

                string companyCode = "0";
                string BranchCode = "0";
                string SessionCode = "0";
                string ClassCode = "0";
                string Gender = "";
                string CourseCode = "0";
                string SubjectCode = "0";
                string ClassSectionCode = "0";
                string SubjectSectionCode = "0";

                if (Request.QueryString["CompanyCode"] != "undefined" && Request.QueryString["CompanyCode"] != "null" && Request.QueryString["CompanyCode"] != null)
                    companyCode = Request.QueryString["CompanyCode"].ToString();
                if (Request.QueryString["BranchCode"] != "undefined" && Request.QueryString["BranchCode"] != "null" && Request.QueryString["BranchCode"] != null)
                    BranchCode = Request.QueryString["BranchCode"].ToString();
                if (Request.QueryString["SessionCode"] != "undefined" && Request.QueryString["SessionCode"] != "null" && Request.QueryString["SessionCode"] != null)
                    SessionCode = Request.QueryString["SessionCode"].ToString();
                if (Request.QueryString["ClassCode"] != "undefined" && Request.QueryString["ClassCode"] != "null" && Request.QueryString["ClassCode"] != null)
                    ClassCode = Request.QueryString["ClassCode"].ToString();
                if (Request.QueryString["Gender"] != "undefined" && Request.QueryString["Gender"] != "null" && Request.QueryString["Gender"] != null)
                    Gender = Request.QueryString["Gender"].ToString();
                if (Request.QueryString["CourseCode"] != "undefined" && Request.QueryString["CourseCode"] != null)
                    CourseCode = Request.QueryString["CourseCode"].ToString();
                if (Request.QueryString["SubjectCode"] != "undefined" && Request.QueryString["SubjectCode"] != null)
                    SubjectCode = Request.QueryString["SubjectCode"].ToString();
                if (Request.QueryString["ClassSectionCode"] != "undefined" && Request.QueryString["ClassSectionCode"] != null)
                    ClassSectionCode = Request.QueryString["ClassSectionCode"].ToString();
                if (Request.QueryString["SubjectSectionCode"] != "undefined" && Request.QueryString["SubjectSectionCode"] != null)
                    SubjectSectionCode = Request.QueryString["SubjectSectionCode"].ToString();

                if (GlobalConstants.APPLICATIONFORM_REPORT == iReportID)
                {
                    SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@CompanyCode", companyCode), new SqlParameter("@BranchCode", BranchCode), new SqlParameter("@SessionCode", SessionCode), new SqlParameter("@ClassCode", ClassCode), new SqlParameter("@Gender", Gender), new SqlParameter("@DateFrom", Request.QueryString["DateFrom"].ToString()), new SqlParameter("@DateTo", Request.QueryString["DateTo"].ToString()) };
                    cmdLatest.Parameters.AddRange(paramList);
                }
                if (GlobalConstants.STUDENTFEE_REPORT == iReportID)
                {
                    SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@CompanyCode", companyCode), new SqlParameter("@BranchCode", BranchCode), new SqlParameter("@SessionCode", SessionCode), new SqlParameter("@StudentNo", Convert.ToString(Request.QueryString["StudentNo"])) };
                    cmdLatest.Parameters.AddRange(paramList);
                }
                if (GlobalConstants.APPLICATIONLIST_REPORT == iReportID)
                {
                    SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@CompanyCode", companyCode), new SqlParameter("@BranchCode", BranchCode), new SqlParameter("@SessionCode", SessionCode), new SqlParameter("@ClassCode", ClassCode), new SqlParameter("@Gender", Gender) };
                    cmdLatest.Parameters.AddRange(paramList);
                }
                if (GlobalConstants.CLASSLIST_REPORT == iReportID)
                {
                    SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@CompanyCode", companyCode), new SqlParameter("@BranchCode", BranchCode), new SqlParameter("@SessionCode", SessionCode), new SqlParameter("@ClassCode", ClassCode), new SqlParameter("@Gender", Gender), new SqlParameter("@CourseCode", CourseCode), new SqlParameter("@SubjectCode", SubjectCode), new SqlParameter("@ClassSection", ClassSectionCode), new SqlParameter("@SubjectSection", SubjectSectionCode) };
                    cmdLatest.Parameters.AddRange(paramList);
                }

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
            ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt.Tables[0]));
            // Parameters Section
            if (GlobalConstants.COMPANY_REPORT == iReportID)
            {
                string imagePath = new Uri(Server.MapPath(string.Format("~/Upload/Companies/3.PNG"))).AbsoluteUri;
                Microsoft.Reporting.WebForms.ReportParameter parameter = new Microsoft.Reporting.WebForms.ReportParameter("ImagePath", imagePath);
                ReportViewer1.LocalReport.SetParameters(parameter);
            }
            if (GlobalConstants.STUDENTFEE_REPORT == iReportID)
            {
                string imagePath = new Uri(Server.MapPath(string.Format("~/Upload/Companies/3.PNG"))).AbsoluteUri;
                Microsoft.Reporting.WebForms.ReportParameter parameter1 = new Microsoft.Reporting.WebForms.ReportParameter("ImagePath", imagePath);
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