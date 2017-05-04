<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonrdlcReport.aspx.cs" Inherits="SmsMisWeb.Views.Report.CommonrdlcReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="700px">
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="scrpt" runat="server"></asp:ScriptManager>
    </form>
</body>
</html>
