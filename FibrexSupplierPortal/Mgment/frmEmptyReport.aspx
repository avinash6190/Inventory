<%@ Page Language="C#" MasterPageFile="~/Mgment/mainMaster.Master" AutoEventWireup="true" CodeBehind="frmEmptyReport.aspx.cs" Inherits="FibrexSupplierPortal.Mgment.frmEmptyReport" %>

<%@ Register Src="~/Mgment/Control/POReportMenu.ascx" TagPrefix="uc1" TagName="POReportMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentMenu" runat="server">
    <uc1:POReportMenu runat="server" ID="POReportMenu" />
</asp:Content>
